using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastResistence : MonoBehaviour
{
	public float blastResistence = 10;
	public float curLife;

	public Sprite fullLifeSprite, middleLifeSprite, lowLifeSprite;

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

		if (curLife > (blastResistence * 0.66f))
			GetComponent<SpriteRenderer>().sprite = fullLifeSprite;
		else if (curLife > (blastResistence * 0.33f))
			GetComponent<SpriteRenderer>().sprite = middleLifeSprite;
		else
			GetComponent<SpriteRenderer>().sprite = lowLifeSprite;

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Name: " + collision.gameObject.name);
		if (collision.gameObject.tag == "Explosion")
			Degrade(1);
	}

	public void Degrade(int points)
	{
		curLife -= points;
		float degRat = curLife / blastResistence;
		GetComponent<SpriteRenderer>().color = new Color(r* degRat, g* degRat, b* degRat, 1);
	}
}
