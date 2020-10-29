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
            string[] lastNames = Names[i].LastNames.Split();
            string[] cpy = (string[])lastNames.Clone();
            for(int j = 0; j < lastNames.Length; j++)
            {
                lastNames[j] = cpy[(j+1)%lastNames.Length];
                Debug.Log(cpy[j] + '|' + lastNames[j]);
            }
            Names[i].LastNames = RebuildLastNames(lastNames);
        }
    }

    public string RebuildLastNames(string[] lastNames)
    {
        string result = lastNames[0];
        for(int i = 1; i < lastNames.Length; i++)
        {
            result += ' ' + lastNames[i];
        }
        Debug.Log(result);
        return result;
    }
}

[System.Serializable]
public struct PersonName
{
    public string Name;
    public string LastNames;
}
