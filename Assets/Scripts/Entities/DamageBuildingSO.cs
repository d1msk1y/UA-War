using UnityEngine;
using FMODUnity;

[CreateAssetMenu()]
public class DamageBuildingSO : EntitySO
{
    public float ActionRadius;
    public LayerMask vulnerable;

    [Header("SFX")]
    public EventReference sfx;
}