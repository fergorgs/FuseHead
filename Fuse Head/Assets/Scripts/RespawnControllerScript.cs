using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControllerScript : MonoBehaviour {
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;

	void Start() {
		RespawnPlayer(1, 0);
	}
	public void RespawnPlayer(int playerID, float secondsToRespawn) {
		StartCoroutine(RespawnPlayerTimer(playerID, secondsToRespawn));
	}

	private IEnumerator RespawnPlayerTimer(int playerID, float secondsToRespawn) //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

		Vector3 respPos = respawnPoints[Random.Range(0, respawnPoints.Length)];

		var player = Instantiate(playerPrefab, respPos, Quaternion.identity);
		player.GetComponent<PlayerBlowUp>().OnBlowUp += () => {
			RespawnPlayer(playerID, secondsToRespawn);
		};
	}
}