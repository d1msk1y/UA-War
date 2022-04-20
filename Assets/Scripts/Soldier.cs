using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Soldier : Enemy
{
    private RaycastHit2D gunRaycast;
    private void GetRaycastHit() => gunRaycast = gun.gunRaycast;

    private void Start()
    {
        gun.OnShootEvent += GetRaycastHit;
        gun.targetMask = enemyParams.targetMask;
        destinationTarget = BaseController.instance.transform;
        aIPath.destination = destinationTarget.position;

        mainAction = Shoot;
    }

    private void Shoot() => gun.Shoot(destinationTarget.position);
}