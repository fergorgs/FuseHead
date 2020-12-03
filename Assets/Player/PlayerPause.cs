using RoboRyanTron.Unite2017.Events;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPause : MonoBehaviour
{
    [SerializeField] private GameEvent PauseEvent = default;

    private PlayerInput _playerInput = default;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnPauseEvent()
    {
        _playerInput.actions.Disable();
    }

    public void OnUnpauseEvent()
    {
        _playerInput.actions.Enable();
    }

    private void PlayerPause_started(InputAction.CallbackContext obj)
    {
        PauseEvent?.Raise();
    }

    private void OnEnable()
    {
        _playerInput.actions["Pause"].started += PlayerPause_started;
    }

    private void OnDisable()
    {
        _playerInput.actions["Pause"].started -= PlayerPause_started;
    }
}