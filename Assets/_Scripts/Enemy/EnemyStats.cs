using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemySO enemySO;

    public float currentMoveSpeed;
    public float currenHealth;
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;
    protected EnemySpawnPoints enemySpawnPoints;

    private void Awake()
    {
        currentMoveSpeed = enemySO.MoveSpeed;
        currenHealth = enemySO.MaxHealth;
        currentDamage = enemySO.Damage;
    }

    private void Start()
    {
        enemySpawnPoints = FindObjectOfType<EnemySpawnPoints>();
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currenHealth -= dmg;
        if (currenHealth <= 0) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + enemySpawnPoints.GetRandom().position;

    }
}
