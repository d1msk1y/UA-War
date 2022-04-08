using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [Header("Parameters")]
    private int _stage;//current upgrade stage
    private int _price;
    [SerializeField] private UpgradeSO _upgradeSO;

    private int _upgradeValue;//nextUpgrade
    public int UpgradeValue { get { return _upgradeValue; } set { _upgradeValue = value; } }

    private Button _button;

    public delegate void UpgradesHandler(int a);
    public UnityEvent OnUpgrade;

    private void Start()
    {
        SetButton();
    }

    private bool WithdrawCoins() => GameManager.Instance.scoreSystem.WithdrawCoins(CalculatedPrice());
    private bool StageInBounds() => _stage >= _upgradeSO.upgradeStages.Length;

    public void Upgrade()
    {
        if (StageInBounds() || WithdrawCoins())
            return;
        _stage++;
        _upgradeValue = _upgradeSO.upgradeStages[_stage];
        OnUpgrade?.Invoke();
    }

    private int CalculatedPrice()
    {
        float multipiedPrice = _price - 2;
        multipiedPrice *= _upgradeSO.priceMultiplier;

        return (int)multipiedPrice;
    }

    private void SetButton()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            Upgrade();
        });
    }
}
