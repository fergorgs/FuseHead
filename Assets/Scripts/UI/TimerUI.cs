using System;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Timer gameTimer = null;

    protected virtual void UpdateUI() { Debug.Log("UpdateUI of Base class"); }

    protected virtual void Update()
    {
        UpdateUI();
    }

    protected float GetPassedTime() => gameTimer.TimeElapsed;
    protected float GetRemainingTime() => (gameTimer.CountdownTime - gameTimer.TimeElapsed);
    protected float GetNormalizedTime() => (gameTimer.TimeElapsed / gameTimer.CountdownTime);
    protected float GetRemainingNormalizedTime() => (GetRemainingTime() / gameTimer.CountdownTime);

    protected virtual void SetReferences() => gameTimer = FindObjectOfType<Timer>();
}
