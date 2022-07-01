using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScanner
{
    private float _radius;
    private LayerMask _entityFilter;
    private Transform _transform;

    public EntityScanner(float radius, LayerMask entityFilter, Transform transform)
    {
        _radius = radius;
        _entityFilter = entityFilter;
        _transform = transform;
    }

    public List<EntityHealth> GetEntitiesInRadius()
    {
        if (_transform == null)
            return null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _radius, _entityFilter);

        return CheckEntities(colliders);
    }

    public List<EntityHealth> GetAllEntities()
    {
        if (_transform == null)
            return null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 300, _entityFilter);

        return CheckEntities(colliders);
    }

    public EntityHealth GetClosestEntity(List<EntityHealth> entities)
    {
        if (_transform == null)
            return null;

        EntityHealth result = null;
        float num = float.PositiveInfinity;
        Vector3 position = _transform.position;
        foreach (EntityHealth entity in entities)
        {
            if (entity == null)
            {
                return null;
            }
            float num2 = Vector3.Distance(entity.transform.position, position);
            if (num2 < num)
            {
                result = entity;
                num = num2;
            }
        }
        return result;
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