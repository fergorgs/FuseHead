using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerRespawnSystem : NetworkBehaviour
{
    public Vector3 respawnPoint;
    public float respawnTime = 1;
    
    private Rigidbody2D _rigidbody2d = null;
    private CapsuleCollider2D _capsuleCollider2d = null;
    public GameObject _playerPuppet = null, _walkParticles = null, _puppetHead = null;
    private PlayerBlowUp _playerBlowUp = null;
    //private bool isVisible = true;
    private NetworkVariable<bool> isVisible = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        _playerBlowUp = GetComponent<PlayerBlowUp>();
        _playerBlowUp.OnBlowUp += () => {
            Respawn();
        };
    }
    private void OnEnable()
    {
        isVisible.OnValueChanged += toggleVisible;
    }

    private void OnDisable()
    {
        isVisible.OnValueChanged -= toggleVisible;
    }

    private void toggleVisible(bool prevIsVisible, bool newIsVisible)
    {
        _rigidbody2d.simulated = newIsVisible;
        _playerPuppet.SetActive(newIsVisible);
        _walkParticles.SetActive(newIsVisible);
        _capsuleCollider2d.enabled = newIsVisible;
    }

    public void Respawn()
    {
        isVisible.Value = !isVisible.Value;
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        transform.position = respawnPoint;
        isVisible.Value = !isVisible.Value;
        _playerBlowUp.ResetBlowUpTimer();
        _puppetHead.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
