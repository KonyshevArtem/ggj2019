using UnityEngine;
using UnityEngine.Events;

public class Destruction : InteractiveMoment
{
    public float DestructionTime;
    public TimerIconAnimation TimerIconAnimation;
    public UnityEvent OnDestroy, OnPlayerApproach, OnReset;
    public AudioSource RepairSource;

    private ActionTimeout actionTimeout, lockTimeout;

    protected virtual bool CanInteract()
    {
        return true;
    }

    public override void Reset()
    {
        IsInteracting = false;
        IsComplete = false;
        actionTimeout = null;
        OnReset.Invoke();
        InteractingAgents.Clear();

        SetLock();
    }

    private void SetLock()
    {
        Locked = true;
        lockTimeout = new ActionTimeout(LockTime, ReleaseLock);
    }

    protected override void AgentBeginInteraction(AIAgent agent, int index)
    {
        agent.ReachTargetChecker.OnDestinationReached += AiAgentApproachTarget;
        agent.IsInteracting = true;
        agent.GoTo(transform.position);
    }

    protected override void AiAgentApproachTarget(Agent agent)
    {
        (agent as AIAgent).ReachTargetChecker.OnDestinationReached -= AiAgentApproachTarget;
        if (!IsInteracting) return;
        actionTimeout = new ActionTimeout(DestructionTime, DestroyGameObject);
    }

    public override void PlayerApproachTarget(Agent agent)
    {
        InteractingPlayer = agent as PlayerAgent;
        InteractingPlayer.ReachTargetChecker.OnDestinationReached = null;
        actionTimeout = null;
        if (IsComplete)
        {
            IsInteracting = true;
            actionTimeout = new ActionTimeout(4, Reset);
            if (RepairSource != null)
                RepairSource.Play();
            TimerIconAnimation.StartAnim(4);
        }
        else
        {
            if (!CanInteract()) return;
            OnPlayerApproach.Invoke();
            EndInteraction(InteractingAgents, false);
            SetLock();
        }
    }

    void Update()
    {
        if (actionTimeout != null && IsInteracting)
        {
            actionTimeout.Tick(Time.deltaTime);
            if (IsComplete && InteractingPlayer != null && !InteractingPlayer.NavMeshAgent.IsDestinationReached())
            {
                actionTimeout = null;
                TimerIconAnimation.StopAnim();
            }
        }

        if (lockTimeout != null)
        {
            lockTimeout.Tick(Time.deltaTime);
        }
    }

    private void DestroyGameObject()
    {
        actionTimeout = null;
        OnDestroy.Invoke();
        InteractingAgents.ForEach(agent => agent.GetComponent<HitAnimation>().PlayHitAnimation(gameObject));
        EndInteraction(InteractingAgents, true);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
    }


    private void ReleaseLock()
    {
        lockTimeout = null;
        Locked = false;
    }
}