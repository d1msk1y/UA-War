using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class Turret : AttackBuilding
{
    private float _shootingRadius;
    private EnemySpawner _enemySpawner;
    private Transform _closestEnemy;

    private Gun _gun;

    private void Start()
    {
        _gun = GetComponent<Gun>();
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
        if (GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Count > 0)
        {
            _closestEnemy = GetClosestEnemy();
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


    private Transform GetClosestEnemy()
    {
        Transform result = null;
        float num = float.PositiveInfinity;
        Vector3 position = transform.position;
        foreach (Transform gameObject in _enemySpawner.currentEnemiesInAction)
        {
            if (gameObject == null)
            {
                return null;
            }
            float num2 = Vector3.Distance(gameObject.transform.position, position);
            if (num2 < num)
            {
                result = gameObject.transform;
                num = num2;
            }
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _shootingRadius);
    }
}
