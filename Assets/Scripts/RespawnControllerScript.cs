using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnControllerScript : MonoBehaviour {
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;

	private PlayerInputManager _playerInputManager;
	private SpawnMultiplePlayers _playerSpawnManager;

	void Start() {
		_playerInputManager = GetComponent<PlayerInputManager>();
        _playerSpawnManager = GetComponent<SpawnMultiplePlayers>();
        _playerInputManager.playerPrefab = playerPrefab;
	}

	void OnDrawGizmos() {
		foreach(var point in respawnPoints){
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(point, .3f);
		}
	}
	public void RespawnPlayer(float secondsToRespawn, int playerId) {
		StartCoroutine(RespawnPlayerTimer(secondsToRespawn, playerId));
	}

	private IEnumerator RespawnPlayerTimer(float secondsToRespawn, int playerId) //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

        //PlayerInput player = _playerInputManager.JoinPlayer();
        PlayerInput player = _playerSpawnManager.SpawnPlayerByIndex(playerId);
		player.GetComponent<PlayerColor>().SetPlayerColor(playerId);
        SetupPlayer(player);
	}

    public void SetupPlayer(PlayerInput player)
    {
        Vector3 respPos = respawnPoints[Random.Range(0, respawnPoints.Length)];
        player.transform.position = respPos;

        PlayerBlowUp playerBlowUp = player.GetComponent<PlayerBlowUp>();
        playerBlowUp.OnBlowUp += () => {
            RespawnPlayer(playerBlowUp.respawnTime, player.playerIndex);
        };
    }

	[ContextMenu("SpawnPlayer")]
	public void SpawnPlayer() => RespawnPlayer(0f, -1);
}