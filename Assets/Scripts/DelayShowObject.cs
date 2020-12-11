using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayShowObject : MonoBehaviour
{
    public float time;
    public GameObject obj;

    public void ShowObject()
    {
        StartCoroutine(_ShowRoutine());
    }

    private IEnumerator _ShowRoutine()
    {
        yield return new WaitForSecondsRealtime(time);

        obj.SetActive(true);
    }

}
