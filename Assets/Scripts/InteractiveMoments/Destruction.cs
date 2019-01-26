using UnityEngine;

public class Destruction : InteractiveMoment
{

    public float DestructionTime;
    public GameObject IntactGameObject, BrokenGameObject;

    private ActionTimeout actionTimeout;
    
    
    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.OnDestinationReached += AgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    private void AgentApproachTarget(AIAgent agent)
    {
        agent.OnDestinationReached -= AgentApproachTarget;
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
        IntactGameObject.SetActive(false);
        BrokenGameObject.SetActive(true);
        InteractingAgents.ForEach(agent => agent.GetComponent<HitAnimation>().PlayHitAnimation(IntactGameObject));
        //CompleteInteraction(InteractingAgents);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
        agent.GoToRandomPoint();
    }
}