using UnityEngine;

public class AttackBuilding : BuildingStuff, IAttack
{
    public new AttackBuildingSO BuildingParams
    {
        get => (AttackBuildingSO)base.BuildingParams;
        set => base.BuildingParams = (AttackBuildingSO)value;
    }

    public void Attack()
    {

    }
}