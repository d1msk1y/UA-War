using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [Header("Main references & parts")]
    public GunConfig gunConfig;
    public Transform firePos;
    public SpriteRenderer shotFx;

    [Header("Other")]
    [Tooltip("By this mask gun is detecting object which are should be shoted and what are not")]
    public LayerMask targetMask;
    [HideInInspector] public RaycastHit2D gunRaycast;

    private LineRenderer _lineRenderer;
    private bool _itCanShoot = true;

    public delegate void GunHandler();
    public event GunHandler OnShootEvent;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Shoot(Vector3 aimPos)
    {
        if (!_itCanShoot)
            return;

        gunRaycast = Physics2D.Raycast(firePos.position, aimPos - transform.position, 20, targetMask);

        Vector2 hitPos = Vector2.zero;
        if (gunRaycast.collider == null)
            hitPos = firePos.position + (aimPos - transform.position) * 30;
        else
            hitPos = gunRaycast.point;

        OnShootEvent?.Invoke();
        ShootingRaycast(gunRaycast);
        StartCoroutine(ShotFX(hitPos));
        StartCoroutine(StartShootingTimer());
    }

    private IEnumerator StartShootingTimer()
    {
        _itCanShoot = false;
        float timer = 0;
        while (timer < gunConfig.shootingRatio)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= gunConfig.shootingRatio)
                _itCanShoot = true;
            yield return null;
        }
    }

    private void ShootingRaycast(RaycastHit2D raycastHit)
    {
        if (raycastHit.collider != null)
            GiveDamage(raycastHit.collider.gameObject, raycastHit);
    }

    private void GiveDamage(GameObject damagedEntity, RaycastHit2D raycastHit)
    {
        EntityHealth target = damagedEntity.GetComponent<EntityHealth>();
        if (target == null)
            return;
        target.TakeDamage(gunConfig.damage);
        CreateParticle(target.hitParticle);
    }

    private void CreateParticle(ParticleSystem particleSystem) =>
     Instantiate(particleSystem, gunRaycast.point, Quaternion.identity);

    private IEnumerator ShotFX(Vector3 linePos)
    {
        _lineRenderer.SetPosition(0, firePos.position);
        _lineRenderer.SetPosition(1, linePos);

        shotFx.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.025f);

        shotFx.gameObject.SetActive(false);

        _lineRenderer.SetPosition(1, firePos.position);
        yield return null;
    }
}