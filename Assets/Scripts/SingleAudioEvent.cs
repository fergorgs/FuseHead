using UnityEngine;

[CreateAssetMenu(fileName = "SingleAudioEvent", menuName = "AudioEvents/Single", order = 3)]
public class SingleAudioEvent : AudioEvent
{
    public AudioClip audioClip = default;

    public override void Play(AudioSource audioSource)
    {
        if (audioClip == null) return;
        if (!configs.SfxOn) return;
        audioSource.PlayOneShot(audioClip, volume);
    }
}