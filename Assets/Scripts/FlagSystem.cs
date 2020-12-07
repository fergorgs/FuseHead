using System;
using System.Collections;
using UnityEngine;
using RoboRyanTron.Unite2017.Events;

public class FlagSystem : MonoBehaviour
{
    [Header("Events")]
    public GameEvent OnVictoryEvent = default;

    [Header("Flags")]
    [Tooltip("Add the flags in scene you want to be considered for winning. Consider using contex menu Get Flags in Scene to automate")]
    [SerializeField] private FlagObject[] _flags = null;
    private int _remainingFlags = 0;

    [Header("Timer")]
    [SerializeField] private float winAnimationTime = 1.0f;

    [ContextMenu("Get Flags in Scene")]
    public void GetFlagsInScene()
    {
        _flags = FindObjectsOfType<FlagObject>();
#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
    }

    public void Setup()
    {
        if(_flags == null || _flags.Length == 0)
        {
            GetFlagsInScene();
            Debug.LogWarning($"Flag System {name} had no flags in array");
        }

        _remainingFlags = _flags.Length;
        foreach(var flag in _flags)
        {
            flag.OnFlagCaptured += FlagCaptured;
        }
    }

    private void FlagCaptured()
    {
        _remainingFlags--;
        if (_remainingFlags <= 0)
        {
            StartCoroutine(WinScreen());
            Debug.Log("Game Won");
        }
    }

    private IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(2.0f);

        OnVictoryEvent?.Raise();
    }

    private void Start()
    {
        Setup();
    }
}
