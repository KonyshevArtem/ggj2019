using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractiveMomentsController : MonoBehaviour
{
    public List<InteractiveMoment> InteractiveMoments;
    public List<AIAgent> AiAgents;

    private ActionRepeater actionRepeater;

    void Start()
    {
        actionRepeater = new ActionRepeater(() => 3, GenerateRandomEvent);
    }

    void Update()
    {
        actionRepeater.Tick(Time.deltaTime);
    }

    private void GenerateRandomEvent()
    {
        InteractiveMoment interactiveMoment = InteractiveMoments
            .Where(moment => !moment.IsInteracting && !moment.IsComplete && moment.gameObject.activeSelf)
            .OrderBy(moment => Random.Range(0, 100))
            .FirstOrDefault();
        if (interactiveMoment != null)
            interactiveMoment.StartInteraction(AiAgents);
    }
}