using UnityEngine;

public class VaseDestruction : InteractiveMoment
{

    public float VaseDestructionTime;
    public GameObject VaseGameObject, BrokenVaseGameObject;

    private ActionTimeout actionTimeout;
    
    
    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.OnDestinationReached += AgentApproachVase;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    private void AgentApproachVase(AIAgent agent)
    {
        agent.OnDestinationReached -= AgentApproachVase;
        actionTimeout = new ActionTimeout(VaseDestructionTime, DestroyVase);
    }

    void Update()
    {
        if (actionTimeout != null)
            actionTimeout.Tick(Time.deltaTime);
    }

    private void DestroyVase()
    {
        actionTimeout = null;
        VaseGameObject.SetActive(false);
        BrokenVaseGameObject.SetActive(true);
        CompleteInteraction(InteractingAgents);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
        agent.GoToRandomPoint();
    }
}