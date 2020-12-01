using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBlowUp : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Explosion")
			BlowUp();
	}

    public void BlowUp() {
        Instantiate(explosion, transform.position, transform.rotation);



        Destroy(gameObject);
    }
}
