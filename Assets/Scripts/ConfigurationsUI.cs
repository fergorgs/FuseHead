using UnityEngine;
using UnityEngine.Audio;
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

    [SerializeField] private ConfigSO configs = null;
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private SoundSprites soundSprites;

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

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        configs.SfxVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        configs.MusicVolume = volume;
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
