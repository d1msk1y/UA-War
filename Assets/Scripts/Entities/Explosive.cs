using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Explosive : Detonator
{
    [SerializeField] internal int _damage;

    private EntityHealth _entityHealth;

    internal override void OnEnable()
    {
        base.OnEnable();
        _entityHealth = GetComponent<EntityHealth>();
    }

    public override void Detonate()
    {
        List<EntityHealth> entities = _entityScanner.GetEntitiesInRadius();
        entities.Remove(_entityHealth);
        Destroy(gameObject);
        foreach (EntityHealth entity in entities)
        {
            GameManager.Instance.ShakeScreen(10);
            entity.TakeDamage(_damage);
        }
    }
}