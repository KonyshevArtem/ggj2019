using UnityEngine;

public class LevelPoints : MonoBehaviour
{
    public static LevelPoints Instance;
    
    public int NeighboursMaxPoints, PizdecMaxPoints, NeighboursStartPoints, PizdecStartPoints;
    public FillBar NeighboursBar, PizdecBar;
    public PickleRick PickleRick;

    public int NeighboursCurrentPoints, PizdecCurrentPoints;

    public float TotalLevelTime, CurrentLevelTime;

    public bool IsGameFinished;
    

    private void Start()
    {
        Instance = this;
        PizdecCurrentPoints = PizdecStartPoints;
        NeighboursCurrentPoints = NeighboursStartPoints;
        PickleRick.Value = 5;
    }

    private void Update()
    {
        NeighboursBar.Value = (int) ((float) NeighboursCurrentPoints / NeighboursMaxPoints * 100);
        PizdecBar.Value = (int) ((float) PizdecCurrentPoints / PizdecMaxPoints * 100);

        CurrentLevelTime += Time.deltaTime;

        if (CurrentLevelTime >= TotalLevelTime)
        {
            PickleRick.Value = 0;
        }
        else
        {
            PickleRick.Value = (int) (Mathf.Max(0, TotalLevelTime - CurrentLevelTime) / TotalLevelTime * 5) + 1; 
        }
        
        if (CurrentLevelTime >= TotalLevelTime && !IsGameFinished)
        {
            IsGameFinished = true;
            
        }

        if (NeighboursCurrentPoints >= NeighboursMaxPoints && !IsGameFinished)
        {
            IsGameFinished = true;
        }
    }

    public void AddPizdecPoints(int amount)
    {
        PizdecCurrentPoints = Mathf.Clamp(PizdecCurrentPoints + amount, 0, PizdecMaxPoints);
    }

    public void AddNeighboursPoints(int amount)
    {
        NeighboursCurrentPoints = Mathf.Clamp(NeighboursCurrentPoints + amount, 0, NeighboursMaxPoints);
    }
}