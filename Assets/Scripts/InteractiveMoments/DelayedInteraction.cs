using UnityEngine;
using UnityEngine.Events;

public class DelayedInteraction : MonoBehaviour
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
}