using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotationScript : MonoBehaviour
{
	public Transform target;
	
    public float range = 10f;

	void Start() {
		target = null;
	}

    // Update is called once per frame
    void Update()
    {
		if(target == null) return;
		//Get the angle between the points
		float angle = AngleBetweenTwoPoints(transform.position, target.position);

		//Ta Daaa
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90));
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
	{
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	void FixedUpdate() {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, range);

		target = null;

        foreach(Collider2D collider2D in overlappedColliders) {
			Debug.Log(collider2D.name);
            if(collider2D.CompareTag("Player")) {
                target = collider2D.transform;
            	break;
            }
        }
		
    }

     private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
