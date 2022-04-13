using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]
public class UpgradeSO : ScriptableObject
{
    public float[] upgradeStages;

    public int price;
    public float priceMultiplier;
}
