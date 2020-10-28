using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeath : MonoBehaviour
{
	public float lifeTime = 0.5f;
	public float explosionDuration = 0.1f;

	private PointEffector2D _effector;

    // Start is called before the first frame update
    void Start() {
		_effector = GetComponent<PointEffector2D>();
		StartCoroutine(DisableEffector());
		StartCoroutine(Die());
    }

	IEnumerator DisableEffector() {
		yield return new WaitForSeconds(explosionDuration);

		_effector.enabled = false;
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(lifeTime);

		Destroy(gameObject);
	}
}
