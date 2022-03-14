using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    public Gun gun;

    private void Update()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gun.Shoot(aimPos);
        }

        Vector3 aimDirection = aimPos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        gun.handler.transform.rotation = Quaternion.Euler(0,0, angle);

    }

}
