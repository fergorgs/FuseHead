using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerJoinUI : MonoBehaviour
{
    [SerializeField] private InputAction joinAction = default;
    [SerializeField] private InputAction leaveAction = default;
    [SerializeField] private InputActionAsset playerActionAsset = default;

    private int _joinedIndex = -1;
    Dictionary<InputDevice, int> _joinedPlayersSlot = new Dictionary<InputDevice, int>(4);

    private void Awake()
    {
        joinAction.canceled += OnJoin;
        leaveAction.canceled += OnLeave;

        joinAction.Enable();
        leaveAction.Enable();
    }

    private void OnLeave(InputAction.CallbackContext inputCtx)
    {
        Debug.Log("Trying to leave");
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
        foreach(var pair in _joinedPlayersSlot)
        {
            if (_joinedPlayersSlot[pair.Key] > leftIndex)
                _joinedPlayersSlot[pair.Key] = pair.Value - 1;
        }

        _joinedIndex--;
    }

    private void OnJoin(InputAction.CallbackContext inputCtx)
    {
        Debug.Log("Trying to join");
        // Check Device Compability
        if (!playerActionAsset.actionMaps[0].IsUsableWithDevice(inputCtx.control.device))
            return;

        // Check if device has already joined
        if (_joinedPlayersSlot.ContainsKey(inputCtx.control.device))
            return;

        // Search Available Slot
        if (_joinedIndex >= 3)
            return;
        _joinedIndex++;

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
            child.GetComponent<Image>().color = Color.white;
            child.gameObject.SetActive(true);
        }
    }


}
