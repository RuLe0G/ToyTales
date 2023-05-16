using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool isTimerRunning = false;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    private void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    private void UpdateTimerText(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SaveTimer()
    {
        PlayerPrefs.SetFloat("SavedTime1", Time.time - startTime);
    }

    private void LoadTimer()
    {
        float savedTime = PlayerPrefs.GetFloat("SavedTime1", 0f);
        startTime = Time.time - savedTime;
    }
}
