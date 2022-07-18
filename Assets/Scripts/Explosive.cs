using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Explosive : MonoBehaviour
{
    public float explosionRadius;
    [SerializeField] private int _startDamage;
    public EntityScanner entityScanner;

    public void Explode()
    {
        foreach (EntityHealth entity in entityScanner.GetEntitiesInRadius())
        {
            entity.TakeDamage(_startDamage);
            GameManager.Instance.ShakeScreen(20);
        }
    }

    private int CalculateDamage(EntityHealth entity)
    {
        float entityDistance = Vector2.Distance(transform.position, entity.transform.position);
        float damageMultiplier = entityDistance / explosionRadius;
        return (int)(_startDamage * damageMultiplier);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}