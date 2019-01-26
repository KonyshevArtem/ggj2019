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
        image.fillAmount = 0;
    }

    void Update()
    {
        if (isPlaying)
        {
            image.fillAmount = currentTime / totalTime;
            currentTime += Time.deltaTime;
            if (currentTime >= totalTime)
                StopAnim();
        }
    }

    public void StartAnim(float seconds)
    {
        currentTime = 0;
        if (image != null)
            image.fillAmount = 0;
        totalTime = seconds;
        isPlaying = true;
        gameObject.SetActive(true);
    }

    public void StopAnim()
    {
        isPlaying = false;
        gameObject.SetActive(false);
    }
}