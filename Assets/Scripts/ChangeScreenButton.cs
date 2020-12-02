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
    [SerializeField] private bool AutoAddListener = true;

    private void Awake()
    {
        if(AutoAddListener)
            GetComponent<Button>().onClick.AddListener(ChangeScreen);
    }

    public void ChangeScreen()
    {
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(nextScreenSelectedObject);
    }
    
}
