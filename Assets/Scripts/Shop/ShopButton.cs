using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    internal int _price;

    [Header("GFX")]
    [SerializeField] internal TextMeshProUGUI _priceText;
    internal Button _button;

    internal virtual void Start()
    {
        SetGfx();
    }

    internal virtual bool WithdrawCoins() => GameManager.Instance.scoreSystem.WithdrawCoins(_price);
    //Cause array lenght doesn't match the latest array obj

    internal virtual bool ValidateTransaction()
    {
        if (!WithdrawCoins())
        {
            DenyTransaction();
            return false;
        }
        else
        {
            return true;
        }
    }

    internal virtual void SetGfx()
    {
        _priceText.text = _price.ToString();
    }

    internal void DenyTransaction()
    {
        //fun stuff to do
    }
}
