﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransport : MonoBehaviour, IInteractable {
    [SerializeField] private Teletransport target;

    public bool isTarget = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isTarget) {
            Debug.Log("Entrou");
            target.isTarget = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(isTarget) isTarget = false;
    }

    public void Interact(GameObject player)
    {
        player.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 1f);
    }
}
