using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class BuildingStuff : MonoBehaviour
{
    [SerializeField] private BuildingSO _buildingParams;

    public virtual BuildingSO BuildingParams
    {
        get => _buildingParams;
        set => _buildingParams = value;
    }

    [SerializeField] private EntityHealth _entityHealth;

    private void OnEnable()
    {
        Instantiate(BuildingParams.buildFX, transform.position, Quaternion.identity);
        _entityHealth.onDieEvent += ScanAStar;
        GameManager.Instance.Astar.Scan();

        _entityHealth.MaxHealth = BuildingParams.health;
    }

    private void ScanAStar() => GameManager.Instance.Astar.Scan();

    public void SetExtraHealth(float percent)
    {
        float extraHealth = _entityHealth.MaxHealth * percent;
        _entityHealth.MaxHealth += (int)extraHealth;
    }
}