using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicAudioSource : MonoBehaviour
{
    [SerializeField] private ConfigSO config;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Config_OnMusicSwitch(bool value)
    {
        _audioSource.mute = !value;
    }

    private void OnEnable()
    {
        config.OnMusicSwitch += Config_OnMusicSwitch;
        _audioSource.mute = !config.MusicOn;
    }

    private void OnDisable()
    {
        config.OnMusicSwitch -= Config_OnMusicSwitch;
    }
}
