using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    [SerializeField] private ConfigSO config = null;
    [SerializeField] private AudioEvent explosionAudio = default;
    [SerializeField] private AudioEvent footstepsAudio = default;

    private PlayerControlls _playerControlls = null;
    private CharacterController2D _characterController = null;
    private PlayerBlowUp _playerExplosion = null;
    private AudioSource _postDestroyAudioSource = null;
    private AudioSource _loopingAudioSource = null;
    private GameObject _postDestroyAudioSourceContainer = null;


    private void Awake()
    {
        ConfigAudioSource();

        _playerExplosion = GetComponent<PlayerBlowUp>();
        _playerControlls = GetComponent<PlayerControlls>();
        _characterController = GetComponent<CharacterController2D>();
        SubscribeToEvents();
    }

    #region Auxiliar
    private void SubscribeToEvents()
    {
        _playerExplosion.OnBlowUp += PlayerExplosion_OnBlowUp;
    }

    private void UnsubscribeToEvents()
    {
        _playerExplosion.OnBlowUp -= PlayerExplosion_OnBlowUp;
    }

    private void ConfigAudioSource()
    {
        _postDestroyAudioSourceContainer = new GameObject();
        _postDestroyAudioSourceContainer.name = name + "_AudioSource";
        _postDestroyAudioSource = _postDestroyAudioSourceContainer.AddComponent<AudioSource>();

        _postDestroyAudioSource.playOnAwake = false;
        _postDestroyAudioSource.loop = false;

        _loopingAudioSource = gameObject.AddComponent<AudioSource>();

        _loopingAudioSource.loop = true;
        _loopingAudioSource.playOnAwake = false;

    }

    private void Config_OnSFXSwitch(bool value)
    {
        if(_postDestroyAudioSource != null)
            _postDestroyAudioSource.mute = !value;
        if (_loopingAudioSource != null)
            _loopingAudioSource.mute = !value;
    }
    #endregion

    private void Start()
    {
        if(config != null)
        {
            UnityEngine.Audio.AudioMixerGroup audioMixerGroup = config.audioMixer.FindMatchingGroups("SFX")[0];
            _postDestroyAudioSource.outputAudioMixerGroup = audioMixerGroup;
            _loopingAudioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    private void Update()
    {
        const float threshold = 0.01f;
        bool isMoving = Mathf.Abs(_playerControlls.horizontalMove) >= threshold;
        if (_characterController.IsGrounded && isMoving)
        {
            footstepsAudio.Play(_loopingAudioSource);
        }
        else
        {
            _loopingAudioSource.Stop();
        }
    }

    private void PlayerExplosion_OnBlowUp()
    {
        if(explosionAudio != null)
        {
            explosionAudio.Play(_postDestroyAudioSource);
        }
        else
            _postDestroyAudioSource.PlayOneShot(SoundDataBase.Instance.GetClip(SoundDataBase.PlayerSounds.Explosion));
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
        Destroy(_postDestroyAudioSourceContainer, 1f);
    }
}
