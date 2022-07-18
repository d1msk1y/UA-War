using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : ShopButton
{
    [Header("Parameters")]
    private int _stage;//current upgrade stage
    [SerializeField] private UpgradeSO _upgradeSO;

    [Header("GFX")]
    [SerializeField] private TextMeshProUGUI _stageText;

    private float _upgradeValue;//nextUpgrade
    public float UpgradeValue { get { return _upgradeValue; } set { _upgradeValue = value; } }

    public delegate void UpgradesHandler(int a);
    public UnityEvent OnUpgrade;

    internal override void Start()
    {
        _price = _upgradeSO.price;
        base.Start();
        SetButton();
    }

    private int CalculatePrice()
    {
        float multipiedPrice = _price;
        multipiedPrice *= _upgradeSO.priceMultiplier;

        return (int)multipiedPrice;
    }

    internal override bool WithdrawCoins() => GameManager.Instance.scoreSystem.WithdrawCoins(CalculatePrice());
    //Cause array lenght doesn't match the latest array obj
    private bool StageInBounds() => _stage < _upgradeSO.upgradeStages.Length - 1;

    internal override bool ValidateTransaction()
    {
        if (!StageInBounds() || !WithdrawCoins())
        {
            DenyTransaction();
            return false;
        }
        else
        {
            _price = (int)CalculatePrice();
            SetGfx();
            return true;
        }
    }

    public void Upgrade()
    {
        if (!ValidateTransaction())
            return;
        _stage++;
        SetGfx();
        UpgradeValue = _upgradeSO.upgradeStages[_stage];
        OnUpgrade?.Invoke();
    }

    internal override void SetGfx()
    {
        base.SetGfx();
        _stageText.text = (_stage + 1) + "/" + _upgradeSO.upgradeStages.Length; // front-end stage serializer
    }

    private void SetButton()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(() => { Upgrade(); });
    }
}
