using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent OnCountdownEnd = null;
    public float CountdownTime { get => _countdownTime; private set => _countdownTime = value; }
    public float TimeElapsed { get; private set; } = 0;

    [SerializeField] private float _countdownTime = 0;
    [SerializeField] private float timeScale = 1;

    private void Update()
    {
        TimeElapsed += Time.deltaTime * timeScale;

        VerifyTime();
    }

    private void VerifyTime()
    {
        if(TimeElapsed >= _countdownTime)
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
        _countdownTime = countdownTime;
        StartTimer();
    }

    public void ContinueTimer() => timeScale = 1;
    public void StopTimer() => timeScale = 0f;
    public void UpdateTimeScale(float scale) => timeScale = scale;
}
