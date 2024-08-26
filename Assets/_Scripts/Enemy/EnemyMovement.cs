using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyStats enemyStats;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        enemyStats = FindObjectOfType<EnemyStats>();
    }

    private void Update()
    {
        transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime);
        Flip();
    }

    void Flip()
    {
        if (player.transform.position.x > transform.parent.position.x) transform.parent.localScale = new Vector2(1f, 1f);
        else transform.parent.localScale = new Vector2(-1f, 1f);
    }
}
