using UnityEngine;
using UnityEngine.Events;

public class Destruction : InteractiveMoment
{
    public float DestructionTime;
    public UnityEvent OnDestroy, OnPlayerApproach, OnReset;

    private ActionTimeout actionTimeout;


    public override void Reset()
    {
        IsInteracting = false;
        IsComplete = false;
        OnReset.Invoke();
    }

    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.ReachTargetChecker.OnDestinationReached += AiAgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    private void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached -= AiAgentApproachTarget;
        if (!IsInteracting) return;
        actionTimeout = new ActionTimeout(DestructionTime, DestroyGameObject);
    }

    public void PlayerApproachTarget(Agent agent)
    {
        (agent as PlayerAgent).ReachTargetChecker.OnDestinationReached -= PlayerApproachTarget;
        if (IsComplete) return;
        OnPlayerApproach.Invoke();
        EndInteraction(InteractingAgents);
    }

    void Update()
    {
        if (actionTimeout != null && IsInteracting)
            actionTimeout.Tick(Time.deltaTime);
    }

    private void DestroyGameObject()
    {
        actionTimeout = null;
        OnDestroy.Invoke();
        InteractingAgents.ForEach(agent => agent.GetComponent<HitAnimation>().PlayHitAnimation(gameObject));
        EndInteraction(InteractingAgents);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
    }
}