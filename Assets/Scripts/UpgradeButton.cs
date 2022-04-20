using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [Header("Parameters")]
    private int _stage;//current upgrade stage
    private int _price;
    [SerializeField] private UpgradeSO _upgradeSO;

    [Header("GFX")]
    [SerializeField] private TextMeshProUGUI _stageText;
    [SerializeField] private TextMeshProUGUI _priceText;
    private Button _button;

    private float _upgradeValue;//nextUpgrade
    public float UpgradeValue { get { return _upgradeValue; } set { _upgradeValue = value; } }

    public delegate void UpgradesHandler(int a);
    public UnityEvent OnUpgrade;

    private void Start()
    {
        SetButton();
        _price = _upgradeSO.price;
        SetGfx();
        GameManager.Instance.scoreSystem.OnMoneyChange += CheckButtonAbility;
    }

    private int CalculatedPrice()
    {
        float multipiedPrice = _price;
        multipiedPrice *= _upgradeSO.priceMultiplier;
        _price = (int)multipiedPrice;

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
            _button.interactable = false;
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

    private void CheckButtonAbility()
    {
        if (GameManager.Instance.scoreSystem.TotalCoins < _price)
            _button.interactable = false;
        else
            _button.interactable = true;
    }

    public void Upgrade()
    {
        if (!TransactionAccepted())
            return;
        _stage++;
        SetGfx();
        UpgradeValue = _upgradeSO.upgradeStages[_stage];
        OnUpgrade?.Invoke();
    }

    private void SetGfx()
    {
        _stageText.text = (_stage + 1) + "/" + _upgradeSO.upgradeStages.Length; // front-end stage serializer
        _priceText.text = _price.ToString();
    }

    private void DenyTransaction()
    {
        //Some fun gfx stuff to do
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
