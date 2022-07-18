using UnityEngine;

public class ActionEntity : Entity
{
    public LayerMask targetMask;
    public new DamageBuildingSO BuildingParams
    {
        get => (DamageBuildingSO)base.BuildingParams;
        set => base.BuildingParams = (DamageBuildingSO)value;
    }

    private EntityScanner _entityScanner;
    internal EntityHealth closestEnemy;

    internal override void OnEnable()
    {
        base.OnEnable();
        _entityScanner = new EntityScanner(BuildingParams.attackRadius, targetMask, transform);
    }

    internal virtual void Action()
    {

    }

    internal virtual void OnReachZoneEnter()
    {

    }

    internal virtual void Update()
    {
        if (!TryGetClosestEnemy())
            return;

        if (Vector3.Distance(closestEnemy.transform.localPosition, transform.position) > BuildingParams.attackRadius)
            return;

        OnReachZoneEnter();
    }

    internal bool TryGetClosestEnemy()
    {
        closestEnemy = _entityScanner.GetClosestEntity(_entityScanner.GetEntitiesInRadius());
        if (GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Count > 0 && closestEnemy != null)
        {
            return true;
        }
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BuildingParams.attackRadius);
    }
}