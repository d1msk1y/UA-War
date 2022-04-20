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
    public GameObject legs;

    [Header("Base References")]
    [SerializeField] internal AIPath aIPath;
    [SerializeField] internal Transform destinationTarget;
    [SerializeField] internal Hostage hostageStatement;
    [SerializeField] internal GameObject deadBody;

    internal delegate void EnemyHandler();
    internal EnemyHandler mainAction;

    private void Start()
    {
        GameManager.Instance.battleManager.OnRest += Escape;
    }

    private void Update()
    {
        if (destinationTarget == null)
            return;

        body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destinationTarget));
        legs.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destinationTarget));

        if (!TargetInRadius())
            return;
        mainAction();

    }

    public void Die()
    {
        if (!gameObject.activeSelf)
            return;
        StartCoroutine(DieCoroutine());
        GetComponent<EntityHealth>().onDieEvent += DropScore;
        GameManager.Instance.battleManager.currentEnemiesInAction.Remove(transform);
    }

    private void Escape()
    {
        destinationTarget = GameManager.Instance.battleManager.spawners[1];
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
        if (Vector2.Distance(transform.position, destinationTarget.transform.position) > enemyParams.radius)
            return false;
        else
            return true;
    }

    private IEnumerator DieCoroutine()
    {
        gameObject.SetActive(false);
        GameObject deadObj =
        Instantiate(deadBody, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        yield return new WaitForSeconds(30);

        Destroy(deadObj);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyParams.radius);
        Gizmos.color = Color.green;
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }
}