using UnityEngine;
using UnityEngine.Events;

public class InteractHandler : InteractiveMoment
{
    public UnityEvent OnAiInteract, OnPlayerInteract;
    
    public override void Reset()
    {
        IsComplete = false;
        IsInteracting = false;
    }

    protected override void AgentBeginInteraction(AIAgent agent, int agentIndex)
    {
        agent.ReachTargetChecker.OnDestinationReached += AiAgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    protected override void AgentEndInteraction(AIAgent agent, int agentIndex)
    {
        agent.IsInteracting = false;
        OnAiInteract.Invoke();
        agent.GetComponent<HitAnimation>().PlayHitAnimation(gameObject);
    }

    public override void PlayerApproachTarget(Agent agent)
    {
        (agent as PlayerAgent).ReachTargetChecker.OnDestinationReached = null;
        OnPlayerInteract.Invoke();
        EndInteraction(InteractingAgents);
        IsComplete = false;
    }

    protected override void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached = null;
        EndInteraction(InteractingAgents);
        IsComplete = false;
    }
}