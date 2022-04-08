using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]
public class UpgradeSO : ScriptableObject
{
    public int[] upgradeStages;

    public int price;
    public float priceMultiplier;
}
