using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameEvent UnpauseEvent = default;
    [SerializeField] private GameObject pauseMenuContainer = default;
    [SerializeField] private GameObject firstSelectedObject = default;
    [SerializeField] private EventSystem eventSystem = default;

    public void CloseMenu()
    {
        UnpauseEvent.Raise();
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
