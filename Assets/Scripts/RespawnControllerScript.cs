using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnControllerScript : NetworkBehaviour
{
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;

	private PlayerInputManager _playerInputManager;
	private SpawnMultiplePlayers _playerSpawnManager;

	private List<Vector3>[] playerSpawnPoints = new List<Vector3>[4];

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

	public void SetupRespawnPointsToPlayers(int playerCount) {
		for (int i = 0; i < playerCount; i++) {
			playerSpawnPoints[i] = new List<Vector3>();
        }

		int p = 0;
		foreach (var point in respawnPoints) {
			playerSpawnPoints[p % playerCount].Add(point);
			p++;
        }
    }

	private IEnumerator RespawnPlayerTimer(float secondsToRespawn, int playerId) //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

		if (playerSpawnPoints[playerId].Count != 0)
		{
			PlayerInput player = _playerSpawnManager.SpawnPlayerByIndex(playerId);
			SetupPlayer(player, playerId);
		}
	}

    public void SetupPlayer(PlayerInput player, int playerId)
    {
        if(playerSpawnPoints[playerId] == null || playerSpawnPoints[playerId].Count == 0)
        {
            Destroy(player.gameObject);
            return;
        }
		Vector3 respPos = playerSpawnPoints[playerId][0];
		playerSpawnPoints[playerId].RemoveAt(0);
		playerSpawnPoints[playerId].Add(respPos);

        player.transform.position = respPos;

		player.GetComponent<PlayerColor>().SetPlayerColor(playerId);

		PlayerBlowUp playerBlowUp = player.GetComponent<PlayerBlowUp>();
        playerBlowUp.OnBlowUp += () => {
            RespawnPlayer(playerBlowUp.respawnTime, player.playerIndex);
        };
    }

	[ContextMenu("SpawnPlayer")]
	public void SpawnPlayer() => RespawnPlayer(0f, -1);
}