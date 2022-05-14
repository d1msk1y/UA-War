using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Actor : MonoBehaviour
{
    [Header("Base References")]
    public GameObject body;

    internal float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }
}
