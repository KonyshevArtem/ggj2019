using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject FbiScreen, ParentsLoseScreen, ParentsWinScreen;
    private ActionTimeout fbiTimeout;

    public GameObject TutorialWindow;


    private void Start()
    {
        Instance = this;
        PizdecCurrentPoints = PizdecStartPoints;
        NeighboursCurrentPoints = NeighboursStartPoints;
        PickleRick.Value = 5;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 0;
            TutorialWindow.SetActive(true);
        }
    }

    public void FinishTutorial()
    {
        TutorialWindow.SetActive(false);
        Time.timeScale = 1;
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

        if (fbiTimeout != null)
        {
            fbiTimeout.Tick(Time.deltaTime);
        }
    }

    public void Win()
    {
        ParentsWinScreen.SetActive(true);
    }

    public void ParentLose()
    {
        ParentsLoseScreen.SetActive(true);
    }

    public void NeighboursLose()
    {
        MusicController.NeighboursLose();
        fbiTimeout = new ActionTimeout(3, () =>
        {
            fbiTimeout = null;
            FbiScreen.SetActive(true);
        });
    }

    public void AddPizdecPoints(int amount)
    {
        PizdecCurrentPoints = Mathf.Clamp(PizdecCurrentPoints + amount, 0, PizdecMaxPoints);
    }

    public void AddNeighboursPoints(int amount)
    {
        NeighboursCurrentPoints = Mathf.Clamp(NeighboursCurrentPoints + amount, 0, NeighboursMaxPoints);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        int sceneIndex = Mathf.Max(1,
            (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(sceneIndex);
    }
}