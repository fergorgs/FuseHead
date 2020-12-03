﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float CountdownTime { get => countdownTime; private set => countdownTime = value; }
    public float TimeElapsed { get; private set; } = 0;

    public bool BeginTimerAtStart = true;
    public UnityEvent OnCountdownEnd = null;
    public BooleanEventSO PauseEvent = default;

    [SerializeField] private float countdownTime = 0;
    [SerializeField] private float timeScale = 1;

    private void Start()
    {
        if (BeginTimerAtStart)
            StartTimer();
        PauseEvent?.Subscribe(OnPauseEvent);
    }

    private void OnPauseEvent(bool paused)
    {
        if (paused)
            StopTimer();
        else
            ContinueTimer();
    }

    private void Update()
    {
        TimeElapsed += Time.deltaTime * timeScale;

        VerifyTime();
    }

    private void VerifyTime()
    {
        if(TimeElapsed >= countdownTime)
        {
            OnCountdownEnd?.Invoke();
            StopTimer();
        }
    }

    public void StartTimer()
    {
        TimeElapsed = 0;
        if (timeScale == 0f)
            ContinueTimer();
    }

    public void StartTimer(float countdownTime)
    {
        this.countdownTime = countdownTime;
        StartTimer();
    }

    public void ContinueTimer()
    {
        timeScale = 1f;
        Time.timeScale = timeScale;
    }

    public void StopTimer()
    {
        timeScale = 0f;
        Time.timeScale = timeScale;
    }

    public void UpdateTimeScale(float scale)
    {
        timeScale = scale;
        Time.timeScale = timeScale;
    }

    private void OnDestroy()
    {
        PauseEvent?.Unsubscribe(OnPauseEvent);
        Time.timeScale = 1f;
    }
}
