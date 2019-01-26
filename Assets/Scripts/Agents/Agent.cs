using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    public ReachTargetChecker ReachTargetChecker;
    
    protected NavMeshAgent NavMeshAgent;

    protected virtual void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        ReachTargetChecker = new ReachTargetChecker(this, NavMeshAgent);
    }

    protected virtual void Update()
    {
        ReachTargetChecker.Update();
    }
}