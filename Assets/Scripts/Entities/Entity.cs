using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }

    [SerializeField] internal EntityHealth _entityHealth;
    private PolygonCollider2D _polygonCollider;

    [SerializeField] private EntitySO _buildingParams;
    public virtual EntitySO BuildingParams
    {
        get => _buildingParams;
        set => _buildingParams = value;
    }

    internal virtual void OnEnable()
    {
        SetEntity();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void ScanAStar() => GameManager.Instance.ScanAStar();

    public void SetEntity()
    {
        _entityHealth.onDieEvent += ScanAStar;
        _entityHealth.onDieEvent += DestroyEntity;
        _entityHealth.onDamageEvent += TakeDamage;
        GameManager.Instance.ShakeScreen(15);
        GameManager.Instance.ScanAStar();
        _entityHealth.MaxHealth = BuildingParams.health;
        AudioManager.Instance.PlaySoundEvent(_buildingParams.buildSFX);
        Instantiate(BuildingParams.buildFX, transform.position, Quaternion.identity);
        GameManager.Instance.sortLayerSetter.entities.Add(SpriteRenderer);
        SetExtraHealth(GameManager.Instance.buildingSystem.extraHealthPercent);
    }

    public void SetExtraHealth(float percent)
    {
        float extraHealth = _entityHealth.MaxHealth * percent;
        _entityHealth.MaxHealth += (int)extraHealth;
    }

    internal virtual void DestroyEntity()
    {
        GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.destroySFX);
        GameManager.Instance.sortLayerSetter.entities.Remove(SpriteRenderer);
        Destroy(gameObject);
    }

    private void RevieveEntity()
    {
        _polygonCollider.enabled = true;
        SetEntity();
    }

    private void TakeDamage() => GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.hitSFX);
}