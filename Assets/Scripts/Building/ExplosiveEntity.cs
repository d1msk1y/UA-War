using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Explosive))]
public class ExplosiveEntity : ActionEntity
{
    private Explosive _explosiveBody;

    internal override void OnEnable()
    {
        base.OnEnable();
        _explosiveBody = GetComponent<Explosive>();
        _explosiveBody.explosionRadius = BuildingParams.attackRadius;
        _explosiveBody.entityScanner = new EntityScanner(BuildingParams.attackRadius, BuildingParams.vulnerable, transform);
    }

    private new void Update()
    {

    }
}