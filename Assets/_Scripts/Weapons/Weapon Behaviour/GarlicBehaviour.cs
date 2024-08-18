using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    protected List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemyStats = col.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(currentDamage);
            markedEnemies.Add(col.gameObject);
        }
        else if (col.CompareTag("Prop") && !markedEnemies.Contains(col.gameObject))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                markedEnemies.Add(col.gameObject);
            }
        }
    }
}
