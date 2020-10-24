using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBlowUp : MonoBehaviour {

    public GameObject explosion, puppetHead;
    public float blowUpTime, respawnTime;
    public Action OnBlowUp;

	private float startTime, t = 0;

    void Start() {
        StartCoroutine(BlowUpTimer());
		startTime = Time.time;
    }

	private void Update()
	{
		puppetHead.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, t);

		if (t < 1)
			t += Time.deltaTime / blowUpTime;

	}
	public void BlowUp() {
        Instantiate(explosion, transform.position, transform.rotation);

        OnBlowUp?.Invoke();

        Destroy(gameObject);
    }

    private IEnumerator BlowUpTimer() {
        yield return new WaitForSeconds(blowUpTime);

        BlowUp();
    }
}