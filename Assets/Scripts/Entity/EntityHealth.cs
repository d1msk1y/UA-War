using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    [Header("Health")]
    public ParticleSystem hitParticle;

    public int Health { get => _health; set => _health = value; }
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            Health = value;
        }
    }

    [SerializeField] private UnityEvent onDie;

    public delegate void EntityHealthHandler();
    public event EntityHealthHandler onDamageEvent;
    public event EntityHealthHandler onDieEvent;

    public void CreateParticle(ParticleSystem FX) => Instantiate(FX, transform.position, Quaternion.identity);

    private void Start()
    {
        Health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        onDamageEvent?.Invoke();
        IsEntityAlive();
    }

    private bool IsEntityAlive()
    {
        if (Health <= 0)
        {
            onDie?.Invoke();
            onDieEvent?.Invoke();
            GameManager.Instance.ScanAStar();
            return false;
        }
        else
            return true;
    }
}