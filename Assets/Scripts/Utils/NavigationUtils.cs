using UnityEngine;
using UnityEngine.AI;

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
}