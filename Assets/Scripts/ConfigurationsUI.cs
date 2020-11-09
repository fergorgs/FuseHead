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
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private SoundObjects soundObjs;

    // Start is called before the first frame update
    private void Awake()
    {
        LoadConfig();
    }

    public void SwitchSfx()
    {
        configs.SfxOn = !configs.SfxOn;
        soundObjs.SfxImage.sprite = configs.SfxOn ? soundObjs.SfxOn : soundObjs.SfxOff;
    }

    public void SwitchMusic()
    {
        configs.MusicOn = !configs.MusicOn;
        soundObjs.MusicImage.sprite = configs.MusicOn ? soundObjs.MusicOn : soundObjs.MusicOff;
    }

    public void SetSfxVolume(float volume)
    {
        float decibalVolume = UtilsClass.LinearToDecibel(volume);
        audioMixer.SetFloat("sfxVolume", decibalVolume);
        configs.SfxVolume = decibalVolume;
    }

    public void SetMusicVolume(float volume)
    {
        float decibalVolume = UtilsClass.LinearToDecibel(volume);
        audioMixer.SetFloat("musicVolume", decibalVolume);
        configs.MusicVolume = decibalVolume;
    }

    public void LoadConfig()
    {
        configs.LoadFromFile();

        audioMixer.SetFloat("sfxVolume", configs.SfxVolume);
        audioMixer.SetFloat("musicVolume", configs.MusicVolume);
        soundObjs.SfxSlider.value = UtilsClass.DecibelToLinear(configs.SfxVolume);
        soundObjs.MusicSlider.value = UtilsClass.DecibelToLinear(configs.MusicVolume);


        soundObjs.SfxImage.sprite = configs.SfxOn ? soundObjs.SfxOn : soundObjs.SfxOff;
        soundObjs.MusicImage.sprite = configs.MusicOn ? soundObjs.MusicOn : soundObjs.MusicOff;
    }

    private void OnDisable()
    {
        configs.SaveToFile();
    }
}
