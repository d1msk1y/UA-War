using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private GameObject _selectedObject;
    public Transform spawnPos;

    public void SelectItem(GameObject item) => _selectedObject = item;

    public void InstantiateObject()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y));
        Instantiate(_selectedObject, position, Quaternion.identity);
        _selectedObject = null;
    }
}
