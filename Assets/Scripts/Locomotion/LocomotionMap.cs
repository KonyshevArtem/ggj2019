using System.Collections.Generic;
using UnityEngine;

public class LocomotionMap : MonoBehaviour
{
    public List<LocomotionPoint> LocomotionPoints;

    public Vector3 GetRandomPoint()
    {
        return LocomotionPoints[Random.Range(0, LocomotionPoints.Count)].GetRandomPoint();
    }
}