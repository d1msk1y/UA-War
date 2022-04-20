using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    [Header("Parameters")]
    public float extraHealthPercent = 1;

    [Header("GFX")]
    [SerializeField] private ParticleSystem _buildFX;
    [SerializeField] private SpriteRenderer _cursor;
    private SpriteRenderer _selectedObjectSpriteRenderer;

    public BuildingSO selectedObject;
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
        _scoreSystem = GameManager.Instance.scoreSystem;
    }

    private void Update()
    {
        if (selectedObject != null)
        {
            SetCursor();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Build();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                RotateSelectedItem();

        }
    }

    private void RotateSelectedItem() => ObjectRotation = new Vector3(0, 0, _objectRotation.z + 90);

    public void SelectItem(BuildingSO item)
    {
        if (!_scoreSystem.WithdrawCoins(item.price))
            return;

        selectedObject = item;
        _selectedObjectSpriteRenderer = selectedObject.building.GetComponent<SpriteRenderer>();
        GameManager.Instance.shopManager.ToggleShop();
    }

    public void Build()
    {
        if (selectedObject == null)
            return;
        SpawnItem();
        _cursor.gameObject.SetActive(false);
        GameManager.Instance.shopManager.ToggleShop();
    }

    private void SpawnItem()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform item = Instantiate(selectedObject.building, mousePos, Quaternion.Euler(ObjectRotation));
        Instantiate(_buildFX, item.transform.position, Quaternion.identity);
        GameManager.Instance.Astar.Scan();
        selectedObject = null;
    }

    private void SetBuilding()
    {
        EntityHealth entityHealth = selectedObject.building.GetComponent<EntityHealth>();
        int extraHealth = ExtraHealth(entityHealth);
        entityHealth.MaxHealth += extraHealth;
    }

    private int ExtraHealth(EntityHealth entityHealth)
    {
        float extraHealth = entityHealth.MaxHealth * extraHealthPercent;

        return (int)extraHealth;
    }

    private void SetCursor()
    {
        _cursor.sprite = _selectedObjectSpriteRenderer.sprite;
        _cursor.gameObject.SetActive(true);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursor.transform.position = mousePos;
        _cursor.transform.localScale = selectedObject.building.transform.localScale;
    }
}