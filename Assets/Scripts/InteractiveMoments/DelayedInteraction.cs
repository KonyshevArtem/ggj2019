using UnityEngine;
using UnityEngine.Events;

public class DelayedInteraction : InteractiveMoment
{
    public float Delay;
    public UnityEvent OnTimerEnd;
    public TimerIconAnimation TimerIconAnimation;

    private ActionTimeout actionTimeout;
    private Agent interactingAgent;

    public void InitializeInteraction(Agent agent)
    {
        interactingAgent = agent;
        PlayerAgent.Instance.ReachTargetChecker.OnDestinationReached = null;
        actionTimeout = new ActionTimeout(Delay, () => OnTimerEnd.Invoke());
        TimerIconAnimation.StartAnim(Delay);
    }

    void Update()
    {
        if (actionTimeout != null)
        {
            actionTimeout.Tick(Time.deltaTime);
            if (!interactingAgent.NavMeshAgent.IsDestinationReached())
            {
                actionTimeout = null;
                TimerIconAnimation.StopAnim();
            }
        }
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    protected override void AgentBeginInteraction(AIAgent agent, int agentIndex)
    {
        throw new System.NotImplementedException();
    }

    protected override void AgentEndInteraction(AIAgent agent, int agentIndex)
    {
        throw new System.NotImplementedException();
    }

    public override void PlayerApproachTarget(Agent agent)
    {
        throw new System.NotImplementedException();
    }

    protected override void AiAgentApproachTarget(Agent agent)
    {
        throw new System.NotImplementedException();
    }
}