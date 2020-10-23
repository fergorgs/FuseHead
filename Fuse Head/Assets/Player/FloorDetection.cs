using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
	public GameObject parent;

	private float ghostJumpTime;

	// Start is called before the first frame update
	void Start()
	{
		ghostJumpTime = parent.GetComponent<PlayerControlls>().ghostJumpTime;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject != parent)
		{
			StopAllCoroutines();
			parent.GetComponent<PlayerControlls>().SetGrounded(true);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject != parent)
			StartCoroutine(SetGroundedToFalse());
	}

	private IEnumerator SetGroundedToFalse()
	{
		yield return new WaitForSeconds(ghostJumpTime);

		parent.GetComponent<PlayerControlls>().SetGrounded(false);

	}
}
