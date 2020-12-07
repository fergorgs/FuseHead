using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 1f;
    public ConfigSO configs = default;
    public abstract void Play(AudioSource audioSource);
}
