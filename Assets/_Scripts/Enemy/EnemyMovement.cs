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
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime);
        Flip();
    }

    void Flip()
    {
        if (player.transform.position.x > transform.position.x) transform.localScale = new Vector2(1f, 1f);
        else transform.localScale = new Vector2(-1f, 1f);
    }
}
