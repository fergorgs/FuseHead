using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    public BooleanEventSO PauseEvent = default;
    [SerializeField] private GameObject pauseMenuContainer = default;
    [SerializeField] private GameObject firstSelectedObject = default;
    [SerializeField] private EventSystem eventSystem = default;

    private void Awake()
    {
        PauseEvent?.Subscribe(OnPauseEvent);
    }

    public void CloseMenu()
    {
        PauseEvent.Invoke(false);
    }

    private void OnPauseEvent(bool paused)
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
