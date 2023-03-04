using UnityEngine;

public class ChaseState : State
{
    [SerializeField] private float stoppingDistance = 3.0f;

    public override State UpdateState()
    {
        MoveTo(Target.transform.position);
        return IsInRange(Target.transform.position, stoppingDistance) ? nextState : this;
    }
}
