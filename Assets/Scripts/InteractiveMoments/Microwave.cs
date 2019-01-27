﻿using UnityEngine;

public class Microwave : InteractHandler
{
    public float MicrowaveTime;
    public AudioSource AudioSource;
    public int PizdecPoints;

    private ActionTimeout microwaveActionTimeout, repairActionTimeout;
    private PlayerAgent interactingPlayer;

    public GameObject IntactModel, BrokenModel;

    public override void PlayerApproachTarget(Agent agent)
    {
        interactingPlayer = agent as PlayerAgent;        
        if (interactingPlayer.GetComponent<ItemsManager>().IsHoldingItem) return;
        
        if (!IsInteracting)
        {
            bool isComplete = IsComplete;
            base.PlayerApproachTarget(agent);
        
            IsComplete = isComplete;
            if (!IsComplete)
            {
                if (microwaveActionTimeout != null)
                {
                    interactingPlayer = null;
                    microwaveActionTimeout = null;
                    AudioSource.Stop();
                    OnAiInteract.Invoke();
                }
            }
            else
            {
                TimerIconAnimation.StartAnim(8);
                repairActionTimeout = new ActionTimeout(8, () =>
                {
                    LevelPoints.Instance.AddPizdecPoints(-PizdecPoints);
                    repairActionTimeout = null;
                    Reset();
                });
            }
        }
    }

    protected override void AgentEndInteraction(AIAgent agent, int agentIndex)
    {
        base.AgentEndInteraction(agent, agentIndex);
        AudioSource.Play();
        microwaveActionTimeout = new ActionTimeout(MicrowaveTime, Explode);
    }

    private void Explode()
    {
        LevelPoints.Instance.AddPizdecPoints(PizdecPoints);
        microwaveActionTimeout = null;
        AudioSource.Stop();
        IsComplete = true;
    }

    protected override void Update()
    {
        base.Update();
        if (microwaveActionTimeout != null)
            microwaveActionTimeout.Tick(Time.deltaTime);
        if (repairActionTimeout != null)
        {
            repairActionTimeout.Tick(Time.deltaTime);
            if (!interactingPlayer.NavMeshAgent.IsDestinationReached())
            {
                repairActionTimeout = null;
                TimerIconAnimation.StopAnim();
            }
        }
        
        IntactModel.SetActive(!IsComplete);
        BrokenModel.SetActive(IsComplete);
    }
}