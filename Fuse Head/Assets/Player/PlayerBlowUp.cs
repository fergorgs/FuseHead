using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBlowUp : MonoBehaviour {

    public GameObject explosion;
    public float blowUpTime, respawnTime;
    public Action OnBlowUp;
    void Start() {
        StartCoroutine(BlowUpTimer());
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