using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPause : MonoBehaviour
{
    [SerializeField] private BooleanEventSO PauseEvent = default;

    private PlayerInput _playerInput = default;
    private bool _paused = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Pause"].started += PlayerPause_started;

        _paused = PauseEvent.lastCall;
        PauseEvent?.Subscribe(OnPauseEvent);
        Paused();
    }

    private void OnPauseEvent(bool paused)
    {
        _paused = paused;
        Paused();
    }

    private void Paused()
    {
        if (_paused)
            _playerInput.actions.Disable();
        else
            _playerInput.actions.Enable();
    }

    private void PlayerPause_started(InputAction.CallbackContext obj)
    {
        PauseEvent.Invoke(!_paused);
    }

    private void OnDestroy()
    {
        PauseEvent?.Unsubscribe(OnPauseEvent);
    }
}