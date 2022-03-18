using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Soldier : Actor
{
    public LayerMask targetMask;
    public GameObject legs;
    public AIPath aIPath;
    public float radius;

    public Transform destinationTarget;
    public Transform aimTarget;

    private RaycastHit2D gunRaycast;
    private void GetRaycastHit() => gunRaycast = gun.gunRaycast;

    private void Start()
    {
        gun.onShootEvent += GetRaycastHit;

        gun.targetMask = targetMask;
        destinationTarget = BaseController.instance.transform;
        aimTarget = BaseController.instance.transform;
        aIPath.destination = destinationTarget.position;
    }

    private void Update()
    {
        if (aimTarget == null)
            return;

        body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(aimTarget));
        legs.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destinationTarget));

        if (!TargetInRadius())
            return;
        gun.Shoot(aimTarget.position);

    }

    private bool TargetInRadius()
    {
        if (Vector2.Distance(transform.position, aimTarget.transform.position) > radius)
            return false;
        else
            return true;
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.green;
    }

}
