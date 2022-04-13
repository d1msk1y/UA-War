using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Rounds")]
    [SerializeField] private int _currentRound;
    [SerializeField] private int _roundTime;
    [SerializeField] private int _restTime;
    [SerializeField] private bool _isRest;

    [Header("Spawn")]
    [SerializeField] private GameObject[] enemiesToSpawn;
    public List<Transform> currentEnemiesInAction;
    [SerializeField] private Transform[] spawners;
    [SerializeField] private float spawnRatio;
    private float _spawnerTimer;

    private delegate void BattleHandler();

    private void Start()
    {
        StartRound();
        currentEnemiesInAction = new List<Transform>();
    }

    private void Update()
    {
        if (ReadyToSpawn())
        {
            SpawnEnemy(RandomEnemy());
        }
    }

    #region  Rounds

    public void StartRound()
    {
        _isRest = false;
        StartCoroutine(StartTimer(_roundTime, StopRound));
    }

    private void StopRound()
    {
        _currentRound++;
        if (spawnRatio < 1)
            spawnRatio -= 0.1f;
        _isRest = true;
        StartCoroutine(StartTimer(_restTime, StartRound));
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

    #region Spawn

    private GameObject RandomEnemy()
    {
        GameObject selectedEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        return selectedEnemy;
    }

    private bool ReadyToSpawn()
    {
        _spawnerTimer += 1 * Time.deltaTime;
        if (_spawnerTimer >= spawnRatio && !_isRest)
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