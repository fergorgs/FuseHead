using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControllerScript : MonoBehaviour
{
	public Vector3[] respawnPoints;
	public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void RespawnPlayer(int playerID, float secondsToRespawn)
	{
		StartCoroutine(RespawnPlayerTimer(playerID, secondsToRespawn));
	}

	private IEnumerator RespawnPlayerTimer(int playerID, float secondsToRespawn)          //playerID: diferenciar entre player 1, 2, 3 e 4
	{
		yield return new WaitForSeconds(secondsToRespawn);

		Vector3 respPos = respawnPoints[Random.Range(0, respawnPoints.Length)];

		Instantiate(playerPrefab, respPos, new Quaternion());
	}
}
