using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Transport
{
    [Header("Tank parameters")]
    [SerializeField] private int _damage;
    [SerializeField] private int _shootRatio;
    [SerializeField] private float _shootDistance;

    [Header("Tank parts")]
    [SerializeField] private GameObject _turret;
    [SerializeField] private GameObject _body;
    [SerializeField] private Transform _firepos;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _target;

    private new void Start()
    {
        base.Start();
        SetDestination(_target);
        StartMove();
    }

    private void Update()
    {
        _body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(_target));

        if (Vector2.Distance(transform.position, _target.position) <= _shootDistance)
        {
            _turret.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(_target));
            Shoot();
        }
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firepos.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _shootDistance);
        Gizmos.color = Color.red;
    }
}