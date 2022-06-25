using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private LayerMask _vulnerable;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _startDamage;

    public void Explode()
    {
        foreach (EntityHealth entity in GetEntitiesInRadius())
        {
            entity.TakeDamage(_startDamage);
        }
    }

    private int CalculateDamage(EntityHealth entity)
    {
        float entityDistance = Vector2.Distance(transform.position, entity.transform.position);
        float damageMultiplier = entityDistance / _explosionRadius;
        return (int)(_startDamage * damageMultiplier);
    }

    public List<EntityHealth> GetEntitiesInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _vulnerable);

        return CheckEntities(colliders);
    }

    private List<EntityHealth> CheckEntities(Collider2D[] colliders)
    {
        List<EntityHealth> entities = new List<EntityHealth>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EntityHealth entityHealth))
            {
                entities.Add(entityHealth);
            }
        }
        return entities;
    }
}