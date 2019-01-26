using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveMoment: MonoBehaviour
{
    protected List<AIAgent> InteractingAgents = new List<AIAgent>();
    
    public bool IsInteracting { get; set; }
    public bool IsComplete { get; set; }
    public int AgentsAmounts;

    public virtual void StartInteraction(List<AIAgent> agents)
    {
        IsInteracting = true;
        InteractingAgents = agents;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentBeginInteraction(InteractingAgents[i], i);
    }
    

    public virtual void EndInteraction(List<AIAgent> agents)
    {
        IsInteracting = false;
        IsComplete = true;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentEndInteraction(InteractingAgents[i], i);
        InteractingAgents.Clear();
    }

    public abstract void Reset();
    protected abstract void AgentBeginInteraction(AIAgent agent, int agentIndex);
    protected abstract void AgentEndInteraction(AIAgent agent, int agentIndex);
    public abstract void PlayerApproachTarget(Agent agent);
    protected abstract void AiAgentApproachTarget(Agent agent);
}
