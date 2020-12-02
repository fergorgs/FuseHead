using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ChangeScreenButton : MonoBehaviour
{
    [SerializeField] private GameObject currentScreen = default;
    [SerializeField] private GameObject nextScreen = default;
    [SerializeField] private GameObject nextScreenSelectedObject = default;
    [SerializeField] private EventSystem eventSystem = default;

    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeScreen);
    }

    private void ChangeScreen()
    {
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(nextScreenSelectedObject);
    }
}
