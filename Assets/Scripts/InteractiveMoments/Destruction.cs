using UnityEngine;
using UnityEngine.Events;

public abstract class Destruction : InteractiveMoment
{
    public float DestructionTime;
    public TimerIconAnimation TimerIconAnimation;
    public UnityEvent OnDestroy, OnPlayerApproach, OnReset;
    public AudioSource RepairSource;

    private ActionTimeout actionTimeout;


    protected abstract bool CanInteract();

    public override void Reset()
    {
        IsInteracting = false;
        IsComplete = false;
        actionTimeout = null;
        OnReset.Invoke();
        InteractingAgents.Clear();
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
            RepairSource.Play();
            TimerIconAnimation.StartAnim(4);
        }
        else
        {
            if (!CanInteract()) return;
            OnPlayerApproach.Invoke();
            EndInteraction(InteractingAgents);   
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
    }

    private void DestroyGameObject()
    {
        actionTimeout = null;
        OnDestroy.Invoke();
        InteractingAgents.ForEach(agent => agent.GetComponent<HitAnimation>().PlayHitAnimation(gameObject));
        EndInteraction(InteractingAgents);
    }

    protected override void AgentEndInteraction(AIAgent agent, int index)
    {
        agent.IsInteracting = false;
    }
}