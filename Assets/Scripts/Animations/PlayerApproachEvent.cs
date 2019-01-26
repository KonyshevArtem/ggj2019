using UnityEngine;
using UnityEngine.Events;

public class PlayerApproachEvent : MonoBehaviour
{
    public UnityEvent OnApproach;

    public void StartApproaching()
    {
        PlayerAgent.Instance.ReachTargetChecker.OnDestinationReached += agent => OnApproach.Invoke();
    }
}