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
        return GetNearestPointOnNavMesh(randomPoint, radius);
    }

    public static Vector3 GetNearestPointOnNavMesh(Vector3 pointOutsideNavMesh, float radius)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(pointOutsideNavMesh, out hit, radius, 1);
        return hit.position;
    }
}