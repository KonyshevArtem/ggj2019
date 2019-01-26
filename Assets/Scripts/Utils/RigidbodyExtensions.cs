using UnityEngine;

public static class RigidbodyExtensions
{

    public static void AddForceTowards(this Rigidbody rigidbody, Transform target, float forceAmount)
    {
        Vector3 direction = (target.position - rigidbody.transform.position).normalized;
        rigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
    
}