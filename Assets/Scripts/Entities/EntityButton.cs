using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityButton : ShopButton
{
    [Header("Building parameters")]
    [SerializeField] private Entity _entity;

    internal override void Start()
    {
        _price = _entity.BuildingParams.price;
        base.Start();
    }

    public void SelectBuilding()
    {
        if (!ValidateTransaction())
            return;

        GameManager.Instance.buildingSystem.SelectBuilding(_entity);
    }
}