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
    public int price;
    public int score;
    public float radius;
    public LayerMask targetMask;
    [SerializeField] private float _escapeSpeed;

    [Header("Extra references")]
    public GameObject legs;
    public AIPath aIPath;

    [Header("Destinations")]
    public Transform destinationTarget;
    public Transform aimTarget;

    [Header("Base References")]
    public Hostage hostageStatement;
    public GameObject deadBody;

    internal delegate void EnemyHandler();
    internal EnemyHandler mainAction;

    private void Update()
    {
        if (GameManager.Instance.battleManager.isRest)
            Escape();

        if (aimTarget == null)
            return;

        body.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(aimTarget));
        legs.transform.rotation = Quaternion.Euler(0, 0, CalculateRotation(destinationTarget));

        if (!TargetInRadius())
            return;
        mainAction();

    }

    private void Escape()
    {
        destinationTarget = GameManager.Instance.battleManager.spawners[1];
        aIPath.maxSpeed = _escapeSpeed;
        Invoke("Die", 9);
    }

    public void Die()
    {
        if (!gameObject.activeSelf)
            return;
        StartCoroutine(DieCoroutine());
        GetComponent<EntityHealth>().onDieEvent += DropScore;
        GameManager.Instance.battleManager.currentEnemiesInAction.Remove(transform);
    }

    private void DropScore()
    {
        GameManager.Instance.scoreSystem.AddCoins(price);
        GameManager.Instance.scoreSystem.AddScore(score);
    }

    private bool TargetInRadius()
    {
        if (Vector2.Distance(transform.position, aimTarget.transform.position) > radius)
            return false;
        else
            return true;
    }

    private float CalculateRotation(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        return angle;
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
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.green;
    }
}