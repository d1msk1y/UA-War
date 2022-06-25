using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSO : ScriptableObject
{
    public Sprite buildingSprite;
    public ParticleSystem buildFX;

    [Header("Parameters")]
    public int heightLimit = 0;
    public int health;
    public int price;
}