using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Timer : NetworkBehaviour
{
    public float CountdownTime { get => countdownTime; private set => countdownTime = value; }
    //public float TimeElapsed { get; private set; } = 0;
    private NetworkVariable<float> TimeElapsed = new NetworkVariable<float>(0);
    public float GetTimeElapsed() { return TimeElapsed.Value; }

    public bool BeginTimerAtStart = true;
    public UnityEvent OnCountdownEnd = null;

    [SerializeField] private float countdownTime = 0;
    [SerializeField] private float timeScale = 1;

    private void Start()
    {
        if (BeginTimerAtStart)
            StartTimer();
    }

    public void OnPauseEvent()
    {
        StopTimer();
    }

    public void OnUnpauseEvent()
    {
        ContinueTimer();
    }

    private void Update()
    {
        if( !IsHost ) { return; }
        TimeElapsed.Value += Time.deltaTime * timeScale;

        VerifyTime();
    }

    private void VerifyTime()
    {
        if(TimeElapsed.Value >= countdownTime && timeScale > float.Epsilon)
        {
            OnCountdownEnd?.Invoke();
            StopTimer();
        }
    }

    public void StartTimer()
    {
        TimeElapsed.Value = 0;
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

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
