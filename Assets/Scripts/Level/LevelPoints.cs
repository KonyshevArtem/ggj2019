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

    public MusicController MusicController;
    

    private void Start()
    {
        Instance = this;
        PizdecCurrentPoints = PizdecStartPoints;
        NeighboursCurrentPoints = NeighboursStartPoints;
        PickleRick.Value = 5;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            NeighboursCurrentPoints = NeighboursMaxPoints;
        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentLevelTime = TotalLevelTime;
            PizdecCurrentPoints = PizdecMaxPoints;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            CurrentLevelTime = TotalLevelTime;
            PizdecCurrentPoints = 0;
        }
        
        
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
            if ((float) PizdecCurrentPoints / PizdecMaxPoints > 0.5)
                ParentLose();
            else
                Win();
        }

        if (NeighboursCurrentPoints >= NeighboursMaxPoints && !IsGameFinished)
        {
            IsGameFinished = true;
            NeighboursLose();
        }
    }

    public void Win()
    {
        
    }

    public void ParentLose()
    {
        
    }

    public void NeighboursLose()
    {
        MusicController.NeighboursLose();
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