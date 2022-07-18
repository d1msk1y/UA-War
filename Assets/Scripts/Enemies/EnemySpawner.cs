using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    public List<Transform> currentEnemiesInAction;
    public Transform[] spawners;
    public Transform escapePoint;
    private float _spawnerTimer;
    [SerializeField] private float _spawnRatio;

    [Header("Chances")]
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private int[] rarenessChance = { 40, 30, 20, 10 };

    [Header("Complexity")]
    [SerializeField] private float _coplexityModifier;
    [SerializeField] private float _minSpawnRatio;

    private void Start()
    {
        currentEnemiesInAction = new List<Transform>();
    }

    private void Update()
    {
        if (ReadyToSpawn())
        {
            SpawnEnemy(GetRandomEnemy());
        }
    }

    #region  Complexity
    public void IncreaseSpawnRatio()
    {
        if (_spawnRatio <= _minSpawnRatio)
            return;

        _spawnRatio -= _coplexityModifier;
    }

    private bool ReadyToSpawn()
    {
        _spawnerTimer += 1 * Time.deltaTime;
        if (_spawnerTimer >= _spawnRatio && !GameManager.Instance.battleManager.IsRest)
        {
            return true;
        }
        else if (GameManager.Instance.battleManager.IsRest)
            return false;
        else
            return false;
    }
    #endregion

    #region Raindomizer
    private GameObject GetRandomEnemy()
    {
        GameObject selectedEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        int chance = Random.Range(0, CalculateTotalChance());

        for (int i = 0; i < rarenessChance.Length; i++)
        {
            if (chance <= rarenessChance[i])
            {
                selectedEnemy = enemiesToSpawn[i];
                return selectedEnemy;
            }
            else chance -= rarenessChance[i];
        }
        return selectedEnemy;
    }

    private int CalculateTotalChance()
    {
        int calculatedTotalChance = 0;
        foreach (int item in rarenessChance)
        {
            calculatedTotalChance += item;
        }
        return calculatedTotalChance;
    }
    #endregion

    private void SpawnEnemy(GameObject toSpawn)
    {
        Vector3 spawnPos = spawners[Random.Range(0, spawners.Length)].position;
        GameObject spawnedObject = Instantiate(toSpawn, spawnPos, Quaternion.identity);
        _spawnerTimer = 0;
        currentEnemiesInAction.Add(spawnedObject.transform);
    }
}