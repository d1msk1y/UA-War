using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Soldier : Actor
{
    public GameObject legs;
    public AIPath aIPath;

    private void Update()
    {
        Vector3 walkDir = aIPath.destination - transform.position;

        float legAngle = Mathf.Atan2(walkDir.y, walkDir.x) * Mathf.Rad2Deg - 90;
        legs.transform.rotation = Quaternion.Euler(0, 0, legAngle);
    }
}
