using UnityEngine;

[RequireComponent(typeof(AttackRangeSetter))]
public class BuildingCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cursor;
    [SerializeField] private Color[] _colors;

    private AttackRangeSetter _attackRangeSetter;
    private PolygonCollider2D _collider;
    private Collider2D[] overlappingColliders;

    private void Start()
    {
        _collider = GetComponentInChildren<PolygonCollider2D>();
        _attackRangeSetter = GetComponent<AttackRangeSetter>();
        overlappingColliders = new Collider2D[1];
    }

    public void UpdatePosition(Vector2 position) => transform.position = (Vector2)position;
    public void SetRotation(Vector3 rotation) => transform.eulerAngles = rotation;
    public void SetColor(int index) => _cursor.color = _colors[index];

    public void SetCursor(BuildingStuff building)
    {
        _collider.points = building.GetComponent<PolygonCollider2D>().points;
        _cursor.sprite = building.BuildingParams.buildingSprite;
        gameObject.SetActive(true);
        _cursor.transform.localScale = building.transform.localScale;
        DisplayRange(building);
    }

    public void DisplayRange(BuildingStuff buildingStuff)
    {
        if (buildingStuff.TryGetComponent(out AttackBuilding attackBuilding))
        {
            _attackRangeSetter.DisplayRange(attackBuilding.BuildingParams.radius);
        }
    }

    public void HideCursor()
    {
        gameObject.SetActive(false);
        _attackRangeSetter.HideRange();
    }


    public bool CheckOverlapping(ContactFilter2D filter)
    {
        if (Physics2D.OverlapCollider(_collider, filter, overlappingColliders) > 0)
            return true;
        else return false;
    }
}