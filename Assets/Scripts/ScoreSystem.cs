using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _totalCoins;
    [SerializeField] private int _obtainedScore;

    public void AddScore(int score)
    {
        _obtainedScore += score;
    }

    public void AddCoins(int coins)
    {
        _totalCoins += coins;
    }

    //Return Successful/Unsuccessful result of cash out. True = success.
    public bool WithdrawCoins(int coins)
    {
        if (coins > _totalCoins)
        {
            Debug.Log("Not enough coins");
            return false;
        }
        else
        {
            _totalCoins -= coins;
            return true;
        }
    }
}
