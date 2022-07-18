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

    public UnityEvent onDie;

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
        if (damage < 0)
        {
            Debug.LogWarning("Received damage < 0");
        }
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
            Destroy(gameObject);
            return false;
        }
        else
            return true;
    }
}