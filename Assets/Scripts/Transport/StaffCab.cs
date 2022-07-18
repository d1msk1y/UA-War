using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffCab : Transport
{
    [Header("Other")]
    [SerializeField] public GameObject soldiersGfx;

    [Header("Spawn")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy[] _enemies;

    private bool _canDeploy = true;

    private void Update()
    {
        if (aIPath.reachedEndOfPath && _canDeploy)
        {
            DeployEnemies();
        }
    }

    private void DeployEnemies()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            Instantiate(GetRandomEnemy(), spawnPoint.position, Quaternion.identity);
        }
        Destroy(soldiersGfx);

        Escape();
        _canDeploy = false;
    }

    private Enemy GetRandomEnemy() => _enemies[Random.Range(0, _enemies.Length)];
}