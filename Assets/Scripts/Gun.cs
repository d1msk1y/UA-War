using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public GunConfig gunConfig;
    public Transform firePos;
    public GameObject handler;
    public LayerMask targetMask;
    private LineRenderer _lineRenderer;

    private bool _itCanShoot = true;

    public delegate void OnShoot();
    public event OnShoot onShootEvent;

    private void Start()
    {        
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Shoot(Vector3 aimPos)
    {
        if (!_itCanShoot)
            return;
        onShootEvent.Invoke();
        Shooting(aimPos);        
        StartCoroutine(ShotFX(aimPos));
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        _itCanShoot = false;
        float timer = 0;
        while(timer < gunConfig.shootingRatio)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= gunConfig.shootingRatio)
                _itCanShoot = true;
            yield return null;
        }
    }

    private void Shooting(Vector3 aimPos)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(firePos.position, aimPos - transform.position, 10, targetMask);
        if (raycastHit.collider != null)
            GiveDamage(raycastHit.collider.gameObject, raycastHit);
    }

    private void GiveDamage(GameObject damagedEntity,RaycastHit2D raycastHit)
    {
        Debug.Log(raycastHit.collider.name + " hitted");
        damagedEntity.GetComponent<EntityHealth>().TakeDamage(gunConfig.damage);
    }

    private IEnumerator ShotFX(Vector3 linePos)
    {
        _lineRenderer.SetPosition(0, firePos.position);
        _lineRenderer.SetPosition(1, linePos);
        yield return new WaitForSeconds(0.025f);
        _lineRenderer.SetPosition(1, firePos.position);
        yield return null;
    }

}