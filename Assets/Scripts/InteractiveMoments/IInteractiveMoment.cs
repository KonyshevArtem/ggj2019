using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractiveMoment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected List<AIAgent> InteractingAgents = new List<AIAgent>();
    protected PlayerAgent InteractingPlayer;

    public List<Outline> Outlines;

    public bool IsInteracting { get; set; }
    public bool IsComplete { get; set; }
    public bool Locked { get; set; }

    public float LockTime;
    public int AgentsAmounts;
    
    public TimerIconAnimation TimerIconAnimation;

    public virtual void StartInteraction(List<AIAgent> agents)
    {
        IsInteracting = true;
        InteractingAgents = agents;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentBeginInteraction(InteractingAgents[i], i);
    }


    public virtual void EndInteraction(List<AIAgent> agents, bool isComplete)
    {
        IsInteracting = false;
        IsComplete = isComplete;
        for (int i = 0; i < InteractingAgents.Count; i++)
            AgentEndInteraction(InteractingAgents[i], i);
        InteractingAgents.Clear();
        InteractingPlayer = null;
        SetOutlinesActive(false);
    }

    public abstract void Reset();
    protected abstract void AgentBeginInteraction(AIAgent agent, int agentIndex);
    protected abstract void AgentEndInteraction(AIAgent agent, int agentIndex);
    public abstract void PlayerApproachTarget(Agent agent);
    protected abstract void AiAgentApproachTarget(Agent agent);


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOutlinesActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetOutlinesActive(false);
    }

    private void SetOutlinesActive(bool isActive)
    {
        Outlines.ForEach(outline => outline.enabled = isActive);
    }
}