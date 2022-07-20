using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

[RequireComponent(typeof(EntityHealth))]
public class Freezable : Detonator
{
    [SerializeField] private int _freezeTime;

    internal override void OnEnable()
    {
        base.OnEnable();
        _entityScanner = new EntityScanner(damageRadius, _vulnerable, transform);
    }

    public override void Detonate()
    {
        foreach (EntityHealth entity in _entityScanner.GetEntitiesInRadius())
        {
            entity.StartCoroutine(entity.GetComponent<Enemy>().Freeze(_freezeTime));
            GameManager.Instance.ShakeScreen(10);
        }
    }
}