using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu()]
public class EntitySO : ScriptableObject
{
    [Header("SFX")]
    public EventReference buildSFX;
    public EventReference destroySFX;
    public EventReference hitSFX;

    [Header("Rotation")]
    public Sprite[] sprites;
    public int[] angles;

    [Header("GFX")]
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