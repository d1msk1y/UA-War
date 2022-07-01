using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class Turret : AttackBuilding
{
    private float _shootingRadius;
    private EnemySpawner _enemySpawner;
    private EntityHealth _closestEnemy;

    private EntityScanner _entityScanner;

    private Gun _gun;

    private void Start()
    {
        _gun = GetComponent<Gun>();
        _entityScanner = new EntityScanner(_shootingRadius, BaseController.instance.targetMask, transform);
        _enemySpawner = GameManager.Instance.battleManager.enemySpawner;
        _shootingRadius = BuildingParams.radius;
    }

    private void Update()
    {
        if (!TryGetClosestEnemy())
            return;

        if (Vector3.Distance(_closestEnemy.transform.localPosition, transform.position) > _shootingRadius)
            return;

        Aiming();
        Shoot();
    }

    private void Shoot() => _gun.Shoot(_closestEnemy.transform.localPosition);

    private bool TryGetClosestEnemy()
    {
        _closestEnemy = _entityScanner.GetClosestEntity(_entityScanner.GetEntitiesInRadius());
        if (GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Count > 0 && _closestEnemy != null)
        {
            return true;
        }
        else return false;
    }

    private void Aiming()
    {
        Vector3 aimPos = _closestEnemy.transform.localPosition;
        Vector3 aimDir = aimPos - transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _shootingRadius);
    }
}
