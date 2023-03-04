using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected State nextState;
    [SerializeField] protected string targetTagName = "Player";

    public GameObject Target { get; set; }
    public NavMeshAgent Agent { get; set; }
    public abstract State UpdateState();

    private void Start()
    {
        if(!nextState) Debug.LogWarning($"{gameObject.name}'s next state has not been assigned!");
        Target = GameObject.FindWithTag(targetTagName);
        if(!Target) Debug.LogError("Could not find target by tag!");
    }
    
    protected void MoveTo(Vector3 targetDestination)
    {
        Agent.destination = targetDestination;
    }

    protected bool IsInRange(Vector3 targetPosition, float radius)
    {
        return Vector3.Distance(Agent.transform.position, targetPosition) < radius;
    }

    protected Vector3 GetRandomPointInRadius(float radius)
    {
        Vector3 direction = Random.insideUnitSphere * radius;
        direction += transform.position;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(direction, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}

public class StateMachine : MonoBehaviour
{
    private State[] states;
    public State currentState;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        states = GetComponentsInChildren<State>();
        
        foreach (State state in states)
        {
            state.Agent = agent;
        }
    }

    private void Start()
    {
        currentState = states[0];
    }

    private void Update()
    {
        State nextState = currentState?.UpdateState();
        if (nextState == null) return;
        currentState = nextState;
    }
}