using UnityEngine;

public class BuildingCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cursor;
    [SerializeField] private Color[] _colors;

    private PolygonCollider2D _collider;
    private Collider2D[] overlappingColliders;

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        overlappingColliders = new Collider2D[1];
    }

    public void SetCursor(BuildingStuff building)
    {
        _collider.points = building.GetComponent<PolygonCollider2D>().points;
        _cursor.sprite = building.buildingParams.buildingSprite;
        gameObject.SetActive(true);
        _cursor.transform.localScale = building.transform.localScale;
    }

    public void UpdatePosition(Vector2 position) => transform.position = (Vector2)position;
    public void HideCursor() => gameObject.SetActive(false);
    public void SetRotation(Vector3 rotation) => transform.eulerAngles = rotation;
    public void SetColor(int index) => _cursor.color = _colors[index];

    public bool CheckOverlapping(ContactFilter2D filter)
    {
        if (Physics2D.OverlapCollider(_collider, filter, overlappingColliders) > 0)
        {
            return true;
        }
        else return false;
    }
}