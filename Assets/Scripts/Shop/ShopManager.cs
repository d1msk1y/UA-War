using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop relative")]
    [SerializeField] private Animator _shopAnimator;
    [SerializeField] private GameObject _upgradesPage;

    [SerializeField] bool _toggledShop;

    public void ToggleShop()
    {
        if (!_toggledShop)
            OpenShop();
        else
            CloseShop();
    }

    private void OpenShop()
    {
        _toggledShop = true;
        _shopAnimator.Play("Open Shop");
    }

    private void CloseShop()
    {
        _toggledShop = false;
        _shopAnimator.Play("Close Shop");
    }
}
