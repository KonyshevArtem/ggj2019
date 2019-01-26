using System;
using JetBrains.Annotations;

public class ActionTimeout
{
    private readonly Action action;
    
    private float timeout;

    public ActionTimeout(float timeout, [NotNull] Action action)
    {
        this.timeout = timeout;
        this.action = action;
    }

    public void Tick(float deltaTime)
    {
        timeout -= deltaTime;
        if (timeout <= 0)
            action();
    }
}