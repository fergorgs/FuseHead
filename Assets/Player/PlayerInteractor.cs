using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float overlapCircleRadius = 10f;
    [SerializeField] private PlayerInput _playerInput = null;

    private void Awake()
    {
        if(_playerInput == null)
            _playerInput = GetComponent<PlayerInput>();

        _playerInput.actions["Interact"].started += PlayerInteraction_started;
    }

    private void PlayerInteraction_started(InputAction.CallbackContext obj)
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, overlapCircleRadius);

        foreach(Collider2D collider2D in overlappedColliders)
        {
            if(collider2D.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, overlapCircleRadius);
    }

    private void OnDestroy()
    {
        _playerInput.actions["Interact"].started -= PlayerInteraction_started;
    }
}
