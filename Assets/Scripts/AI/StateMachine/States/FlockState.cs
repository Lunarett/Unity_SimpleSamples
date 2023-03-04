using System;
using UnityEditor;
using UnityEngine;

public class FlockState : State
{
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private float detectionRadius = 10.0f;
    [SerializeField] private float alignmentWeight = 1.0f;
    [SerializeField] private float cohesionWeight = 1.0f;
    [SerializeField] private float separationWeight = 1.0f;
    [SerializeField] private float flockRadius = 5.0f;

    private GameObject[] flockObjects;

    public override State UpdateState()
    {
        if (IsInRange(Target.transform.position, detectionRadius)) return chaseState;

        flockObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (flockObjects.Length <= 1)
        {
            return nextState;
        }

        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int numNeighbors = 0;

        foreach (GameObject flockObject in flockObjects)
        {
            // Ignore Self
            if (flockObject == gameObject) continue;

            // Ignore if not in not in range
            if (!IsInRange(flockObject.transform.position, flockRadius)) continue;

            alignment += flockObject.transform.forward;
            cohesion += flockObject.transform.position;
            separation += transform.position - flockObject.transform.position;
            numNeighbors++;
        }

        if (numNeighbors == 0) return nextState;

        alignment /= numNeighbors;
        cohesion /= numNeighbors;
        separation /= numNeighbors;

        alignment.Normalize();
        cohesion = (cohesion - transform.position).normalized;
        separation.Normalize();

        Vector3 flockDirection =
            alignment * alignmentWeight + cohesion * cohesionWeight + separation * separationWeight;
        MoveTo(transform.position + flockDirection);

        return this;
    }
}