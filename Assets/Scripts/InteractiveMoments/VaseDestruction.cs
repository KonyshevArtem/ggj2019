using System.Collections.Generic;
using UnityEngine.AI;

public class VaseDestruction : InteractiveMoment
{
    public override void Interact(List<AIAgent> agents)
    {
        IsInteracting = true;
        agents.ForEach(agent => agent.GoTo(transform.position));
    }

    public override void CompleteInteraction(List<AIAgent> agents)
    {
        IsInteracting = false;
        IsComplete = true;
        agents.ForEach(agent => agent.GoToRandomPoint());
    }
}