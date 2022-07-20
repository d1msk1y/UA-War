using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Detonator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] internal float damageRadius;
    [SerializeField] internal LayerMask _vulnerable;

    internal EntityScanner _entityScanner;

    internal virtual void OnEnable()
    {
        _entityScanner = new EntityScanner(damageRadius, _vulnerable, transform);
    }

    public virtual void Detonate()
    {
        //Detonation algoritm
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
