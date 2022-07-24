using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float moneyMultiplier;
    [SerializeField] private int _totalCoins;
    [SerializeField] private int _obtainedScore;
    public int TotalCoins
    {
        get => _totalCoins;
        set
        {
            _totalCoins = value;
            OnMoneyChange?.Invoke();
            GameManager.Instance.uiManager.SetCoinsText(_totalCoins);
        }
    }

    public delegate void ScoreHandler();
    public event ScoreHandler OnMoneyChange;

    public void AddScore(int score) => _obtainedScore += score;
    public void AddCoins(int coins) => TotalCoins += (int)(coins * moneyMultiplier);

    //Return Successful/Unsuccessful result of withdrawal.
    public bool WithdrawCoins(int coins)
    {
        if (coins > TotalCoins)
            return false;

        TotalCoins -= coins;
        GameManager.Instance.soundManager.PlaySoundEvent(GameManager.Instance.soundManager.withdraw);
        return true;
    }
}