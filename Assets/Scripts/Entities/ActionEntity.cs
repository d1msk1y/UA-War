using UnityEngine;

public class ActionEntity : Entity
{
    private LayerMask _targetMask;
    public new DamageBuildingSO BuildingParams
    {
        get => (DamageBuildingSO)base.BuildingParams;
        set => base.BuildingParams = (DamageBuildingSO)value;
    }

    private EntityScanner _entityScanner;
    internal EntityHealth closestEntity;

    internal override void OnEnable()
    {
        base.OnEnable();
        _targetMask = BuildingParams.vulnerable;
        _entityScanner = new EntityScanner(BuildingParams.ActionRadius, _targetMask, transform);
    }

    public virtual void Action()
    {

    }

    internal virtual void OnReachZoneEnter()
    {

    }

    internal virtual void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.soundManager.PlaySoundEvent(BuildingParams.sfx);
    }

    internal virtual void Update()
    {
        if (!TryGetClosestEnemy())
            return;

        if (Vector3.Distance(closestEntity.transform.localPosition, transform.position) > BuildingParams.ActionRadius)
            return;

        OnReachZoneEnter();
    }

    internal bool TryGetClosestEnemy()
    {
        closestEntity = _entityScanner.GetClosestEntity(_entityScanner.GetEntitiesInRadius());
        if (GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Count > 0 && closestEntity != null)
        {
            return true;
        }
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BuildingParams.ActionRadius);
    }
}