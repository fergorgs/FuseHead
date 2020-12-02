using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToLevelSelectionButton : MonoBehaviour
{
    [SerializeField] private ChangeScreenButton ChangeToSinglePlayer = default;
    [SerializeField] private ChangeScreenButton ChangeToMultiPlayer = default;

    private Button _button = default;
    private Image _buttonImg = default;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonImg = GetComponent<Image>();

        _button.onClick.AddListener(CheckScreenChange);
    }

    private void CheckScreenChange()
    {
        int numPlayers = PlayerPrefs.GetInt("NumPlayers", 0);
        if(numPlayers == 1)
        {
            ChangeToSinglePlayer.ChangeScreen();
        }
        else if(numPlayers >= 1)
        {
            ChangeToMultiPlayer.ChangeScreen();
        }
        else
        {
            StartCoroutine(ButtonErrorColor());
        }
    }

    private IEnumerator ButtonErrorColor()
    {
        _buttonImg.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _buttonImg.color = Color.white;
    }
}
