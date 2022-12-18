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

    void Start() {
        sprite = puppetHead.GetComponent<SpriteRenderer>();
        ResetBlowUpTimer();

        t.OnValueChanged += UpdateSpriteColor;
    }

    private void Update() {

        if (!IsOwner) { return; }

        if (Input.GetButtonDown("Blow up")) { BlowUp(); }

        //sprite.color = Color.Lerp(Color.white, Color.red, t.Value);

        if (t.Value < 1)
            t.Value += Time.deltaTime / blowUpTime;
        if (t.Value >= 1)
            BlowUp();
    }
    public void BlowUp() {
        Instantiate(explosion, transform.position, transform.rotation);

        OnBlowUp?.Invoke();
        ResetBlowUpTimer();
    }

    public void ResetBlowUpTimer() {
        t.Value = 0;
    }

    private void UpdateSpriteColor(float previousT, float newT)
    {
        sprite.color = Color.Lerp(Color.white, Color.red, newT);
    }
}