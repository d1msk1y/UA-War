using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class Turret : MonoBehaviour
{
    [SerializeField] float _shootingRadius;

    private Gun _gun;

    private void Start()
    {
        _gun = GetComponent<Gun>();
    }

    private void Update()
    {
        if (Vector3.Distance(GetClosestEnemy
        (GameManager.Instance.battleManager.currentEnemiesInAction).position, transform.position) > _shootingRadius)
            return;

        Aiming();
        Shoot();
    }

    private void Shoot() => _gun.Shoot(GetClosestEnemy(GameManager.Instance.battleManager.currentEnemiesInAction).position);

    private void Aiming()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint
        (GetClosestEnemy(GameManager.Instance.battleManager.currentEnemiesInAction).position);
        Vector3 aimDir = aimPos - transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        float delayAngle = Mathf.Lerp(transform.rotation.z, angle, 2f);
    }


    private Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform result = null;
        float num = float.PositiveInfinity;
        Vector3 position = transform.position;
        foreach (Transform gameObject in enemies)
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
        Debug.Log(result.name);
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _shootingRadius);
    }
}
