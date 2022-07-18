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

    [Header("Extra references")]
    [SerializeField] private GameObject _legs;
    [SerializeField] private Sprite _escapingBody;

    [Header("Base References")]
    [SerializeField] internal AIPath aIPath;
    [SerializeField] internal AIDestinationSetter aIDestinationSetter;
    private EntityScanner _entityScanner;
    private SpriteRenderer _bodyRenderer;

    private Transform DestractionTarget { get => destractionTarget.transform; }
    internal Transform DestinationTarget
    {
        get
        {
            return aIDestinationSetter.target;
        }

        set
        {
            aIDestinationSetter.target = value;
        }
    }
    internal EntityHealth destractionTarget;

    [SerializeField] internal Hostage hostageStatement;
    [SerializeField] internal GameObject deadBody;

    internal delegate void EnemyHandler();
    internal EnemyHandler Attack;

    protected void Start()
    {
        _entityScanner = new EntityScanner(enemyParams.radius, enemyParams.targetMask, transform);
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _bodyRenderer = body.GetComponentInChildren<SpriteRenderer>();
        GameManager.Instance.OnGridChange += SetDestractionTarget;
        GameManager.Instance.battleManager.OnRest += Escape;
    }

    private void Update()
    {
        if (DestinationTarget == null || destractionTarget == null)
            return;

        _legs.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(DestinationTarget));
        body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destractionTarget.transform));

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
        StartCoroutine(DieCoroutine());
        GetComponent<EntityHealth>().onDieEvent += DropScore;
    }

    private bool TryRaycastToBase()
    {
        RaycastHit2D raycast = Physics2D.Raycast
        (transform.position, BaseController.instance.transform.position - transform.position, 40, enemyParams.targetMask);
        if (raycast.collider == BaseController.instance)
            return true;
        else return false;
    }

    internal virtual void Escape()
    {
        DestinationTarget = GameManager.Instance.battleManager.enemySpawner.escapePoint;
        destractionTarget = null;

        SetEscapeDestination();
        _bodyRenderer.sprite = _escapingBody;

        aIPath.maxSpeed = enemyParams.escapeSpeed;
        Invoke("Die", 9);
    }

    private void SetEscapeDestination()
    {
        Transform escapePoint = GameManager.Instance.battleManager.enemySpawner.escapePoint;
        body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(escapePoint));
        _legs.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(escapePoint));
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

    private IEnumerator DieCoroutine()
    {
        GameObject deadObj =
        Instantiate(deadBody, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        GameManager.Instance.battleManager.OnRest -= Escape;
        GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Remove(transform);
        Destroy(gameObject);

        GameManager.Instance.OnGridChange -= SetDestractionTarget;
        GameManager.Instance.battleManager.OnRest -= Escape;

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyParams.radius);
        Gizmos.color = Color.green;
    }
}