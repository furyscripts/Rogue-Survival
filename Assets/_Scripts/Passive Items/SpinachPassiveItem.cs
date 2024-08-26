using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        playerStats.currentMight *= 1 + passiveItemSO.Multipler / 100f;
    }
}
