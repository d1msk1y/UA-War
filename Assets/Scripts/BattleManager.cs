using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Rounds")]
    [SerializeField] private int _currentRound;
    [SerializeField] private int _roundTime;
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

    [Header("Spawn")]
    public List<Transform> currentEnemiesInAction;
    public Transform[] spawners;
    public Transform escapePoint;
    private float _spawnerTimer;
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private float _spawnRatio;

    [Header("Complexity")]
    [SerializeField] private float _coplexityModifier;
    [SerializeField] private float _minSpawnRatio;

    public delegate void BattleHandler();
    public event BattleHandler OnRest;

    private void Start()
    {
        currentEnemiesInAction = new List<Transform>();
    }

    private void Update()
    {
        if (ReadyToSpawn())
        {
            SpawnEnemy(RandomEnemy());
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            StopRound();
        }
    }

    #region  Rounds

    public void StartRound()
    {
        IsRest = false;
        _currentRound++;
        GameManager.Instance.uiManager.SetNewGUIText(GameManager.Instance.uiManager._headerTxt,
         new Vector2(0, 5), "WAVE " + _currentRound);
        StartCoroutine(StartTimer(_roundTime, StopRound));
    }

    private void StopRound()
    {
        IncreaseComplexity();
        GameManager.Instance.uiManager.SetNewGUIText(GameManager.Instance.uiManager._headerTxt,
        new Vector2(0, 5), "You win! Russians escaped!");
        IsRest = true;
    }

    private IEnumerator StartTimer(float time, BattleHandler function)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= time)
            {
                function();
            }
            yield return null;
        }
    }

    #endregion

    private void IncreaseComplexity()
    {
        if (_spawnRatio <= _minSpawnRatio)
            return;

        _spawnRatio -= _coplexityModifier;
    }

    #region Spawn

    private GameObject RandomEnemy()
    {
        GameObject selectedEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        return selectedEnemy;
    }

    private bool ReadyToSpawn()
    {
        _spawnerTimer += 1 * Time.deltaTime;
        if (_spawnerTimer >= _spawnRatio && !IsRest)
        {
            return true;
        }
        else
            return false;
    }

    private void SpawnEnemy(GameObject toSpawn)
    {
        Instantiate(toSpawn, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
        _spawnerTimer = 0;
        currentEnemiesInAction.Add(toSpawn.transform);
    }

    #endregion
}