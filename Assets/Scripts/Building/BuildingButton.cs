using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingButton : ShopButton
{
    [Header("Building parameters")]
    [SerializeField] private BuildingStuff _building;
    private BuildingSO _buildingSO;

    internal override void Start()
    {
        _buildingSO = _building.BuildingParams;
        _price = _buildingSO.price;
        base.Start();
    }

    public void SelectBuilding()
    {
        if (!ValidateTransaction())
            return;

        GameManager.Instance.buildingSystem.SelectBuilding(_building);
    }
}