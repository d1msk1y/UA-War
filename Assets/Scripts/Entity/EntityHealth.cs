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

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }

        set
        {
            _maxHealth = value;
            _health = value;
        }
    }

    public UnityEvent onDie;

    public delegate void EntityHandler();
    public event EntityHandler onDamageEvent;
    public event EntityHandler onDieEvent;

    public void CreateParticle(ParticleSystem FX) => Instantiate(FX, transform.position, Quaternion.identity);

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Received damage < 0");
        }
        _health -= damage;
        onDamageEvent?.Invoke();
        isAlive = IsEntityAlive();
    }

    private bool IsEntityAlive()
    {
        if (_health < 0)
        {
            onDie?.Invoke();
            onDieEvent?.Invoke();
            Destroy(gameObject);
            return false;
        }
        else
            return true;
    }
}