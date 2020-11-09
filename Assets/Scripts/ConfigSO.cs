using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Config", menuName = "Config/General")]
[System.Serializable]
public class ConfigSO : ScriptableObject
{
    public event Action<bool> OnSfxSwitch = delegate { };
    public event Action<bool> OnMusicSwitch = delegate { };

    [Header("Sound Options")]
    public bool SfxOn = true;
    public bool MusicOn = true;
    [Range(-80f, 0f)]
    public float SfxVolume = 0f; 
    [Range(-80f, 0f)]
    public float MusicVolume = 0f;
    public AudioMixer audioMixer = null;

    [Header("Game Options")]
    public bool FriendlyFire = false;

    string path;

    private void OnEnable()
    {
        path = Application.persistentDataPath + "/config.json";

        Application.quitting += SaveToFile;

    }

    public void LoadFromFile()
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            string createText = JsonUtility.ToJson(this);
            File.WriteAllText(path, createText, Encoding.UTF8);
        }
        else
        {
            var json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);

            audioMixer.SetFloat("sfxVolume", SfxVolume);
            audioMixer.SetFloat("musicVolume", MusicVolume);
        }
    }

    public void SaveToFile()
    {
        string createText = JsonUtility.ToJson(this);
        File.WriteAllText(path, createText, Encoding.UTF8);
    }

    public void SetMusic(bool value)
    {
        MusicOn = value;
        OnMusicSwitch?.Invoke(value);
    }

    public void SetSfx(bool value)
    {
        SfxOn = value;
        OnSfxSwitch?.Invoke(value);
    }

    public void SetSfxVolume(float volume)
    {
        float decibalVolume = UtilsClass.LinearToDecibel(volume);
        audioMixer.SetFloat("sfxVolume", decibalVolume);
        SfxVolume = decibalVolume;
    }

    public void SetMusicVolume(float volume)
    {
        float decibalVolume = UtilsClass.LinearToDecibel(volume);
        audioMixer.SetFloat("musicVolume", decibalVolume);
        MusicVolume = decibalVolume;
    }
}
