using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu()]
public class GunSO : ScriptableObject
{
    [Header("GFX")]
    public Sprite shotGFX;

    [Header("SFX")]
    public EventReference shot;
    public EventReference noAmmo;
    public EventReference reloading;

    [Header("Parameters")]
    public int damage;
    public int ammos;
    public float reloadingTime;
    public float shootingRatio;
    public float recoilForce;
}
