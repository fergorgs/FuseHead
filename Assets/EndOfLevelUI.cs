using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfLevelUI : MonoBehaviour
{
    [SerializeField] private SceneController sceneController = null;
    [SerializeField] private Button _retryBtn, _nextLevelBtn, _mainMenuBtn = null;
    [SerializeField] private TextMeshProUGUI _headerText, _timeText = null;

    private void Awake()
    {
        _retryBtn?.onClick.AddListener(() => sceneController.ReloadActiveSceneWithTransition());
        _mainMenuBtn?.onClick.AddListener(() => sceneController.LoadSceneWithTransition(1));
        _nextLevelBtn?.onClick.AddListener(() => sceneController.LoadNextSceneWithTransition());
    }

#if UNITY_EDITOR
    [ContextMenu("Cache References")]
    private void CacheReferences()
    {
        _retryBtn = transform.Find(nameof(_retryBtn)).GetComponent<Button>();
        _nextLevelBtn = transform.Find(nameof(_nextLevelBtn)).GetComponent<Button>();
        _mainMenuBtn = transform.Find(nameof(_mainMenuBtn)).GetComponent<Button>();

        _headerText = transform.Find(nameof(_headerText)).GetComponent<TextMeshProUGUI>();
        _timeText = transform.Find(nameof(_timeText)).GetComponent<TextMeshProUGUI>();
    }
#endif
}
