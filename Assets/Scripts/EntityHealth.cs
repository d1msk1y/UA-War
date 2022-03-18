using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    public bool isAlive = true;

    [SerializeField] private UnityEvent onDie;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
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