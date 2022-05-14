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
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private float _spawnRatio;

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
            SpawnEnemy(RandomEnemy());
        }
    }

    private GameObject RandomEnemy()
    {
        GameObject selectedEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        return selectedEnemy;
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

    public void IncreaseSpawnRatio()
    {
        if (_spawnRatio <= _minSpawnRatio)
            return;

        _spawnRatio -= _coplexityModifier;
    }

    private void SpawnEnemy(GameObject toSpawn)
    {
        Vector3 spawnPos = spawners[Random.Range(0, spawners.Length)].position;
        GameObject spawnedObject = Instantiate(toSpawn, spawnPos, Quaternion.identity);
        _spawnerTimer = 0;
        currentEnemiesInAction.Add(spawnedObject.transform);
    }
}