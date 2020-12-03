using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private GameEvent UnpauseEvent = default;

    [Header("Scene References")]
    [SerializeField] private EventSystem eventSystem = default;
    [SerializeField] private SceneController sceneController = default;

    [Header("Prefab References")]
    [SerializeField] private GameObject pauseMenuContainer = default;
    [SerializeField] private GameObject firstSelectedObject = default;

    private void Awake()
    {
        Debug.Assert(eventSystem != null, "Please set a reference to Event System");
        Debug.Assert(sceneController != null, "Please set a reference to Scene Controller");
    }

    public void CloseMenu()
    {
        UnpauseEvent.Raise();
    }

    public void GoToMainMenu()
    {
        sceneController.LoadSceneWithTransition(1);
    }

    public void RetryLevel()
    {
        sceneController.ReloadActiveSceneWithTransition();
    }

    public void OnPauseEvent(bool paused)
    {
        if (paused)
        {
            pauseMenuContainer.SetActive(true);
            eventSystem.SetSelectedGameObject(firstSelectedObject);
        }
        else
        {
            pauseMenuContainer.SetActive(false);
        }
    }
}
