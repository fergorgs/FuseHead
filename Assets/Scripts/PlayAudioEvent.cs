using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayAudioEvent : MonoBehaviour
{
    public AudioEvent audioEvent = default;
    public bool playOnAwake = true;
    public bool playOnStart = false;
    
    private AudioSource _audioSource = default;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(_audioSource);

        if (playOnAwake)
            audioEvent?.Play(_audioSource);
    }

    private void Start()
    {
        if (playOnStart)
            audioEvent?.Play(_audioSource);
    }
}
