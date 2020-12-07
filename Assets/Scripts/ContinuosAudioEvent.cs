using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousAudioEvent", menuName = "AudioEvents/Continuous", order = 3)]
public class ContinuosAudioEvent : AudioEvent
{
    public AudioClip audioClip = default;

    public override void Play(AudioSource audioSource)
    {
        if(audioSource.clip != audioClip)
            audioSource.clip = audioClip;
        audioSource.volume = volume;
        if(!audioSource.isPlaying)
            audioSource.Play();
    }
}
