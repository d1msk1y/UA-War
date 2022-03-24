using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    public bool isAlive = true;

    [Header("Health")]
    public ParticleSystem hitParticle;

    [SerializeField] private UnityEvent onDie;

    public delegate void OnDamage();
    public event OnDamage onDamageEvent;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        onDamageEvent?.Invoke();
        isAlive = IsEntityAlive();
    }

    private bool IsEntityAlive()
    {
        if (_health < 0)
        {
            Destroy(gameObject);
            onDie?.Invoke();
            return false;
        }
        else
            return true;
    }
}