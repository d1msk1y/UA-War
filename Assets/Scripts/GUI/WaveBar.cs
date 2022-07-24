using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    [SerializeField] Image _fillBar;
    [SerializeField] Image _iconBar;

    private BattleManager _battleManager;

    private void Start()
    {
        _battleManager = GameManager.Instance.battleManager;
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        _fillBar.fillAmount = _battleManager.roundTimer / _battleManager.roundTime;
        _iconBar.transform.localPosition = new Vector3(_fillBar.fillAmount * 100, 0, 0);
    }
}
