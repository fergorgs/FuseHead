using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour
{
    public PlayerInput playerInput;
    public float runSpeed = 40f, horizontalMove;
	public PlayerBlowUp playerBlow;


	private bool jump = false;

	public CharacterController2D characterController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Jump"].started += OnJump;
        playerInput.actions["BlowUp"].started += OnBlowUp;
    }

    private void OnBlowUp(InputAction.CallbackContext obj)
    {
        playerBlow.BlowUp();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        jump = true;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        horizontalMove = ctx.ReadValue<float>() * runSpeed;
    }

    void FixedUpdate()
    {
		characterController.Move(horizontalMove, jump);
		jump = false;
	}

    private void OnEnable()
    {
        playerInput.ActivateInput();
    }

    private void OnDisable()
    {
        playerInput.DeactivateInput();
        playerInput.actions["Move"].performed -= OnMovePerformed;
        playerInput.actions["Jump"].started -= OnJump;
        playerInput.actions["BlowUp"].started -= OnBlowUp;
    }
}
