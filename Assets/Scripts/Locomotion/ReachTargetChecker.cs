using System;
using UnityEngine;
using UnityEngine.AI;

public class ReachTargetChecker
{
    public Action<Agent> OnDestinationReached;

    private readonly Agent agent;
    private readonly NavMeshAgent navMeshAgent;

    public ReachTargetChecker(Agent agent, NavMeshAgent navMeshAgent)
    {
        this.agent = agent;
        this.navMeshAgent = navMeshAgent;
    }

    public void Update()
    {
        if (NavigationUtils.IsDestinationReached(navMeshAgent) && OnDestinationReached != null)
            OnDestinationReached(agent);
    }
}