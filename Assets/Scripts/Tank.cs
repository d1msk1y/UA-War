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

    [Header("Optional parts")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _target;

    private bool _itCanShoot;

    private new void Start()
    {
        base.Start();
        _target = BaseController.instance.transform;
        SetDestination(_target);
        StartMove();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _target.position) <= _shootDistance)
        {
            _turret.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(_target));
            Shoot();
        }

        if (Vector2.Distance(transform.position, _target.position) <= aIPath.slowdownDistance)
            StopMove();

        if (aIPath.reachedDestination)
            GameManager.Instance.ScanAStar();
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }

    private void Shoot()
    {
        if (!_itCanShoot)
            return;
        Instantiate(_bulletPrefab, _firepos.position, Quaternion.identity);
        StartCoroutine(CheckShootAbility());
    }

    private IEnumerator CheckShootAbility()
    {
        _itCanShoot = false;
        float timer = 0;
        while (timer < _shootRatio)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= _shootRatio)
                _itCanShoot = true;
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _shootDistance);
        Gizmos.color = Color.red;
    }

}