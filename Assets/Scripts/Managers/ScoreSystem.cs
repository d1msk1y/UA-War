using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _totalCoins;
    [SerializeField] private int _obtainedScore;
    public int TotalCoins
    {
        get { return _totalCoins; }
        set
        {
            _totalCoins = value;
            OnMoneyChange?.Invoke();
            GameManager.Instance.uiManager.SetCoinsText(_totalCoins);
        }
    }

    public delegate void ScoreHandler();
    public event ScoreHandler OnMoneyChange;

    public void AddScore(int score)
    {
        _obtainedScore += score;
    }

    public void AddCoins(int coins)
    {
        TotalCoins += coins;
    }

    //Return Successful/Unsuccessful result of cash out. True = success.
    public bool WithdrawCoins(int coins)
    {
        if (coins > TotalCoins)
        {
            Debug.Log("Not enough coins");
            return false;
        }
        else
        {
            TotalCoins -= coins;
            return true;
        }
    }
}