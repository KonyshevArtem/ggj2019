using System;
using UnityEngine.AI;

public static class NavMeshAgentExtensions
{
    
    public static bool IsDestinationReached(this NavMeshAgent navMeshAgent)
    {
        if (!navMeshAgent.pathPending)
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                if (!navMeshAgent.hasPath || Math.Abs(navMeshAgent.velocity.sqrMagnitude) < 0.01f)
                    return true;

        return false;
    }
    
}