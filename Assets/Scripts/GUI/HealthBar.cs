using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private EntityHealth _entityHealth;

    private void OnEnable()
    {
        _entityHealth.onDamageEvent += UpdateBar;
    }

    private void UpdateBar()
    {
        _fillImage.fillAmount = _entityHealth.Health / _entityHealth.MaxHealth;
        Debug.Log("Max health =" + _entityHealth.MaxHealth);
        Debug.Log("Health =" + _entityHealth.Health);
        Debug.Log("Health / MaxHealth = " + _entityHealth.Health / _entityHealth.MaxHealth);
    }
}