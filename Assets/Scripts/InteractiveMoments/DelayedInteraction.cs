using UnityEngine;
using UnityEngine.Events;

public class DelayedInteraction : InteractiveMoment
{
    public float Delay;
    public UnityEvent OnTimerEnd;
    public TimerIconAnimation TimerIconAnimation;
    public AudioSource InteractSource;

    private ActionTimeout actionTimeout;
    private Agent interactingAgent;

    public void InitializeInteraction(Agent agent)
    {
        PlayerAgent playerAgent = agent as PlayerAgent;
        playerAgent.ReachTargetChecker.OnDestinationReached = null;
        if (playerAgent.GetComponent<ItemsManager>().IsHoldingItem) return;

        interactingAgent = playerAgent;
        actionTimeout = new ActionTimeout(Delay, () => OnTimerEnd.Invoke());
        TimerIconAnimation.StartAnim(Delay);
        InteractSource.Play();
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