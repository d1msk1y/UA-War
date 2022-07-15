using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private PolygonCollider2D _polygonCollider;

    [SerializeField] private EntitySO _buildingParams;
    public virtual EntitySO BuildingParams
    {
        get => _buildingParams;
        set => _buildingParams = value;
    }

    [SerializeField] private EntityHealth _entityHealth;

    internal virtual void OnEnable()
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

    public void SetBuilding()
    {
        GameManager.Instance.ShakeScreen(15);
        AudioManager.Instance.PlaySoundEvent(_buildingParams.buildSFX);

        Instantiate(BuildingParams.buildFX, transform.position, Quaternion.identity);
        _entityHealth.onDieEvent += ScanAStar;
        _entityHealth.onDieEvent += BreakBuilding;
        _entityHealth.onDamageEvent += TakeDamage;
        GameManager.Instance.ScanAStar();
        CheckRotation();

        _entityHealth.MaxHealth = BuildingParams.health;
        SetExtraHealth(GameManager.Instance.buildingSystem.extraHealthPercent);
    }

    private void RevieveBuilding()
    {
        _polygonCollider.enabled = true;
        SetBuilding();
        StopCoroutine(DestroyBuilding());
    }

    private void TakeDamage() => GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.hitSFX);

    private IEnumerator DestroyBuilding()
    {
        yield return new WaitForSeconds(_buildingParams.revievementTime);
        Destroy(gameObject);
    }

    internal virtual void BreakBuilding()
    {
        GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.destroySFX);
        StartCoroutine(DestroyBuilding());
    }
}