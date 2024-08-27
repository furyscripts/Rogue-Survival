using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterSO characterSO;

    //Current Stats
    [SerializeField]
    private float currentHealth;
    private float currentRecovery;
    private float currentMoveSpeed;
    private float currentMight;
    private float currentProjectileSpeed;
    private float currentMagnet;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.Instance != null) GameManager.Instance.currentHealthDisplay.text = "Health: " + currentHealth;
            }
        }
    }

    public float CurrentRecovery
    {
        get => currentRecovery;
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.Instance != null) GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get => currentMoveSpeed;
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.Instance != null) GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
            }
        }
    }

    public float CurrentMight
    {
        get => currentMight;
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.Instance != null) GameManager.Instance.currentMightDisplay.text = "Might: " + currentMight;
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get => currentProjectileSpeed;
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.Instance != null) GameManager.Instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
            }
        }
    }

    public float CurrentMagnet
    {
        get => currentMagnet;
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.Instance != null) GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
            }
        }
    }
    #endregion


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
        AssignVariables();
        //Spawn the starting weapon
        SpawnStarting();
    }
    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        //Set the current stats display
        SetCurrentStatsDisplay();
    }

    private void Update()
    {
        if (invincibilityTimer > 0) invincibilityTimer -= Time.deltaTime;
        else if (isInvincible) isInvincible = false;
        Recover();
    }

    void AssignVariables()
    {
        CurrentHealth = characterSO.MaxHealth;
        currentRecovery = characterSO.Recovery;
        currentMoveSpeed = characterSO.MoveSpeed;
        currentMight = characterSO.Might;
        currentProjectileSpeed = characterSO.ProjectileSpeed;
        currentMagnet = characterSO.Magnet;
    }

    void SpawnStarting()
    {
        SpawnWeapon(characterSO.StartingWeapon);
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveItem);
        SpawnPassiveItem(secondPassiveItem);
    }

    void SetCurrentStatsDisplay()
    {
        GameManager.Instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.Instance.currentMoveSpeedDisplay.text = "MoveSpeed: " + currentMoveSpeed;
        GameManager.Instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.Instance.currentProjectileSpeedDisplay.text = "ProjectileSpeed: " + currentProjectileSpeed;
        GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;

        GameManager.Instance.AssignChosenCharacterUI(characterSO);
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
            CurrentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHealth <= 0) Kill();
        }
    }

    public void Kill()
    {
        if (!GameManager.Instance.isGameover)
        {
            GameManager.Instance.AssignLevelReachedUI(level);
            GameManager.Instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.Instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (CurrentHealth < characterSO.MaxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > characterSO.MaxHealth) CurrentHealth = characterSO.MaxHealth;
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterSO.MaxHealth)
        {
            CurrentHealth += currentRecovery * Time.deltaTime;
            if (CurrentHealth > characterSO.MaxHealth) CurrentHealth = characterSO.MaxHealth;
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count)
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
