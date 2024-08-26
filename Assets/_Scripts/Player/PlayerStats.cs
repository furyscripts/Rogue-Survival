using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterSO characterSO;

    [HideInInspector]
    public float currenHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    [Header("Exprerience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItem, secondPassiveItem;

    private void Awake()
    {
        characterSO = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        //Assign the variables
        currenHealth = characterSO.MaxHealth;
        currentRecovery = characterSO.Recovery;
        currentMoveSpeed = characterSO.MoveSpeed;
        currentMight = characterSO.Might;
        currentProjectileSpeed = characterSO.ProjectileSpeed;
        currentMagnet = characterSO.Magnet;

        //Spawn the starting weapon
        SpawnWeapon(characterSO.StartingWeapon);
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveItem);
        SpawnPassiveItem(secondPassiveItem);
    }
    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    private void Update()
    {
        if (invincibilityTimer > 0) invincibilityTimer -= Time.deltaTime;
        else if (isInvincible) isInvincible = false;
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currenHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currenHealth <= 0) Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("PLAYER IS DEAD");
        //Destroy(gameObject);
    }

    public void RestoreHealth(float amount)
    {
        if(currenHealth < characterSO.MaxHealth)
        {
            currenHealth += amount;
            if (currenHealth > characterSO.MaxHealth) currenHealth = characterSO.MaxHealth;
        }
    }

    void Recover()
    {
        if(currenHealth < characterSO.MaxHealth)
        {
            currenHealth += currentRecovery * Time.deltaTime;
            if (currenHealth > characterSO.MaxHealth) currenHealth = characterSO.MaxHealth;
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.weaponSlots.Count)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
