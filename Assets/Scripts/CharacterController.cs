using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Actor
{
    public BaseController baseController;
    private Rigidbody2D _bodyRB;

    Gun.GunHandler onShoot;

    private void Start()
    {
        _bodyRB = body.GetComponent<Rigidbody2D>();
        gun.OnShootEvent += PushBody;

        onShoot = PushBody;
    }

    public void PushBody()
    {
        Debug.Log("Pushed");
        _bodyRB.AddForce(-body.transform.up * gun.gunConfig.recoilForce, ForceMode2D.Impulse);
    }

}
