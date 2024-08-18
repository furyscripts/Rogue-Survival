using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public WeaponSO weaponSO;

    public float destroyAfterSecond;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponSO.Damage;
        currentSpeed = weaponSO.Speed;
        currentCooldownDuration = weaponSO.CooldownDuration;
        currentPierce = weaponSO.Pierce;
    }

    private void Start()
    {
        Destroy(gameObject, destroyAfterSecond);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = col.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(currentDamage);
            ReducePierce();
        }
    }

    protected virtual void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0) Destroy(gameObject);
    }
}
