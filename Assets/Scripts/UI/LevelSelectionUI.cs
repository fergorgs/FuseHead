using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private LevelTextureName[] levelTextureArray = null; 
    [SerializeField] private SceneController sceneController = null;

    private Button _rightArrow = null;
    private Button _leftArrow = null;
    private Button _playBtn = null;
    private RawImage _levelImage = null;
    private int _selectedIndex = 0;

    private void Awake()
    {
        _rightArrow = transform.Find(nameof(_rightArrow)).GetComponent<Button>();
        _leftArrow = transform.Find(nameof(_leftArrow)).GetComponent<Button>();
        _playBtn = transform.Find(nameof(_playBtn)).GetComponent<Button>();
        _levelImage = transform.Find(nameof(_levelImage)).GetComponent<RawImage>();

        if(levelTextureArray.Length > 0)
        {
            _levelImage.texture = levelTextureArray[0].Texture;
            _selectedIndex = 0;
        }

        _rightArrow.onClick.AddListener(NextLevel);
        _leftArrow.onClick.AddListener(PreviousLevel);
        _playBtn.onClick.AddListener(LoadSelectedLevel);
    }

    private void LoadSelectedLevel()
    {
        sceneController.LoadSceneWithTransition(levelTextureArray[_selectedIndex].Name);
    }

    private void NextLevel()
    {
        _selectedIndex = (_selectedIndex + 1) % levelTextureArray.Length;

        UpdatePreviewImage();
    }

    private void PreviousLevel()
    {
        _selectedIndex = (_selectedIndex - 1);
        if (_selectedIndex < 0)
            _selectedIndex = levelTextureArray.Length - 1;

        UpdatePreviewImage();
    }

    private void UpdatePreviewImage() => _levelImage.texture = levelTextureArray[_selectedIndex].Texture;

    [System.Serializable]
    private struct LevelTextureName
    {
        public Texture2D Texture;
        public string Name;
    }
}
