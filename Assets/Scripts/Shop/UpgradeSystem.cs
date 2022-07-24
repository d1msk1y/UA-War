using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{

    public void UpgradeBase(UpgradeButton upgradeButton)
    => BaseController.instance.entityHealth.MaxHealth = (int)upgradeButton.UpgradeValue;

    public void UpgradeShop(UpgradeButton upgradeButton)
    => GameManager.Instance.scoreSystem.moneyMultiplier = (int)upgradeButton.UpgradeValue;

    public void UpgradeGun(UpgradeButton upgradeButton)
    => BaseController.instance.gun.damageMultiplier = upgradeButton.UpgradeValue;

    public void UpgradeAmmos(UpgradeButton upgradeButton)
    => BaseController.instance.gun.extraAmmosPercent = upgradeButton.UpgradeValue;

    public void UpgradeBuildings(UpgradeButton upgradeButton)
    => GameManager.Instance.buildingSystem.extraHealthPercent = upgradeButton.UpgradeValue;
}