using System;
using JetBrains.Annotations;

public class ActionRepeater
{
    private readonly float defaultTime;
    private readonly Action action;

    private float currentTime;

    public ActionRepeater(float repeatTime, [NotNull] Action action)
    {
        defaultTime = repeatTime;
        currentTime = repeatTime;
        this.action = action;
    }

    public void Tick(float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0)
        {
            action();
            currentTime = defaultTime;
        }
    }
}
