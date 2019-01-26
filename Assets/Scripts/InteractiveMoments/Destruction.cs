using UnityEngine;
using UnityEngine.Events;

public abstract class Destruction : InteractiveMoment
{
    public float DestructionTime;
    public UnityEvent OnDestroy, OnPlayerApproach, OnReset;

    private ActionTimeout actionTimeout;


    protected abstract bool CanInteract();

    public override void Reset()
    {
        IsInteracting = false;
        IsComplete = false;
        actionTimeout = null;
        OnReset.Invoke();
        InteractingAgents.Clear();
    }

    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.ReachTargetChecker.OnDestinationReached += AiAgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    protected override void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached -= AiAgentApproachTarget;
        if (!IsInteracting) return;
        actionTimeout = new ActionTimeout(DestructionTime, DestroyGameObject);
    }

    public override void PlayerApproachTarget(Agent agent)
    {
        (agent as PlayerAgent).ReachTargetChecker.OnDestinationReached = null;
        actionTimeout = null;
        if (IsComplete)
        {
            IsInteracting = true;
            actionTimeout = new ActionTimeout(3, Reset);
        }
        else
        {
            if (!CanInteract()) return;
            OnPlayerApproach.Invoke();
            EndInteraction(InteractingAgents);   
        }
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