using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    private GameObject _selectedObject;
    private Vector3 _objectRotation;
    // {
    //     get
    //     {
    //         return this._objectRotation;
    //     }
    //     set
    //     {
    //         this._objectRotation = value;
    //         _cursor.transform.eulerAngles = this._objectRotation;
    //     }
    // }

    private float a
    {
        get { return a; }
        set { a = value; }
    }

    [SerializeField] private SpriteRenderer _cursor;
    private SpriteRenderer _selectedObjectSpriteRenderer;

    private void RotateSelectedItem()
    {
        _objectRotation = new Vector3(0, 0, _objectRotation.z + 90);
        _cursor.transform.eulerAngles = _objectRotation;
    }

    public void SelectItem(GameObject item)
    {
        _objectRotation = Vector3.zero;
        _selectedObject = item;
        _selectedObjectSpriteRenderer = _selectedObject.GetComponent<SpriteRenderer>();
    }

    public void SpawnItem()
    {
        if (_selectedObject == null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject item = Instantiate(_selectedObject, mousePos, Quaternion.Euler(_objectRotation));
        GameManager.instance.Astar.Scan();
        _cursor.gameObject.SetActive(false);

        _selectedObject = null;
    }


    private void Update()
    {
        if (_selectedObject != null)
        {
            SetCursor();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                SpawnItem();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                RotateSelectedItem();

        }
    }

    private void SetCursor()
    {
        _cursor.sprite = _selectedObjectSpriteRenderer.sprite;
        _cursor.gameObject.SetActive(true);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursor.transform.position = mousePos;
        _cursor.transform.localScale = _selectedObject.transform.localScale;
    }
}
