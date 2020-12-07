using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EndOfLevelUI : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private SceneController sceneController = null;
    [SerializeField] private EventSystem eventSystem = null;

    [Header("Prefab References")]
    [SerializeField] private Button _retryBtn, _nextLevelBtn, _mainMenuBtn = null;
    [SerializeField] private TextMeshProUGUI _headerText;


    private void Awake()
    {
        Debug.Assert(sceneController != null, "Please set a reference to Scene Controller");
        Debug.Assert(eventSystem != null, "Please set a reference to Event System");

        _retryBtn?.onClick.AddListener(() => sceneController.ReloadActiveSceneWithTransition());
        _mainMenuBtn?.onClick.AddListener(() => sceneController.LoadSceneWithTransition(1));
        _nextLevelBtn?.onClick.AddListener(() => sceneController.LoadNextSceneWithTransition());
    }

    private void DisableNextLevelButton()
    {
        Color new_color = _nextLevelBtn.GetComponent<Image>().color;
        new_color.a = 0.5f;
        _nextLevelBtn.GetComponent<Image>().color = new_color;
        _nextLevelBtn.enabled = false;
        _nextLevelBtn.interactable = false;
    }

    public void OnVictory()
    {
        _headerText.SetText("Victory");
        if (!sceneController.IsNextLevelAvailable())
        {
            DisableNextLevelButton();
            eventSystem.SetSelectedGameObject(_mainMenuBtn.gameObject);
        }
        else
        {
            eventSystem.SetSelectedGameObject(_nextLevelBtn.gameObject);
        }

    }

    public void OnGameOver()
    {
        _headerText.SetText("Defeat");
        DisableNextLevelButton();
        eventSystem.SetSelectedGameObject(_retryBtn.gameObject);

    }

#if UNITY_EDITOR
    [ContextMenu("Cache References")]
    private void CacheReferences()
    {
        _retryBtn = transform.Find(nameof(_retryBtn)).GetComponent<Button>();
        _nextLevelBtn = transform.Find(nameof(_nextLevelBtn)).GetComponent<Button>();
        _mainMenuBtn = transform.Find(nameof(_mainMenuBtn)).GetComponent<Button>();

        _headerText = transform.Find(nameof(_headerText)).GetComponent<TextMeshProUGUI>();
    }
#endif
}
