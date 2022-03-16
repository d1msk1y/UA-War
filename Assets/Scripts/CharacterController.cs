using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Actor
{
    public BaseController baseController;
    private Rigidbody2D _bodyRB;

    private void Start()
    {
        _bodyRB = body.GetComponent<Rigidbody2D>();
        gun.onShootEvent += PushBody;
    }

    public void PushBody()
    {
        Debug.Log("Pushed");
        _bodyRB.AddForce(-body.transform.up * gun.gunConfig.recoilForce, ForceMode2D.Impulse);
    }

}
