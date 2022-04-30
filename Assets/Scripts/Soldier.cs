using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Soldier : Enemy
{
    private RaycastHit2D gunRaycast;
    public Gun gun;
    private void GetRaycastHit() => gunRaycast = gun.gunRaycast;

    private new void Start()
    {
        base.Start();
        gun.OnShootEvent += GetRaycastHit;
        gun.targetMask = enemyParams.targetMask;
        DestinationTarget = BaseController.instance.transform;

        Attack = Shoot;
    }

    private void Shoot() => gun.Shoot(DestinationTarget.position);

    internal override void Escape()
    {
        base.Escape();
        Disarm();
    }

    private void Disarm() => gun.Ammos = 0;
}