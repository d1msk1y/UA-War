using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject _hud;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private GameObject _gunTxt;

    [Header("Shop")]
    [SerializeField] private GameObject _shop;

    [Header("Hostages")]
    [SerializeField] private GameObject _hostages;
    [SerializeField] private GameObject _hostageCaught;

    public void SetCoinsText(int coins) => _coinsText.text = coins.ToString();

    public void ToggleShop() => GameManager.Instance.shopManager.ToggleShop();
    public void ToggleHostages() => Debug.Log("Hostages toggled");

    public void SetGunText(Vector3 position, string text)
    {
        TextMeshProUGUI gunText = Instantiate(_gunTxt, position, Quaternion.identity, _hud.transform)
        .GetComponentInChildren<TextMeshProUGUI>();
        gunText.text = text;
    }

    public void ToggleHostageCaught()
    {
        if (_hostageCaught.activeSelf)
            _hostageCaught.SetActive(false);
        else
            _hostageCaught.SetActive(true);
    }
}