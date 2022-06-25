using UnityEngine;

public class AttackBuilding : BuildingStuff
{
    public new AttackBuildingSO BuildingParams
    {
        get => (AttackBuildingSO)base.BuildingParams;
        set => base.BuildingParams = (AttackBuildingSO)value;
    }
}