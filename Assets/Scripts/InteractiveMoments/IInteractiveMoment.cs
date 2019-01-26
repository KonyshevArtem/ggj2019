using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveMoment: MonoBehaviour
{
    protected List<AIAgent> InteractingAgents;
    
    public bool IsInteracting { get; set; }
    public bool IsComplete { get; set; }
    public int AgentsAmounts;

    public virtual void Interact(List<AIAgent> agents)
    {
        IsInteracting = true;
        InteractingAgents = agents;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentBeginInteraction(InteractingAgents[i], i);
    }
    

    public virtual void CompleteInteraction(List<AIAgent> agents)
    {
        IsInteracting = false;
        IsComplete = true;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentEndInteraction(InteractingAgents[i], i);
    }

    protected abstract void AgentBeginInteraction(AIAgent agent, int agentIndex);
    protected abstract void AgentEndInteraction(AIAgent agent, int agentIndex);
}
