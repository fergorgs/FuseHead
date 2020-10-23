using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour
{
	public float horizontalForce = 0.07f, horizontalDrag = 0.01f, horizontalTol = 0.05f, jumpForce = 3f, respawnTime = 2, blowUpTime = 5f;
	public float ghostJumpTime = 0.25f, bufferJumpTime = 0.25f; 

	private bool grounded = true; public void SetGrounded(bool val) { grounded = val; }

	//public Vector2 spawnPoint = Vector2.zero;
	public GameObject explosion;

	private float r, g, b;

	private float iniTime, iniBufferJumpTime = -100000;

	private GameObject respawnController;

	// Start is called before the first frame update
	void Start()
	{
		r = GetComponent<SpriteRenderer>().color.r;
		g = GetComponent<SpriteRenderer>().color.g;
		b = GetComponent<SpriteRenderer>().color.b;

		iniTime = Time.time;
		StartCoroutine(BlowUpTimer());

		respawnController = GameObject.FindGameObjectWithTag("Respawn Controller");
	}

	// Update is called once per frame
	void Update()
	{
		//----------------------------------------------------------------------
		//------------------------------MOVEMENT--------------------------------
		//----------------------------------------------------------------------
		float xVel = GetComponent<Rigidbody2D>().velocity.x;
		float yVel = GetComponent<Rigidbody2D>().velocity.y;
		
		//Horizontal Movement--------------------------------------------------------------------------
		GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalForce * Input.GetAxis("Horizontal"), 0));

		if(Input.GetAxis("Horizontal") == 0)
		{
			if (Mathf.Abs(xVel) <= horizontalTol)
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, yVel);
		}
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(-horizontalDrag * xVel, 0));

		//Jump------------------------------------------------------------------------------------------

		if(!grounded && Input.GetAxis("Vertical") > 0)
			iniBufferJumpTime = Time.time;

		if (grounded)
		{
			if(Input.GetAxis("Vertical") > 0 || Time.time - iniBufferJumpTime < bufferJumpTime)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
				grounded = false;
			}
		}


		//----------------------------------------------------------------------
		//------------------------------BLOW UP---------------------------------
		//----------------------------------------------------------------------
		if (Input.GetKeyDown(KeyCode.S))
			BlowUp();

		//makes character more red as time passed (to be replaced by final animation)
		GetComponent<SpriteRenderer>().color = new Color(1, (1 - (Time.time - iniTime) / blowUpTime), (1 - (Time.time - iniTime) / blowUpTime), 1);
	}

	public void BlowUp()
	{
		Instantiate(explosion, transform.position, transform.rotation);

		respawnController.GetComponent<RespawnControllerScript>().RespawnPlayer(1, respawnTime);

		Destroy(gameObject);
	}

	IEnumerator BlowUpTimer()
	{
		yield return new WaitForSeconds(blowUpTime);

		BlowUp();
	}
}
