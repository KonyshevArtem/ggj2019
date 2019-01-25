using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class RunAnimator : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public float AnimatorSpeedFactor;

    private Animator animator;
    private float currentVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentVelocity = Mathf.Lerp(currentVelocity, NavMeshAgent.velocity.normalized.magnitude, Time.deltaTime * AnimatorSpeedFactor);
        animator.SetFloat("Speed", currentVelocity);
    }

}
