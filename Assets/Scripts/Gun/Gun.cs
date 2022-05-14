using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GunFxSetter))]
public class Gun : MonoBehaviour
{
    [Header("Main references & parts")]
    public GunSO gunConfig;
    public Transform firePos;
    private GunFxSetter gunFxSetter;

    [Header("Ammos")]
    public float extraAmmosPercent;
    private int _ammos;
    public float damageMultiplier;
    public int Ammos
    {
        get { return _ammos; }
        set
        {
            _ammos = value;
            if (_ammos <= 0)
            {
                _itCanShoot = false;
                StartCoroutine(Reload());
            }
            else if (_ammos > 0) _itCanShoot = true;
        }
    }

    [Header("Other")]
    [HideInInspector] public LayerMask targetMask;
    [HideInInspector] public RaycastHit2D gunRaycast;

    private bool _itCanShoot = true;

    public delegate void GunHandler();
    public event GunHandler OnShootEvent;

    private void Start()
    {
        SetGun();
    }

    private void SetGun()
    {
        Ammos = gunConfig.ammos;
        gunFxSetter = GetComponent<GunFxSetter>();
    }

    private void CreateParticle(ParticleSystem particleSystem)
    => Instantiate(particleSystem, gunRaycast.point, Quaternion.identity);

    private int ExtraAmmos()
    {
        float extraAmmos = gunConfig.ammos;
        extraAmmos -= extraAmmos * extraAmmosPercent;

        return (int)-extraAmmos;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunConfig.reloadingTime);
        Ammos = gunConfig.ammos + ExtraAmmos();
    }

    public void Shoot(Vector3 aimPos)
    {
        if (!_itCanShoot)
            return;
        Ammos -= 1;
        gunRaycast = Physics2D.Raycast(firePos.position, aimPos - transform.position, 20, targetMask);

        OnShootEvent?.Invoke();
        StartCoroutine(gunFxSetter.ShotFX(gunFxSetter.SetVFX(aimPos)));
        ShootingRaycast(gunRaycast);
        StartCoroutine(StartShootingTimer());
    }

    private IEnumerator StartShootingTimer()
    {
        _itCanShoot = false;
        float timer = 0;
        while (timer < gunConfig.shootingRatio)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= gunConfig.shootingRatio && Ammos > 0)
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
        target.TakeDamage((int)(gunConfig.damage * damageMultiplier));
        CreateParticle(target.hitParticle);
    }
}