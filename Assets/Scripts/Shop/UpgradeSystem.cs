using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public void UpgradeBase(UpgradeButton upgradeButton)
    => BaseController.instance.GetComponent<EntityHealth>().MaxHealth = upgradeButton.UpgradeValue;

    public void UpgradeGun(UpgradeButton upgradeButton)
    => BaseController.instance.character.gun.damageMultiplier = upgradeButton.UpgradeValue;

    public void UpgradeAmmos(UpgradeButton upgradeButton)
    => BaseController.instance.GetComponent<EntityHealth>().MaxHealth = upgradeButton.UpgradeValue;
}
