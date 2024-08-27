using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;

    //References
    Rigidbody2D rigidBody;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        rigidBody = transform.parent.GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);
    }

    private void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        if (GameManager.Instance.isGameover) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    //Last moved X
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);  //Last moved Y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        if (GameManager.Instance.isGameover) return;
        rigidBody.velocity = new Vector2(moveDir.x * playerStats.CurrentMoveSpeed, moveDir.y * playerStats.CurrentMoveSpeed);
    }
}
