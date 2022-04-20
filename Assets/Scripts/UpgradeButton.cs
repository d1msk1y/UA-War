using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [Header("Parameters")]
    private int _stage = 1;//current upgrade stage
    private int _price;
    [SerializeField] private UpgradeSO _upgradeSO;

    private float _upgradeValue;//nextUpgrade
    public float UpgradeValue { get { return _upgradeValue; } set { _upgradeValue = value; } }

    private Button _button;

    public delegate void UpgradesHandler(int a);
    public UnityEvent OnUpgrade;

    private void Start()
    {
        SetButton();
    }

    private int CalculatedPrice()
    {
        float multipiedPrice = _price - 2;
        multipiedPrice *= _upgradeSO.priceMultiplier;

        return (int)multipiedPrice;
    }

    private bool WithdrawCoins() => GameManager.Instance.scoreSystem.WithdrawCoins(CalculatedPrice());
    //Cause array lenght doesn't match the latest array obj
    private bool StageInBounds() => _stage < _upgradeSO.upgradeStages.Length - 1;

    private bool TransactionAccepted()
    {
        if (!StageInBounds())
        {
            Debug.Log("Upgrade is completely bought");
            DenyTransaction();
            return false;
        }
        else if (!WithdrawCoins())
        {
            DenyTransaction();
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Upgrade()
    {
        if (!TransactionAccepted())
            return;
        _stage++;
        UpgradeValue = _upgradeSO.upgradeStages[_stage];
        OnUpgrade?.Invoke();
    }

    private void DenyTransaction()
    {
        _button.interactable = false;
    }

    private void SetButton()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(() =>
        {
            Upgrade();
        });
    }
}
