using System;
using System.Collections;
using UnityEngine;

public class PlayerBlowUp : MonoBehaviour {

    public event Action OnBlowUp;

    public GameObject explosion, puppetHead;
    public float blowUpTime, respawnTime;
    private SpriteRenderer sprite;
    private float startTime, t = 0;
    private Coroutine blowUpCoroutine = null;

    void Start() {
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
        StopCoroutine(blowUpCoroutine);

        // Destroy player on next frame
        // Destroy(gameObject, .01f);
    }

    private IEnumerator BlowUpTimer() {
        yield return new WaitForSeconds(blowUpTime);

        BlowUp();
    }

    public void StartBlowUpTimer() {
        blowUpCoroutine = StartCoroutine(BlowUpTimer());
        t = 0;
    }
}