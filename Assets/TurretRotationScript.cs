using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotationScript : MonoBehaviour
{
	public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		//Get the angle between the points
		float angle = AngleBetweenTwoPoints(transform.position, player.transform.position);

		//Ta Daaa
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90));
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
	{
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
