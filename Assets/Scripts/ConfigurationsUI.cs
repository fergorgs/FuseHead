using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigurationsUI : MonoBehaviour
{
    [System.Serializable]
    private struct SoundObjects
    {
        public Image SfxImage;
        public Slider SfxSlider;
        public Sprite SfxOn;
        public Sprite SfxOff;
        public Image MusicImage;
        public Slider MusicSlider;
        public Sprite MusicOn;
        public Sprite MusicOff;
    }

    [SerializeField] private ConfigSO configs = null;
    [SerializeField] private SoundObjects soundObjs = new SoundObjects(){};

    // Start is called before the first frame update
    private void Start()
    {
        LoadConfig();

        soundObjs.SfxSlider.onValueChanged.AddListener(SetSfxVolume);
        soundObjs.SfxImage.GetComponent<Button>().onClick.AddListener(SwitchSfx);

        soundObjs.MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundObjs.MusicImage.GetComponent<Button>().onClick.AddListener(SwitchMusic);
    }

    public void SwitchSfx()
    {
        configs.SetSfx(!configs.SfxOn);
        soundObjs.SfxImage.sprite = configs.SfxOn ? soundObjs.SfxOn : soundObjs.SfxOff;
    }

    public void SwitchMusic()
    {
        configs.SetMusic(!configs.MusicOn);
        soundObjs.MusicImage.sprite = configs.MusicOn ? soundObjs.MusicOn : soundObjs.MusicOff;
    }

    public void SetSfxVolume(float volume)
    {
        configs.SetSfxVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        configs.SetMusicVolume(volume);
    }

    public void LoadConfig()
    {
        configs.LoadFromFile();
        soundObjs.SfxSlider.value = UtilsClass.DecibelToLinear(configs.SfxVolume);
        soundObjs.MusicSlider.value = UtilsClass.DecibelToLinear(configs.MusicVolume);
        SetSfxVolume(soundObjs.SfxSlider.value);
        SetMusicVolume(soundObjs.MusicSlider.value);


        soundObjs.SfxImage.sprite = configs.SfxOn ? soundObjs.SfxOn : soundObjs.SfxOff;
        soundObjs.MusicImage.sprite = configs.MusicOn ? soundObjs.MusicOn : soundObjs.MusicOff;
    }

    private void OnDestroy()
    {
        configs.SaveToFile();
    }
}
