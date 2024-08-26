using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PassiveItemSO", menuName = "_ScriptableObject/Passive Item")]
public class PassiveItemSO : ScriptableObject
{
    [SerializeField]
    private float multipler;
    public float Multipler { get => multipler; set => multipler = value; }
}
