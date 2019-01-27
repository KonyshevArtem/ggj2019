using UnityEngine;

public class LevelPoints : MonoBehaviour
{
    public static LevelPoints Instance;
    
    public int NeighboursMaxPoints, PizdecMaxPoints, NeighboursStartPoints, PizdecStartPoints;
    public FillBar NeighboursBar, PizdecBar;

    public int NeighboursCurrentPoints, PizdecCurrentPoints;


    private void Start()
    {
        Instance = this;
        PizdecCurrentPoints = PizdecStartPoints;
        NeighboursCurrentPoints = NeighboursStartPoints;
    }

    private void Update()
    {
        NeighboursBar.Value = (int) ((float) NeighboursCurrentPoints / NeighboursMaxPoints * 100);
        PizdecBar.Value = (int) ((float) PizdecCurrentPoints / PizdecMaxPoints * 100);       
    }

    public void AddPizdecPoints(int amount)
    {
        PizdecCurrentPoints += amount;
    }

    public void AddNeighboursPoints(int amount)
    {
        NeighboursCurrentPoints += amount;
    }
}