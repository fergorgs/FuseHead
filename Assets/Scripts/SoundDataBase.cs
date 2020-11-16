using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundDataBase : MonoBehaviour
{
    public static SoundDataBase Instance = null;

    public enum PlayerSounds
    {
        Explosion,
        //Jump,
        //Walk,
        //Land,
    }
    
    private Dictionary<string, AudioClip[]> _audioClipEnumToArrayDictionary = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        _audioClipEnumToArrayDictionary = new Dictionary<string, AudioClip[]>();

        LoadSFXFromDatabase();

        DontDestroyOnLoad(gameObject);
    }

    private void LoadSFXFromDatabase()
    {
        string[] soundNames = Enum.GetNames(typeof(PlayerSounds));
        AudioClip[] audioClipArray = new AudioClip[soundNames.Length];
        int i = 0;
        foreach (var name in soundNames)
        {
            audioClipArray[i] = Resources.Load<AudioClip>(Path.Combine("Player", name));
            i++;
        }

        _audioClipEnumToArrayDictionary[nameof(PlayerSounds)] = audioClipArray;
    }

    public AudioClip GetClip(PlayerSounds sound) => _audioClipEnumToArrayDictionary[nameof(PlayerSounds)][(int)sound];

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
