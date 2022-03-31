using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    [Header("GFX")]
    [SerializeField] private ParticleSystem _buildFX;
    [SerializeField] private SpriteRenderer _cursor;
    private SpriteRenderer _selectedObjectSpriteRenderer;

    public BuildingStuff selectedObject;
    private ScoreSystem _scoreSystem;

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
            _cursor.transform.eulerAngles = _objectRotation;
        }
    }

    private void Start()
    {
        _scoreSystem = GameManager.instance.scoreSystem;
    }

    private void RotateSelectedItem() => ObjectRotation = new Vector3(0, 0, _objectRotation.z + 90);

    public void SelectItem(BuildingStuff item)
    {
        if (!_scoreSystem.WithdrawCoins(item.price))
            return;

        selectedObject = item;
        _selectedObjectSpriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
    }

    public void SpawnItem()
    {
        if (selectedObject == null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        BuildingStuff item = Instantiate(selectedObject, mousePos, Quaternion.Euler(ObjectRotation));
        Instantiate(_buildFX, item.transform.position, Quaternion.identity);
        GameManager.instance.Astar.Scan();
        _cursor.gameObject.SetActive(false);

        selectedObject = null;
    }

    private void Update()
    {
        if (selectedObject != null)
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
        _cursor.transform.localScale = selectedObject.transform.localScale;
    }
}