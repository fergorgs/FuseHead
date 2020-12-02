using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerJoinUI : MonoBehaviour
{
    [SerializeField] private InputAction joinAction = default;
    [SerializeField] private InputAction leaveAction = default;
    [SerializeField] private InputActionAsset playerActionAsset = default;
    [SerializeField] private Button joinBtn = default;
    [SerializeField] private EventSystem eventSystem = default;

    private int _joinedIndex = -1;

    Dictionary<InputDevice, int> _joinedPlayersSlot = new Dictionary<InputDevice, int>(4);

    private void Awake()
    {
        SetJoinedIndex(-1);
    }

    private void OnLeave(InputAction.CallbackContext inputCtx)
    {
        // Check if it is joined
        if (!_joinedPlayersSlot.ContainsKey(inputCtx.control.device))
            return;

        // Get Index
        int leftIndex = _joinedPlayersSlot[inputCtx.control.device];
        
        // Remove From Dictionary
        _joinedPlayersSlot.Remove(inputCtx.control.device);

        // Deselect Slot and shift players image
        int i = 0;
        foreach (Transform leftObj in transform)
        {
            if (i < _joinedIndex)
                SetImageToSelected(leftObj);
            else
                SetImageToDeselected(leftObj);
            i++;
        }

        // Shift player slot in dictionary
        var dictionaryCopy = new Dictionary<InputDevice, int>(4);
        foreach (var pair in _joinedPlayersSlot)
        {
            if (pair.Value > leftIndex)
                dictionaryCopy[pair.Key] = pair.Value - 1;
            else
                dictionaryCopy[pair.Key] = pair.Value;
        }
        _joinedPlayersSlot.Clear();
        _joinedPlayersSlot = dictionaryCopy;

        SetJoinedIndex(_joinedIndex - 1);
    }

    private void OnJoin(InputAction.CallbackContext inputCtx)
    {
        // Check if Join Button is selected
        if (!eventSystem.currentSelectedGameObject.name.Equals(joinBtn.name))
            return;

        // Check Device Compability
        if (!playerActionAsset.actionMaps[0].IsUsableWithDevice(inputCtx.control.device))
            return;

        // Check if device has already joined
        if (_joinedPlayersSlot.ContainsKey(inputCtx.control.device))
            return;

        // Search Available Slot
        if (_joinedIndex >= 3)
            return;

        SetJoinedIndex(_joinedIndex + 1);

        // Assign player to slot
        _joinedPlayersSlot[inputCtx.control.device] = _joinedIndex;

        // Change UI to show assigned player
        int i = 0;
        foreach (Transform joinObj in transform)
        {
            if(i == _joinedIndex)
            {
                SetImageToSelected(joinObj);
                break;
            }
            i++;
        }
    }

    private void SetJoinedIndex(int index)
    {
        _joinedIndex = index;
        PlayerPrefs.SetInt("NumPlayers", _joinedIndex + 1);
    }

    // TODO:
    // Separate Active Player Display (make new component)
    // Visualize Device Layout for joined players
    private void SetImageToDeselected(Transform obj)
    {
        obj.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        foreach (Transform child in obj)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void SetImageToSelected(Transform obj)
    {
        obj.GetComponent<Image>().color = Color.white;
        foreach (Transform child in obj)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        joinAction.canceled += OnJoin;
        leaveAction.canceled += OnLeave;

        joinAction.Enable();
        leaveAction.Enable();
    }

    private void OnDisable()
    {
        joinAction.canceled -= OnJoin;
        leaveAction.canceled -= OnLeave;

        joinAction.Disable();
        leaveAction.Disable();
    }
}