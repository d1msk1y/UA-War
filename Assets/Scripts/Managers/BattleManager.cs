using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Rounds")]
    public EnemySpawner enemySpawner;

    [Header("Rounds")]
    public int roundTime;
    public float roundTimer;
    [SerializeField] private int _currentRound;
    [SerializeField] private bool _isRest = true;

    public bool IsRest
    {
        get { return _isRest; }
        set
        {
            _isRest = value;
            if (value == true)
                OnRest?.Invoke();
        }
    }

    public delegate void BattleHandler();
    public event BattleHandler OnRest;

    public void StartRound()
    {
        IsRest = false;
        _currentRound++;
        GameManager.Instance.uiManager.SetNewGUIText(GameManager.Instance.uiManager._headerTxt,
         new Vector2(0, 5), "WAVE " + _currentRound);
        StartCoroutine(StartTimer(roundTime, StopRound));
    }

    private void StopRound()
    {
        IncreaseComplexity();
        GameManager.Instance.uiManager.ShowStartButton();
        GameManager.Instance.uiManager.SetNewGUIText(GameManager.Instance.uiManager._headerTxt,
        new Vector2(0, 5), "You win! Russians escaped!");
        IsRest = true;
    }

    private IEnumerator StartTimer(float time, BattleHandler function)
    {
        roundTimer = 0;
        while (roundTimer < time)
        {
            roundTimer += 1 * Time.deltaTime;
            if (roundTimer >= time)
            {
                function();
            }
            yield return null;
        }
    }

    private void IncreaseComplexity()
    {
        enemySpawner.IncreaseSpawnRatio();
    }
}