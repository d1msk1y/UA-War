using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Actor character;
    public LayerMask targetMask;

    public static BaseController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        character.gun.targetMask = targetMask;        
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
            character.gun.Shoot(aimPos);
        }
    }

    private void Aiming()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = aimPos - character.transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        //float delayAngle = BodyAiming(angle);
        float delayAngle = Mathf.Lerp(character.body.transform.rotation.z, angle, 2f);
        character.body.transform.rotation = Quaternion.Euler(0, 0, delayAngle);
    }

    private float BodyAiming(float angle)
    {
        float calculatedAngle =  Mathf.Lerp(character.body.transform.rotation.z, angle, 0.1f);
        return calculatedAngle;
    }


}
