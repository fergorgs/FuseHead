using System;
using System.IO;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    [SerializeField] private ConfigSO config = null;
    private PlayerBlowUp _playerExplosion = null;
    private AudioSource _audioSource = null;
    private GameObject _audioSourceContainer = null;


    private void Awake()
    {
        ConfigAudioSource();

        _playerExplosion = GetComponent<PlayerBlowUp>();
        SubscribeToEvents();
    }

    #region Auxiliar
    private void SubscribeToEvents()
    {
        config.OnSfxSwitch += Config_OnSFXSwitch;
        _playerExplosion.OnBlowUp += PlayerExplosion_OnBlowUp;
    }

    private void UnsubscribeToEvents()
    {
        config.OnSfxSwitch -= Config_OnSFXSwitch;
        _playerExplosion.OnBlowUp -= PlayerExplosion_OnBlowUp;
    }

    private void ConfigAudioSource()
    {
        _audioSourceContainer = new GameObject();
        _audioSourceContainer.name = name + "_AudioSource";
        _audioSource = _audioSourceContainer.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.mute = !config.SfxOn;
    }

    private void Config_OnSFXSwitch(bool value)
    {
        if(_audioSource != null)
            _audioSource.mute = !value;
    }
    #endregion

    private void Start()
    {
        if(config != null)
        {
            _audioSource.outputAudioMixerGroup = config.audioMixer.FindMatchingGroups("SFX")[0];
        }
    }

    private void PlayerExplosion_OnBlowUp()
    {
        Debug.Log("Exploooosion");
        _audioSource.PlayOneShot(SoundDataBase.Instance.GetClip(SoundDataBase.PlayerSounds.Explosion));
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
        Destroy(_audioSourceContainer, 1f);
    }
}
