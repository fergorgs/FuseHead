﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnControllerScript : MonoBehaviour {
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;
	public PlayerInputManager playerInputManager;

	void Start() {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerPrefab;
        SpawnPlayer();
        //RespawnPlayer(0.1f);
		//RespawnPlayer(0);
	}

    
	public void RespawnPlayer(float secondsToRespawn) {
		StartCoroutine(RespawnPlayerTimer(secondsToRespawn));
	}

	private IEnumerator RespawnPlayerTimer(float secondsToRespawn) //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

		Vector3 respPos = respawnPoints[Random.Range(0, respawnPoints.Length)];

        //var player = Instantiate(playerPrefab, respPos, Quaternion.identity);
        var player = playerInputManager.JoinPlayer();
        player.transform.position = respPos;

        player.GetComponent<PlayerBlowUp>().OnBlowUp += () => {
            RespawnPlayer(secondsToRespawn);
        };
		
	}

    [ContextMenu("SpawnPlayer")]
    public void SpawnPlayer() => RespawnPlayer(0f);
}