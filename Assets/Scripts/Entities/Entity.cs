using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    private PolygonCollider2D _polygonCollider;

    [SerializeField] private EntitySO _buildingParams;
    public virtual EntitySO BuildingParams
    {
        get => _buildingParams;
        set => _buildingParams = value;
    }

    [SerializeField] internal EntityHealth _entityHealth;

    internal virtual void OnEnable()
    {
        SetEntity();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void ScanAStar() => GameManager.Instance.ScanAStar();

    public void SetExtraHealth(float percent)
    {
        float extraHealth = _entityHealth.MaxHealth * percent;
        _entityHealth.MaxHealth += (int)extraHealth;
    }

    private void CheckRotation()
    {
        for (int i = 0; i < BuildingParams.angles.Length; i++)
        {
            if (transform.rotation.z == BuildingParams.angles[i])
            {
                if (i >= BuildingParams.sprites.Length)
                    i = BuildingParams.sprites.Rank;
                spriteRenderer.sprite = BuildingParams.sprites[i];
            }
        }
    }

    public void SetEntity()
    {
        GameManager.Instance.ShakeScreen(15);
        AudioManager.Instance.PlaySoundEvent(_buildingParams.buildSFX);

        Instantiate(BuildingParams.buildFX, transform.position, Quaternion.identity);
        _entityHealth.onDieEvent += ScanAStar;
        _entityHealth.onDieEvent += DestroyEntity;
        _entityHealth.onDamageEvent += TakeDamage;
        GameManager.Instance.sortLayerSetter.entities.Add(spriteRenderer);
        GameManager.Instance.ScanAStar();
        CheckRotation();

        _entityHealth.MaxHealth = BuildingParams.health;
        SetExtraHealth(GameManager.Instance.buildingSystem.extraHealthPercent);
    }

    private void RevieveEntity()
    {
        _polygonCollider.enabled = true;
        SetEntity();
    }

    private void TakeDamage() => GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.hitSFX);

    internal virtual void DestroyEntity()
    {
        GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.destroySFX);
        GameManager.Instance.sortLayerSetter.entities.Remove(spriteRenderer);
        Destroy(gameObject);
    }
}