using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PassiveItemSO", menuName = "_ScriptableObject/Passive Item")]
public class PassiveItemSO : ScriptableObject
{
    [SerializeField]
    private float multipler;
    public float Multipler { get => multipler; set => multipler = value; }

    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
}
