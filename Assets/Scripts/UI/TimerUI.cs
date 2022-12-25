using System;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Timer gameTimer = null;

    protected virtual void UpdateUI() { }

    protected virtual void Update()
    {
        UpdateUI();
    }

    protected float GetPassedTime() => gameTimer.GetTimeElapsed();
    protected float GetRemainingTime() => (gameTimer.CountdownTime - gameTimer.GetTimeElapsed());
    protected float GetNormalizedTime() => (gameTimer.GetTimeElapsed() / gameTimer.CountdownTime);
    protected float GetRemainingNormalizedTime() => (GetRemainingTime() / gameTimer.CountdownTime);

    protected virtual void SetReferences() => gameTimer = FindObjectOfType<Timer>();
}
