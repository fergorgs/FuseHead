using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeath : MonoBehaviour
{
	public float lifeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Die());    
    }

	IEnumerator Die()
	{
		yield return new WaitForSeconds(lifeTime);

		Destroy(gameObject);
	}
}
