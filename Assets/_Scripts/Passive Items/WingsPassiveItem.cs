using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        playerStats.currentMoveSpeed *= 1 + passiveItemSO.Multipler / 100f;
    }
}