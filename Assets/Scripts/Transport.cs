using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(AIDestinationSetter))]

public class Transport : MonoBehaviour
{
    [Header("Components")]
    internal AIPath aIPath;
    internal AIDestinationSetter aIDestinationSetter;
    internal EntityHealth entityHealth;

    [Header("Transport parameters")]
    [SerializeField] internal float _speed;

    internal void SetDestination(Transform target) => aIDestinationSetter.target = target;

    internal delegate void TransportHandler();
    internal event TransportHandler OnStopMove;
    internal event TransportHandler OnStartMove;

    internal virtual void Start()
    {
        aIPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        entityHealth = GetComponent<EntityHealth>();

        aIPath.maxSpeed = _speed;
    }

    internal virtual void StartMove()
    {
        if (aIDestinationSetter.target == null)
            return; Debug.LogWarning("Destination isn't assighned!");

        OnStartMove?.Invoke();
        aIPath.canMove = true;
    }

    internal virtual void StopMove()
    {
        OnStopMove?.Invoke();
        aIPath.canMove = false;
    }
}