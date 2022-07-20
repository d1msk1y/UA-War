using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIDestinationSetter))]
public class Enemy : Actor
{
    [Header("Parameters")]
    [SerializeField] internal EnemySO enemyParams;

    [Header("Base References")]
    [SerializeField] internal AIPath aIPath;
    internal AIDestinationSetter aIDestinationSetter;
    private EntityScanner _entityScanner;
    private SpriteRenderer _spriteRenderer;

    [Header("AI Targets")]
    internal EntityHealth destractionTarget;
    internal Transform DestinationTarget { get => aIDestinationSetter.target; set => aIDestinationSetter.target = value; }

    [Header("Extra references")]
    [SerializeField] private Sprite _escapingBody;
    [SerializeField] internal GameObject deadBody;

    internal delegate void EnemyHandler();
    internal EnemyHandler Attack;

    protected void Start()
    {
        _entityScanner = new EntityScanner(enemyParams.radius, enemyParams.targetMask, transform);
        _spriteRenderer = body.GetComponentInChildren<SpriteRenderer>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();

        GameManager.Instance.OnGridChange += SetDestractionTarget;
        GameManager.Instance.battleManager.OnRest += Escape;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, BaseController.instance.transform.position);

        if (DestinationTarget != null || destractionTarget != null)
            transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destractionTarget.transform));

        if (TargetInRadius())
            Attack();
    }

    internal void SetDestractionTarget()
    {
        if (!TryRaycastToBase())
            destractionTarget = _entityScanner.GetClosestEntity(_entityScanner.GetAllEntities());
        else destractionTarget = BaseController.instance.entityHealth;
    }

    public void Die()
    {
        if (!gameObject.activeSelf)
            return;

        Instantiate(deadBody, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(gameObject);

        DropScore();
        GameManager.Instance.OnGridChange -= SetDestractionTarget;
        GameManager.Instance.battleManager.OnRest -= Escape;
        GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Remove(transform);
    }

    private bool TryRaycastToBase()
    {
        RaycastHit2D raycast =
        Physics2D.Raycast(transform.position, BaseController.instance.transform.position, 40, enemyParams.targetMask);

        if (raycast.collider == BaseController.instance)
            return true;
        else return false;
    }

    public IEnumerator Freeze(float freezeTime)
    {
        aIPath.canMove = false;
        _spriteRenderer.color = Color.blue;

        yield return new WaitForSeconds(freezeTime);

        aIPath.canMove = true;
        _spriteRenderer.color = Color.white;
    }

    internal virtual void Escape()
    {
        DestinationTarget = GameManager.Instance.battleManager.enemySpawner.escapePoint;
        destractionTarget = null;

        _spriteRenderer.sprite = _escapingBody;
        aIPath.maxSpeed = enemyParams.escapeSpeed;
        Invoke("Die", 9);
    }

    private void DropScore()
    {
        GameManager.Instance.scoreSystem.AddCoins(enemyParams.price);
        GameManager.Instance.scoreSystem.AddScore(enemyParams.score);
    }

    private bool TargetInRadius()
    {
        if (destractionTarget == null)
            return false;
        if (Vector2.Distance(transform.position, destractionTarget.transform.position) > enemyParams.radius)
            return false;
        else
            return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyParams.radius);
        Gizmos.color = Color.green;
    }
}