using UnityEngine;
using UnityEngine.UI;

public class TimerIconAnimation : MonoBehaviour
{
    private Image image;

    private float totalTime, currentTime;
    private bool isPlaying;

    void Start()
    {
        image = GetComponent<Image>();
    }
    
    void Update()
    {
        if (isPlaying)
        {
            image.fillAmount = currentTime / totalTime;
            currentTime += Time.deltaTime;
            if (currentTime >= totalTime)
            {
                isPlaying = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void StartAnim(float seconds)
    {
        currentTime = 0;
        totalTime = seconds;
        isPlaying = true;
        gameObject.SetActive(true);
    }
}