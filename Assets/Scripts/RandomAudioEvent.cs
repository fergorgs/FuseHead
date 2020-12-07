using UnityEngine;

[CreateAssetMenu(fileName = "RandomAudioEvent", menuName = "AudioEvents/Random", order = 3)]
public class RandomAudioEvent : AudioEvent
{
    public AudioClip[] audioClips = default;

    public override void Play(AudioSource audioSource)
    {
        if (audioClips.Length == 0) return;
        if (!configs.SfxOn) return;
        AudioClip audioClip = audioClips[Random.Range((int)0, audioClips.Length)];
        audioSource.PlayOneShot(audioClip, volume);
    }
}