using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EnemyName
{
    E_Zombie_01 = 0,
    E_Zombie_02 = 1
}

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public EnemyName enemyName;
        public int enemyCount;
        public int spawnCount;
        //public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;

    [Header("Spawned Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;

    [Header("Spawn Positions")]
    protected EnemySpawnPoints enemySpawnPoints;
    //public List<Transform> relativeSpawnPoints;

    Transform player;

    // Sử dụng một mảng hoặc List để lưu các prefab
    public GameObject[] enemyPrefabs;

    private void Awake()
    {
        LoadPrefabs();
    }

    private void Start()
    {
        enemySpawnPoints = FindObjectOfType<EnemySpawnPoints>();
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    private void Update()
    {
        //Check if the wave has ended and the next wave should start
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) StartCoroutine(BeginNextWave());
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //Wave for 'waveInterval' seconds before starting the next wave.
        yield return new WaitForSeconds(waveInterval);

        //If there are more waves to start after the current wave, move onto the next wave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void LoadPrefabs()
    {
        AssetPrefabLoader prefabLoader = new AssetPrefabLoader();
        enemyPrefabs = prefabLoader?.LoadPrefabs("Enemies");
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    GameObject enemyPrefab = enemyPrefabs[(int)enemyGroup.enemyName];
                    GameObject enemy = Instantiate(enemyPrefab, player.position + enemySpawnPoints.GetRandom().position, Quaternion.identity);
                    enemy.transform.parent = transform;

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        if (enemiesAlive < maxEnemiesAllowed) maxEnemiesReached = false;
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
