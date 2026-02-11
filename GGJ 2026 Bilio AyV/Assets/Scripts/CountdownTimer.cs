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
        GameManager.OnMatchStart += StartTimer;
    }

    void OnDisable()
    {
        GameManager.OnMatchStart -= StartTimer;
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
        
        if(time < startTimeInSeconds * 0.1f) //less than 10% of initial time remaining?
        {
            HitRedTimerThreshold();
        }
    }

    void HitRedTimerThreshold() //Timer should start appearing red
    {
        timerText.color = Color.red;
    }

    private void TimerEnd()
    {
        Debug.Log("it's over mah dude");
        GameManager.GMInstance.TriggerOnMatchEnd();
    }
}
