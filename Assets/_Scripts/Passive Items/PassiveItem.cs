using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : MonoBehaviour
{
    protected PlayerStats playerStats;
    public PassiveItemSO passiveItemSO;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }

    protected abstract void ApplyModifier();
}
