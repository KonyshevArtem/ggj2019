using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIAgent : MonoBehaviour
{
    public float randomWalkingRadius;

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
    }

    public void GoTo(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void GoToRandomPoint()
    {
        Vector3 randomPoint = NavigationUtils.GetRandomPointOnNavMesh(transform.position, randomWalkingRadius);
        GoTo(randomPoint);
    }
}