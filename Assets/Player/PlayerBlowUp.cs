using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerBlowUp : NetworkBehaviour
{
    public event Action OnBlowUp;

    public GameObject explosion, puppetHead;
    public float blowUpTime, respawnTime;
    private SpriteRenderer sprite;
    private NetworkVariable<float> t = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //private Coroutine blowUpCoroutine = null;

    void Start() {
        sprite = puppetHead.GetComponent<SpriteRenderer>();
        ResetBlowUpTimer();
    }

    private void Update() {

        //if (!IsOwner) { return; }

        if (Input.GetButtonDown("Blow up") && IsOwner) { BlowUp(); }

        sprite.color = Color.Lerp(Color.white, Color.red, t.Value);

        if (t.Value < 1)
            t.Value += Time.deltaTime / blowUpTime;
        if (t.Value >= 1)
            BlowUp();
    }
    public void BlowUp() {
        Instantiate(explosion, transform.position, transform.rotation);

        OnBlowUp?.Invoke();
        ResetBlowUpTimer();
        //StopCoroutine(blowUpCoroutine);

        // Destroy player on next frame
        // Destroy(gameObject, .01f);
    }

    //private IEnumerator BlowUpTimer() {
      //  yield return new WaitForSeconds(blowUpTime);

        //BlowUp();
    //}

    public void ResetBlowUpTimer() {
        t.Value = 0;
    }
}