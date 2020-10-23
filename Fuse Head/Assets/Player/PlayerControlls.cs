using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour {
	public float runSpeed = 40f, horizontalMove;

	private float iniTime;

	public PlayerBlowUp playerBlow;
	private bool jump = false;
	public SpriteRenderer sprite;

	public CharacterController2D characterController;
	// Start is called before the first frame update
	void Start() {
		iniTime = Time.time;
	}

	void FixedUpdate() {
		characterController.Move(horizontalMove, jump);
		jump = false;
	}
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.S))
			playerBlow.BlowUp();
		
		if (Input.GetButtonDown("Jump")) {
			jump = true;
		}
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		//makes character more red as time passed (to be replaced by final animation)
		//sprite.color = new Color(1, (1 - (Time.time - iniTime) / blowUpTime), (1 - (Time.time - iniTime) / blowUpTime), 1);
	}

}