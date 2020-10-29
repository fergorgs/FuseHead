using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCompanyText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMPro.TMP_Text>().text = $"(c) {Application.companyName} {System.DateTime.Today.Year}";   
    }
}
