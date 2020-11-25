using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float CountdownTime { get => _countdownTime; private set => _countdownTime = value; }
    public float TimeElapsed { get; private set; } = 0;
    public bool IsPaused { get; private set; } = true;

    public bool BeginTimerAtStart = true;
    public UnityEvent OnCountdownEnd = null;

    [SerializeField] private float _countdownTime = 0;
    [SerializeField] private float timeScale = 1;

    private void Start()
    {
        if (BeginTimerAtStart)
            StartTimer();
    }

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
        IsPaused = false;
    }

    public void StartTimer(float countdownTime)
    {
        _countdownTime = countdownTime;
        StartTimer();
    }

    public void ContinueTimer()
    {
        timeScale = 1;
        IsPaused = false;
    }

    public void StopTimer()
    {
        timeScale = 0f;
        IsPaused = true;
    }

    public void UpdateTimeScale(float scale) => timeScale = scale;
}
