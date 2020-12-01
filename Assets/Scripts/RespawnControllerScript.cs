﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnControllerScript : MonoBehaviour {
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;

	private PlayerInputManager _playerInputManager;

	void Start() {
		_playerInputManager = GetComponent<PlayerInputManager>();
		_playerInputManager.playerPrefab = playerPrefab;
	}

	void OnDrawGizmos() {
		foreach(var point in respawnPoints){
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(point, .3f);
		}
	}
	public void RespawnPlayer(float secondsToRespawn) {
		StartCoroutine(RespawnPlayerTimer(secondsToRespawn));
	}

	private IEnumerator RespawnPlayerTimer(float secondsToRespawn) //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

		Vector3 respPos = respawnPoints[Random.Range(0, respawnPoints.Length)];

		//var player = Instantiate(playerPrefab, respPos, Quaternion.identity);
		PlayerInput player = _playerInputManager.JoinPlayer();
		player.transform.position = respPos;
        PlayerBlowUp playerBlowUp = player.GetComponent<PlayerBlowUp>();
		playerBlowUp.OnBlowUp += () => {
			RespawnPlayer(playerBlowUp.respawnTime);
		};

	}

	[ContextMenu("SpawnPlayer")]
	public void SpawnPlayer() => RespawnPlayer(0f);
}