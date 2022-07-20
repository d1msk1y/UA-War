using UnityEngine;

public class Soldier : Enemy
{
    private RaycastHit2D gunRaycast;
    public Gun gun;
    private void GetRaycastHit() => gunRaycast = gun.gunRaycast;

    private new void Start()
    {
        base.Start();
        gun.OnShootEvent += GetRaycastHit;
        gun.targetMask = enemyParams.targetMask;
        Debug.Log(BaseController.instance.transform);
        DestinationTarget = BaseController.instance.transform;

        Attack = Shoot;

        SetDestractionTarget();
    }

    private void Shoot()
    {
        if (destractionTarget != null)
            gun.Shoot(destractionTarget.transform.position);
    }

    internal override void Escape()
    {
        base.Escape();
        Disarm();
    }

    private void Disarm() => gun.Ammos = 0;
}