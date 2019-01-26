using UnityEngine;
using UnityEngine.Events;

public class InteractHandler : InteractiveMoment
{
    public UnityEvent OnAiInteract, OnPlayerInteract;

    private ActionTimeout lockTimeout;
    
    public override void Reset()
    {
        IsComplete = false;
        IsInteracting = false;
    }

    void Update()
    {
        if (lockTimeout != null)
            lockTimeout.Tick(Time.deltaTime);
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
        PlayerAgent playerAgent = agent as PlayerAgent;
        playerAgent.ReachTargetChecker.OnDestinationReached = null;
        if (playerAgent.GetComponent<ItemsManager>().IsHoldingItem) return;
        
        OnPlayerInteract.Invoke();
        EndInteraction(InteractingAgents, true);
        IsComplete = false;
        SetLock();
    }

    protected override void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached = null;
        EndInteraction(InteractingAgents, true);
        IsComplete = false;
        SetLock();
    }

    private void SetLock()
    {
        Locked = true;
        lockTimeout = new ActionTimeout(LockTime, ReleaseLock);
    }

    private void ReleaseLock()
    {
        Locked = false;
        lockTimeout = null;
    }
}