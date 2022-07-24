using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : ActionEntity
{
    [SerializeField] private int _damage;

    private void Hit(Enemy enemy)
    {
        enemy.GetComponent<EntityHealth>().TakeDamage(_damage);
        _entityHealth.TakeDamage(1);
    }

    internal override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy enemy))
            return;

        base.OnTriggerEnter2D(other);
        Hit(enemy);
    }
}