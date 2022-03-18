using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    [Header("Spawn")]
    public GameObject[] enemiesToSpawn;
    public Transform[] spawners;
    [SerializeField] private float spawnRatio;

    private float timer;

    private void ResetTimer() => timer = 0;

    private GameObject RandomEnemy()
    {
        GameObject selectedEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        return selectedEnemy;
    }

    private void Update()
    {
        if(ReadyToSpawn())
            SpawnEnemy(RandomEnemy());
    }

    private bool ReadyToSpawn()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= spawnRatio)
        {
            return true;
        }
        else
            return false;
    }

    private void SpawnEnemy(GameObject toSpawn)
    {
        Instantiate(toSpawn, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
        ResetTimer();
    }

}
