using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [Header("Parameters")]
    public ContactFilter2D overlappingFilter;
    public float extraHealthPercent = 1;

    [Header("GFX")]
    [SerializeField] private BuildingCursor _cursor;

    public BuildingStuff selectedObject;
    public BuildingStuff SelectedObject
    {
        get
        {
            return selectedObject;
        }
        set
        {
            selectedObject = value;
            if (selectedObject == null)
                return;
        }
    }
    private Vector3 _objectRotation;
    private Vector3 ObjectRotation
    {
        get
        {
            return _objectRotation;
        }
        set
        {
            _objectRotation = value;
            _cursor.SetRotation(value);
        }
    }

    private void Update()
    {
        if (SelectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Build();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                RotateSelectedItem();
            _cursor.UpdatePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void FixedUpdate()
    {
        IsAbleToSpawn();
    }

    private void RotateSelectedItem() => ObjectRotation = new Vector3(0, 0, _objectRotation.z + 90);

    public void SelectItem(BuildingStuff item)
    {
        if (!GameManager.Instance.scoreSystem.WithdrawCoins(item.buildingParams.price))
            return;

        SelectedObject = item;
        _cursor.SetCursor(item);
        GameManager.Instance.shopManager.ToggleShop();
    }

    public void Build()
    {
        if (!IsAbleToSpawn())
        {

            return;
        }
        SpawnItem();
        _cursor.HideCursor();
        GameManager.Instance.shopManager.ToggleShop();
    }

    private void SpawnItem()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(SelectedObject, mousePos, Quaternion.Euler(ObjectRotation));
        SetBuilding();
        SelectedObject = null;
    }

    private bool IsAbleToSpawn()
    {
        bool abilityBool = _cursor.transform.position.y > 0 || SelectedObject == null;
        if (abilityBool || _cursor.CheckOverlapping(overlappingFilter))
        {
            _cursor.SetColor(0);
            return false;
        }
        else
        {
            _cursor.SetColor(1);
            return true;
        }
    }

    private void SetBuilding() => SelectedObject.SetExtraHealth(extraHealthPercent);
}