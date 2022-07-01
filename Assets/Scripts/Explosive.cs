using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private LayerMask _vulnerable;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private int _startDamage;
    private EntityScanner _entityScanner;

    private void Start()
    {
        _entityScanner = new EntityScanner(_explosionRadius, _vulnerable, transform);
    }

    public void Explode()
    {
        foreach (EntityHealth entity in _entityScanner.GetEntitiesInRadius())
        {
            entity.TakeDamage(_startDamage);
            GameManager.Instance.ShakeScreen(20);
        }
    }

    private int CalculateDamage(EntityHealth entity)
    {
        float entityDistance = Vector2.Distance(transform.position, entity.transform.position);
        float damageMultiplier = entityDistance / _explosionRadius;
        return (int)(_startDamage * damageMultiplier);
    }
}