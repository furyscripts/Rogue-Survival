using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
