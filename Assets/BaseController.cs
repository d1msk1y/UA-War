using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    public Gun gun;
    public LayerMask targetMask;

    private void Start()
    {
        gun.targetMask = targetMask;
    }

    private void Update()
    {
        Aiming();
        ShootingCheck();
    }

    private void ShootingCheck()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gun.Shoot(aimPos);
        }
    }

    private void Aiming()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = aimPos - transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        gun.handler.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


}
