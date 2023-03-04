using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoamState : State
{
    [SerializeField] private FlockState flockState;
    [SerializeField] private float detectionRadius = 5.0f;
    [SerializeField] private float roamRadius = 3.0f;
    [SerializeField] private float acceptanceRadius = 0.5f;
    [SerializeField] private float flockRadius = 10.0f;

    private Vector3 currentDestination;
    private bool hasReachedDestination = true;

    public override State UpdateState()
    {
        // Switch to Flock State if nearby enemies
        GameObject[] flockColliders = GameObject.FindGameObjectsWithTag(flockState.gameObject.tag);
        if (flockColliders.Length >= 1)
        {
            int numNeighbors = flockColliders.Count(flockObject => IsInRange(flockObject.transform.position, flockRadius) && flockObject != flockState.gameObject);
            if (numNeighbors > 0) return flockState;
        }
        
        if (hasReachedDestination || !Agent.hasPath)
        {
            // Update destination once reached position
            currentDestination = GetRandomPointInRadius(roamRadius);
            hasReachedDestination = false;
        }

        // Move to random point within radius
        MoveTo(currentDestination);

        // Check if reached destination
        if (IsInRange(currentDestination, acceptanceRadius))
            hasReachedDestination = true;

        return IsInRange(Target.transform.position, detectionRadius) ? nextState : this;
    }
}