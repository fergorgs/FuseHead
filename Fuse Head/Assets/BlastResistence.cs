using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastResistence : MonoBehaviour
{
	public float blastResistence = 10;
	public float curLife;

	private float r;
	private float g;
	private float b;

	// Start is called before the first frame update
	void Start()
    {
		curLife = blastResistence;

		r = GetComponent<SpriteRenderer>().color.r;
		g = GetComponent<SpriteRenderer>().color.g;
		b = GetComponent<SpriteRenderer>().color.b;
	}

    // Update is called once per frame
    void Update()
    {
		if (curLife <= 0)
			Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Name: " + collision.gameObject.name);
		if (collision.gameObject.tag == "Explosion")
			Degrade(2);
	}

	public void Degrade(int points)
	{
		curLife -= points;
		float degRat = curLife / blastResistence;
		GetComponent<SpriteRenderer>().color = new Color(r* degRat, g* degRat, b* degRat, 1);
	}
}
