using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemySO enemySO;

    public float currentMoveSpeed;
    public float currenHealth;
    public float currentDamage;

    private void Awake()
    {
        currentMoveSpeed = enemySO.MoveSpeed;
        currenHealth = enemySO.MaxHealth;
        currentDamage = enemySO.Damage;
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
}
