using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpawnMultiplePlayers : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager = default;
    private RespawnControllerScript _respawner = null;
    private GameObject playerPrefab = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcamera;
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

        if (numPlayers <= 0)
        {
            Debug.LogError("No Player Amount on Prefs");
            return;
        }

        playerIndexToDevice = new Dictionary<int, InputDevice>(numPlayers);

        for (int i = 0; i < numPlayers; i++)
        {
            int id = PlayerPrefs.GetInt($"player_{i}", -1);
            if(id != -1)
            {
                InputDevice device = InputSystem.GetDeviceById(id);
                PlayerInput player = playerInputManager.JoinPlayer(playerIndex: i, pairWithDevice: device);

                playerIndexToDevice[player.playerIndex] = device;
                _respawner.SetupPlayer(player);
            }
        }
    }

    public PlayerInput SpawnPlayerByIndex(int index)
    {   
        var player = playerInputManager.JoinPlayer(0);
        vcamera.Follow = player.transform;
        return player;
        /*InputDevice device = playerIndexToDevice[index];
        return playerInputManager.JoinPlayer(playerIndex: index, pairWithDevice: device);*/
    }
}