using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextNameRandomizer : MonoBehaviour
{
    public PersonName[] Names = null;

    [SerializeField] private TMP_Text TextUI = null;
    
    private void OnEnable()
    {
        RandomizeNames();
        TextUI.text = string.Empty;
        foreach (PersonName personName in Names)
        {
            TextUI.text += personName.Name + ' ' + personName.LastNames + '\n'; 
        }
    }

    public void RandomizeNames()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            List<string> lastNames = new List<string>(Names[i].LastNames.Split());
            List<string> cpy = new List<string>(lastNames.Count);

            for(int j = lastNames.Count-1; j >= 0; j--)
            {
                int index = Random.Range(0, lastNames.Count);
                cpy.Add(lastNames[index]);
                lastNames.RemoveAt(index);
            }

            Names[i].LastNames = RebuildLastNames(cpy.ToArray());
        }
    }

    public string RebuildLastNames(string[] lastNames)
    {
        string result = lastNames[0];
        for(int i = 1; i < lastNames.Length; i++)
        {
            result += ' ' + lastNames[i];
        }
        return result;
    }
}

[System.Serializable]
public struct PersonName
{
    public string Name;
    public string LastNames;
}
