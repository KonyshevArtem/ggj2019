using UnityEngine;
using UnityEngine.Events;

public class DelayedInteraction : MonoBehaviour
{
    public float Delay;
    public UnityEvent OnTimerEnd;
    public TimerIconAnimation TimerIconAnimation;

    private ActionTimeout actionTimeout;

    public void InitializeInteraction()
    {
        PlayerAgent.Instance.ReachTargetChecker.OnDestinationReached = null;
        actionTimeout = new ActionTimeout(Delay, () => OnTimerEnd.Invoke());
        TimerIconAnimation.StartAnim(Delay);
    }

    void Update()
    {
        if (actionTimeout != null)
            actionTimeout.Tick(Time.deltaTime);
    }
}