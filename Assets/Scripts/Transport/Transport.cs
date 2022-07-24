using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(EntityHealth))]
[RequireComponent(typeof(Explosive))]
abstract public class Transport : MonoBehaviour
{
    [Header("Components")]
    internal AIPath aIPath;
    internal AIDestinationSetter aIDestinationSetter;
    internal EntityHealth entityHealth;
    internal Explosive explosive;

    [Header("Transport parameters")]
    [SerializeField] internal float _speed;

    [Header("SFX")]
    [SerializeField] private EventReference _detonationSfx;

    [Header("VFX")]
    public GameObject detonatedBody;
    public ParticleSystem[] explosionFXs;

    internal void SetDestination(Transform target) => aIDestinationSetter.target = target;

    internal delegate void TransportHandler();
    internal event TransportHandler OnStopMove;
    internal event TransportHandler OnStartMove;

    internal virtual void OnEnable()
    {
        aIPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        entityHealth = GetComponent<EntityHealth>();
        explosive = GetComponent<Explosive>();
        SetDestination(BaseController.instance.transform);
        entityHealth.onDieEvent += Detonate;
        GameManager.Instance.battleManager.OnRest += Escape;

        aIPath.maxSpeed = _speed;
        StartMove();
    }

    internal void Escape()
    {
        SetDestination(GameManager.Instance.battleManager.enemySpawner.escapePoint);
        StartMove();

        Destroy(gameObject, 9);
    }

    internal virtual void StartMove()
    {
        if (aIDestinationSetter.target == null)
            return;

        OnStartMove?.Invoke();
        aIPath.canMove = true;
    }

    internal virtual void StopMove()
    {
        OnStopMove?.Invoke();
        aIPath.canMove = false;
    }

    internal virtual void Detonate()
    {
        Instantiate(detonatedBody, transform.position, Quaternion.Euler(transform.eulerAngles * -1));
        explosive.Detonate();
        GameManager.Instance.soundManager.PlaySoundEvent(_detonationSfx);
        entityHealth.CreateParticle(explosionFXs[Random.Range(0, explosionFXs.Length)]);
        Destroy(gameObject);
        GameManager.Instance.ScanAStar();
        GameManager.Instance.battleManager.enemySpawner.currentEnemiesInAction.Remove(transform);
    }
}