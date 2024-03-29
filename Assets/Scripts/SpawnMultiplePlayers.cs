using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpawnMultiplePlayers : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager = default;
    private RespawnControllerScript _respawner = null;
    private GameObject playerPrefab = null;

    private Dictionary<int, InputDevice> playerIndexToDevice;

    public void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        _respawner = GetComponent<RespawnControllerScript>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerPrefab = _respawner.playerPrefab;
        SpawnJoinedPlayers();
        //SpawnAllConnectedPlayers();
    }

    public void SpawnAllConnectedPlayers()
    {
        var playerActionMap = playerPrefab.GetComponent<PlayerInput>().actions.actionMaps[0];
        foreach(var device in InputSystem.devices)
        {
            if(playerActionMap.IsUsableWithDevice(device))
            {
                _respawner.SpawnPlayer();
            }
        }
    }

    public void SpawnJoinedPlayers()
    {
        int numPlayers = PlayerPrefs.GetInt("NumPlayers", 0);
        _respawner.SetupRespawnPointsToPlayers(numPlayers);


        if (numPlayers <= 0)
        {
            Debug.LogError("No Player Amount on Prefs");
            return;
        }

        playerIndexToDevice = new Dictionary<int, InputDevice>(numPlayers);

        for (int i = 0; i < numPlayers; i++)
        {
            int deviceId = PlayerPrefs.GetInt($"player_{i}", -1);
            if(deviceId != -1)
            {
                InputDevice device = InputSystem.GetDeviceById(deviceId);
                PlayerInput player = playerInputManager.JoinPlayer(playerIndex: i, pairWithDevice: device);
                playerIndexToDevice[player.playerIndex] = device;
                _respawner.SetupPlayer(player, player.playerIndex);
            }
        }
    }

    public PlayerInput SpawnPlayerByIndex(int index)
    {
        InputDevice device = playerIndexToDevice[index];
        return playerInputManager.JoinPlayer(playerIndex: index, pairWithDevice: device);
    }
}