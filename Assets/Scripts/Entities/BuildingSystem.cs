using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [Header("Parameters")]
    public ContactFilter2D overlappingFilter;
    public float extraHealthPercent = 1;

    private int _rotationIndex;
    private Vector3 _objectRotation;
    public Entity _selectedObject;

    [Header("GFX")]
    [SerializeField] private EntityCursor _cursor;

    public delegate void BuildingSystemHandler();
    public event BuildingSystemHandler onBuild;

    #region Properties

    public int RotationIndex
    {
        get => _rotationIndex;
        set
        {
            if (value >= SelectedObject.BuildingParams.angles.Length)
            {
                _rotationIndex = 0;
            }
            else
            {
                _rotationIndex = value;
            }
        }
    }
    public Entity SelectedObject
    {
        get => _selectedObject;
        set
        {
            _selectedObject = value;
            if (_selectedObject == null)
                return;
        }
    }
    private Vector3 ObjectRotation
    {
        get => _objectRotation;
        set
        {
            _objectRotation = value;
            _cursor.SetRotation(value);
        }
    }

    #endregion

    private void Update()
    {
        if (SelectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Build();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                RotateSelectedItem();
            _cursor.UpdateCursor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void FixedUpdate()
    {
        IsAbleToSpawn();
    }

    public void SelectBuilding(Entity item)
    {
        SelectedObject = item;
        RotationIndex = 0;
        _cursor.SetCursor(item);
        GameManager.Instance.shopManager.ToggleShop();
    }

    private void RotateSelectedItem()
    {
        RotationIndex++;
        ObjectRotation = new Vector3(0, 0, SelectedObject.BuildingParams.angles[RotationIndex]);
    }


    private void Build()
    {
        if (!IsAbleToSpawn())
        {
            return;
        }
        _cursor.HideCursor();
        GameManager.Instance.shopManager.ToggleShop();
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Entity spawnedObj = Instantiate(SelectedObject, mousePos, Quaternion.Euler(ObjectRotation));
        // spawnedObj.spriteRenderer.sprite = SelectedObject.BuildingParams.sprites[RotationIndex];

        yield return new WaitForSeconds(0.1f);
        SelectedObject = null;
    }

    private bool IsAbleToSpawn()
    {
        if (SelectedObject == null)
            return false;
        bool abilityBool = _cursor.transform.position.y > SelectedObject.BuildingParams.heightLimit;
        if (abilityBool || _cursor.CheckOverlapping(overlappingFilter))
        {
            _cursor.SetColor(0);//0 = false/red
            return false;
        }
        else
        {
            _cursor.SetColor(1);// 1 = true/green
            return true;
        }
    }
}