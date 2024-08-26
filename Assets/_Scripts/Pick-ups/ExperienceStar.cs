using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceStar : Pickup, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.IncreaseExperience(experienceGranted);
    }
}
