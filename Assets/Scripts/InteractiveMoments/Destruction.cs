using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Destruction : InteractiveMoment, IPointerClickHandler
{
    public float DestructionTime;
    public UnityEvent OnDestroy, OnPlayerApproach;

    private ActionTimeout actionTimeout;


    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.ReachTargetChecker.OnDestinationReached += AiAgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    private void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached -= AiAgentApproachTarget;
        if (IsInteracting)
            actionTimeout = new ActionTimeout(DestructionTime, DestroyGameObject);
    }

    private void PlayerApproachTarget(Agent agent)
    {
        (agent as PlayerAgent).ReachTargetChecker.OnDestinationReached -= PlayerApproachTarget;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAgent.Instance.ReachTargetChecker.OnDestinationReached += PlayerApproachTarget;
    }
}