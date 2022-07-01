using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class BuildingStuff : MonoBehaviour
{
    [SerializeField] private BuildingSO _buildingParams;

    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider;

    public virtual BuildingSO BuildingParams
    {
        get => _buildingParams;
        set => _buildingParams = value;
    }

    [SerializeField] private EntityHealth _entityHealth;

    private void OnEnable()
    {
        SetBuilding();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void ScanAStar() => GameManager.Instance.ScanAStar();

    public void SetExtraHealth(float percent)
    {
        float extraHealth = _entityHealth.MaxHealth * percent;
        _entityHealth.MaxHealth += (int)extraHealth;
    }

    private void SetBuilding()
    {
        GameManager.Instance.ShakeScreen(15);

        Instantiate(BuildingParams.buildFX, transform.position, Quaternion.identity);
        _entityHealth.onDieEvent += ScanAStar;
        GameManager.Instance.ScanAStar();

        _entityHealth.MaxHealth = BuildingParams.health;
    }

    private void RevieveBuilding()
    {
        _polygonCollider.enabled = true;
        SetBuilding();
        StopCoroutine(DestroyBuilding());
    }

    private IEnumerator DestroyBuilding()
    {
        yield return new WaitForSeconds(_buildingParams.revievementTime);
        Destroy(gameObject);
    }

    private void BreakBuilding()
    {
        _spriteRenderer.sprite = _buildingParams.destroyedSprite;
        _polygonCollider.enabled = false;

        RevievementMenu revievementMenu =
        Instantiate(_buildingParams.revivementCanvas, transform.position, Quaternion.identity, transform);
        revievementMenu.SetButton(RevieveBuilding);
    }
}