using UnityEngine;
using UnityEngine.UI;

public class ConfigurationsUI : MonoBehaviour
{
    [System.Serializable]
    private struct SoundSprites
    {
        public Image SfxImage;
        public Sprite SfxOn;
        public Sprite SfxOff;
        public Image MusicImage;
        public Sprite MusicOn;
        public Sprite MusicOff;
    }

    [SerializeField]
    private ConfigSO configs = null;

    [SerializeField]
    private SoundSprites soundSprites;


    // Start is called before the first frame update
    private void Awake()
    {
        LoadConfig();
    }

    public void SwitchSfx()
    {
        configs.SfxOn = !configs.SfxOn;
        soundSprites.SfxImage.sprite = configs.SfxOn ? soundSprites.SfxOn : soundSprites.SfxOff;
    }

    public void SwitchMusic()
    {
        configs.MusicOn = !configs.MusicOn;
        soundSprites.MusicImage.sprite = configs.MusicOn ? soundSprites.MusicOn : soundSprites.MusicOff;
    }

    private void LoadConfig()
    {
        configs.LoadFromFile();
        soundSprites.SfxImage.sprite = configs.SfxOn ? soundSprites.SfxOn : soundSprites.SfxOff;
        soundSprites.MusicImage.sprite = configs.MusicOn ? soundSprites.MusicOn : soundSprites.MusicOff;
    }

    private void OnDisable()
    {
        configs.SaveToFile();
    }
}
