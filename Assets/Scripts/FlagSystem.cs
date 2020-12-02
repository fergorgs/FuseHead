using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class FlagSystem : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnGameVictory = new UnityEvent();

    [Header("Flags")]
    [Tooltip("Add the flags in scene you want to be considered for winning. Consider using contex menu Get Flags in Scene to automate")]
    [SerializeField] private FlagObject[] _flags = null;
    private int _remainingFlags = 0;

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
            OnGameVictory?.Invoke();
            Debug.Log("Game Won");
        }
    }

    private void Start()
    {
        Setup();
    }
}
