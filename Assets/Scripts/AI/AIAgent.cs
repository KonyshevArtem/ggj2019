using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class AIAgent : MonoBehaviour
{
    public float RandomWalkingRadius;
    public bool IsInteracting;

    public Action<AIAgent> OnDestinationReached;

    private NavMeshAgent navMeshAgent;
    private ActionRepeater actionRepeater;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        actionRepeater = new ActionRepeater(() => Random.Range(3, 3), GoToRandomPoint);
    }

    private void Update()
    {
        actionRepeater.Tick(Time.deltaTime);
        if (NavigationUtils.IsDestinationReached(navMeshAgent) && OnDestinationReached != null)
            OnDestinationReached(this);
    }

    public void GoTo(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void GoToRandomPoint()
    {
        if (!IsInteracting)
        {
            Vector3 randomPoint = NavigationUtils.GetRandomPointOnNavMesh(transform.position, RandomWalkingRadius);
            GoTo(randomPoint);
        }
    }
}