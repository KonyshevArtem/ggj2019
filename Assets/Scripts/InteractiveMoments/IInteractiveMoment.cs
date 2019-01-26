using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveMoment: MonoBehaviour
{
    public bool IsInteracting { get; set; }
    public bool IsComplete { get; set; }
    public int AgentsAmounts;
    
    public abstract void Interact(List<AIAgent> agents);
    public abstract void CompleteInteraction(List<AIAgent> agents);
}
