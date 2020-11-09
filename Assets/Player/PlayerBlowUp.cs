using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlowUp : MonoBehaviour {

    public GameObject explosion, puppetHead;
    public float blowUpTime, respawnTime;
    public Action OnBlowUp;
    private SpriteRenderer sprite;
    private float startTime, t = 0;

    void Start() {
        StartCoroutine(BlowUpTimer());
        startTime = Time.time;
        sprite = puppetHead.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        sprite.color = Color.Lerp(Color.white, Color.red, t);

        if (t < 1)
            t += Time.deltaTime / blowUpTime;

    }
    public void BlowUp() {
        Instantiate(explosion, transform.position, transform.rotation);

        OnBlowUp?.Invoke();

        // Destroy player on next frame
        Destroy(gameObject, .01f);
    }

    private IEnumerator BlowUpTimer() {
        yield return new WaitForSeconds(blowUpTime);

        BlowUp();
    }
}