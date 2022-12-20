using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour {

    public float moveSpeed = 0.5f;

    // Update is called once per frame
    void Update() {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //if (collider.gameObject.CompareTag("Player"))
         //   collider.gameObject.GetComponent<PlayerBlowUp>().BlowUp();
        DespawnBulletServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnBulletServerRpc() {
        Destroy(gameObject);
    }
}
