using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class BuildingStuff : MonoBehaviour
{
    public BuildingSO buildingParams;
    [SerializeField] private EntityHealth _entityHealth;

    private void OnEnable()
    {
        Instantiate(buildingParams.buildFX, transform.position, Quaternion.identity);
        _entityHealth.onDieEvent += ScanAStar;
        GameManager.Instance.Astar.Scan();

        _entityHealth.MaxHealth = buildingParams.health;
    }

    private void ScanAStar() => GameManager.Instance.Astar.Scan();

    public void SetExtraHealth(float percent)
    {
        float extraHealth = _entityHealth.MaxHealth * percent;
        _entityHealth.MaxHealth += (int)extraHealth;
    }
}