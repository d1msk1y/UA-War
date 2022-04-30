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

    [Header("GUI")]
    [SerializeField] private Button _startButton;

    [Header("Shop")]
    [SerializeField] private GameObject _shop;

    [Header("Hostages")]
    [SerializeField] private GameObject _hostages;
    [SerializeField] private GameObject _hostageCaught;

    [Header("UI GFX")]
    public GameObject _gunTxt;
    public GameObject _headerTxt;

    [Header("UI VFX")]
    public ParticleSystem coinsWithdraw;

    private void Start()
    {
        GameManager.Instance.scoreSystem.OnMoneyChange += SpawnWithdrawVfx;
    }

    public void SetCoinsText(int coins) => _coinsText.text = coins.ToString();

    public void SetNewGUIText(GameObject textObj, Vector3 position, string text)
    {
        TextMeshProUGUI uiText = Instantiate(textObj, position, Quaternion.identity, _hud.transform)
        .GetComponentInChildren<TextMeshProUGUI>();
        uiText.text = text;

        Destroy(uiText, uiText.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void ToggleShop() => GameManager.Instance.shopManager.ToggleShop();
    public void ToggleHostages() => Debug.Log("Hostages toggled");

    public void ToggleHostageCaught()
    {
        if (_hostageCaught.activeSelf)
            _hostageCaught.SetActive(false);
        else
            _hostageCaught.SetActive(true);
    }

    public void SpawnWithdrawVfx()
    => Instantiate(coinsWithdraw, _coinsText.transform.position, Quaternion.identity, _hud.transform);
}