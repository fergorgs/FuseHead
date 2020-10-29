using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnMultiplePlayers : MonoBehaviour
{
    private RespawnControllerScript _respawner = null;
    private GameObject playerPrefab = null;

    public void Start()
    {
        _respawner = GetComponent<RespawnControllerScript>();
        playerPrefab = _respawner.playerPrefab;
        SpawnAllConnectedPlayers();
    }

    public void SpawnAllConnectedPlayers()
    {
        var playerActionMap = playerPrefab.GetComponent<PlayerInput>().actions.actionMaps[0];
        foreach(var device in InputSystem.devices)
        {
            if(playerActionMap.IsUsableWithDevice(device))
            {
                Debug.Log("Connecting player on " + device.displayName);
                _respawner.SpawnPlayer();
            }
        }
    }
}