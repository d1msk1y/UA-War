using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ActionEntity
{
    [Header("Parameters")]
    [SerializeField] private float _explosionDelay;

    private Detonator _detonator;

    internal override void OnEnable()
    {
        base.OnEnable();
        _detonator = GetComponent<Detonator>();
    }

    private IEnumerator Detonate()
    {
        yield return new WaitForSeconds(_explosionDelay);
        _detonator.Detonate();
        _entityHealth.TakeDamage(_entityHealth.MaxHealth);
    }

    internal override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        StartCoroutine(Detonate());
    }
}
