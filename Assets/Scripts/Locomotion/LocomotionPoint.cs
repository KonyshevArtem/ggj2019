using UnityEngine;

public class LocomotionPoint : MonoBehaviour
{
    public float Radius;


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public Vector3 GetRandomPoint()
    {
        Vector2 insideUnitCircle = Random.insideUnitCircle;
        return transform.position + new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y);
    }
}