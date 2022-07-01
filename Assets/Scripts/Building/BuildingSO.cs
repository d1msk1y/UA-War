using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSO : ScriptableObject
{
    [Header("GFX")]
    public Sprite buildingSprite;
    public Sprite destroyedSprite;
    public ParticleSystem buildFX;

    [Header("Extra parts")]
    public RevievementMenu revivementCanvas;

    [Header("Parameters")]
    public int heightLimit = 0;
    public float revievementTime = 5;
    public int health;
    public int price;
}