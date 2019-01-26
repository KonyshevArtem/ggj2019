using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavigationUtils
{
    public static Vector3 GetRandomPointOnNavMesh(Vector3 originPoint, float radius)
    {
        Vector2 insideUnitCircle = Random.insideUnitCircle;
        Vector3 randomPoint = new Vector3(insideUnitCircle.x, originPoint.y, insideUnitCircle.y) * radius;
        randomPoint += originPoint;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }

    public static bool IsDestinationReached(NavMeshAgent navMeshAgent)
    {
        if (!navMeshAgent.pathPending)
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                if (!navMeshAgent.hasPath || Math.Abs(navMeshAgent.velocity.sqrMagnitude) < 0.01f)
                    return true;

        return false;
    }
}