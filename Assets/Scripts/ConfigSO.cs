using System;
using System.IO;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Config/General")]
[System.Serializable]
public class ConfigSO : ScriptableObject
{
    [Header("Sound Options")]
    public bool SfxOn = true;
    public bool MusicOn = true;
    [Range(0f, 1f)]
    public float SfxAmount = 1f; 
    [Range(0f, 1f)]
    public float MusicAmount = 1f;

    [Header("Game Options")]
    public bool FriendlyFire = false;

    string path;

    private void OnEnable()
    {
        path = Application.persistentDataPath + "/config.json";
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
        }
    }

    public void SaveToFile()
    {
        string createText = JsonUtility.ToJson(this);
        File.WriteAllText(path, createText, Encoding.UTF8);
    }
}
