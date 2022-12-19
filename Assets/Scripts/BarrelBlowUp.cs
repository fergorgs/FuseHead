using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BarrelBlowUp : NetworkBehaviour {

    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Explosion")
			BlowUp();
	}

    public void BlowUp() {
        //Instantiate(explosion, transform.position, transform.rotation);
        SpawnExplosionServerRpc(transform.position, transform.rotation);
        Destroy(gameObject);
    }

    [ServerRpc]
    private void SpawnExplosionServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        spawnedExplosion.GetComponent<NetworkObject>().Spawn(true);
    }
}
