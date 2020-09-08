using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlls : MonoBehaviour
{
	public float speed = 1f, jump = 3f, drag = 0.01f, linearTol = 0.05f, respawnTime = 2, blowUpTime = 5f;

	public bool grounded = true;

	public Vector2 spawnPoint = Vector2.zero;
	public GameObject explosion;

	private bool alive = true;

	private float r, g, b;

	private float iniTime;

	// Start is called before the first frame update
	void Start()
	{
		r = GetComponent<SpriteRenderer>().color.r;
		g = GetComponent<SpriteRenderer>().color.g;
		b = GetComponent<SpriteRenderer>().color.b;

		iniTime = Time.time;
		StartCoroutine(BlowUpTimer());
	}

	// Update is called once per frame
	void Update()
	{
		float xVel = GetComponent<Rigidbody2D>().velocity.x;
		float yVel = GetComponent<Rigidbody2D>().velocity.y;

		if (alive)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
				GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, yVel);
			else if (Input.GetKey(KeyCode.RightArrow))
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, yVel);
			else if (xVel > linearTol)
				GetComponent<Rigidbody2D>().velocity -= new Vector2(drag, 0);
			else if (xVel < -linearTol)
				GetComponent<Rigidbody2D>().velocity += new Vector2(drag, 0);

			if (Input.GetKey(KeyCode.UpArrow) && grounded)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, jump);
				grounded = false;
			}

			if (Input.GetKeyDown(KeyCode.S))
				BlowUp();

			GetComponent<SpriteRenderer>().color = new Color(1, (1-(Time.time-iniTime)/blowUpTime), (1 - (Time.time - iniTime) / blowUpTime), 1);
		}

		if (Input.GetKeyUp(KeyCode.R))
		{
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}
	}

	public void BlowUp()
	{
		Instantiate(explosion, transform.position, transform.rotation);

		GetComponent<SpriteRenderer>().enabled = false;
		alive = false;
		transform.position = new Vector3(spawnPoint.x, spawnPoint.y, transform.position.z);

		StopAllCoroutines();
		//StopCoroutine(BlowUpTimer());
		StartCoroutine(Respawn());
	}

	IEnumerator BlowUpTimer()
	{
		yield return new WaitForSeconds(blowUpTime);

		BlowUp();
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(respawnTime);
		GetComponent<SpriteRenderer>().enabled = true;
		alive = true;
		iniTime = Time.time;
		StartCoroutine(BlowUpTimer());
	}
}
