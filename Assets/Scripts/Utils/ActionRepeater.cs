using System;
using JetBrains.Annotations;

public class ActionRepeater
{
    private readonly Func<float> getTimeout;
    private readonly Action action;

    private float currentTime;

    public ActionRepeater(Func<float> getTimeout, [NotNull] Action action)
    {
        this.getTimeout = getTimeout;
        currentTime = getTimeout();
        this.action = action;
    }

    public void Tick(float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0)
        {
            action();
            currentTime = getTimeout();
        }
    }
}
