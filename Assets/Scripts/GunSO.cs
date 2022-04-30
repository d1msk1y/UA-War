using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GunSO : ScriptableObject
{
    [Header("GFX")]
    public Sprite shotGFX;

    [Header("Parameters")]
    public int damage;
    public int ammos;
    public float reloadingTime;
    public float shootingRatio;
    public float recoilForce;
}
