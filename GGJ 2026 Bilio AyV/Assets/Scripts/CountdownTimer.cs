using UnityEngine;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startTimeInSeconds = 60f;

    private float remainingTime;
    private bool isRunning = false;

    void OnEnable()
    {
        //RoundManager.OnRoundStart += StartTimer;
    }

    void OnDisable()
    {
        //RoundManager.OnRoundStart -= StartTimer;
    }

    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        SetTimer(startTimeInSeconds);
    }

    void Update()
    {
        if (!isRunning) return;

        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay(remainingTime);
        }
        else
        {
            isRunning = false;
            UpdateTimerDisplay(0f);
            TimerEnd();
        }
    }

    public void SetTimer(float seconds)
    {
        remainingTime = seconds;
        UpdateTimerDisplay(remainingTime);
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void UpdateTimerDisplay(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        timerText.text = string.Format("{0}:{1:D2}", t.Minutes, t.Seconds);
    }

    private void TimerEnd()
    {
        Debug.Log("it's over mah dude");
    }
}
