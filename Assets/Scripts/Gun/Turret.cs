using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class Turret : ActionEntity
{
    private EnemySpawner _enemySpawner;

    private Gun _gun;

    private void Start()
    {
        _gun = GetComponent<Gun>();
        _enemySpawner = GameManager.Instance.battleManager.enemySpawner;
    }

    internal override void Action() => _gun.Shoot(closestEnemy.transform.localPosition);

    internal override void OnReachZoneEnter()
    {
        Action();
        CalculateRotation(closestEnemy.transform.localPosition);
    }

    private void CalculateRotation(Vector3 aimPos)
    {
        Vector3 aimDir = aimPos - transform.position;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
