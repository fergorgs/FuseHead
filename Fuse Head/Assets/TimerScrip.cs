using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScrip : MonoBehaviour
{
	public float initialTime = 16f;

	private float iniTime;

    // Start is called before the first frame update
    void Start()
    {
		iniTime = Time.time;    
    }

    // Update is called once per frame
    void Update()
    {
		GetComponent<Text>().text = "TIME LEFT\n" + (int)(initialTime - (Time.time - iniTime)) + "s";

		if((initialTime - (Time.time - iniTime)) <= 0)
		{
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		}

    }
}
