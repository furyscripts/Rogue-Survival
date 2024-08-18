using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName ="_ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    //Base stats for enemies

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }
}