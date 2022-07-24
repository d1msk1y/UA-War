using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Transport
{
    private new void OnEnable()
    {
        base.OnEnable();
        SetDestination(BaseController.instance.transform);
        StartMove();
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }
}