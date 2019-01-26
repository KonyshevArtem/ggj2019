using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Destruction : InteractiveMoment, IPointerClickHandler
{
    public float DestructionTime;
    public UnityEvent OnDestroy, OnClick;

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
        actionTimeout = new ActionTimeout(DestructionTime, DestroyGameObject);
    }

    void Update()
    {
        if (actionTimeout != null)
            actionTimeout.Tick(Time.deltaTime);
    }

    private void DestroyGameObject()
    {
        actionTimeout = null;
        OnDestroy.Invoke();
        InteractingAgents.ForEach(agent => agent.GetComponent<HitAnimation>().PlayHitAnimation(gameObject));
        CompleteInteraction(InteractingAgents);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
        agent.GoToRandomPoint();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}