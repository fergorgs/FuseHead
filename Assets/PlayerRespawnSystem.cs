using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    public Vector3 respawnPoint;
    public float respawnTime = 1;
    
    private Rigidbody2D _rigidbody2d = null;
    private CapsuleCollider2D _capsuleCollider2d = null;
    public GameObject _playerPuppet = null, _walkParticles = null, _puppetHead = null;
    private PlayerBlowUp _playerBlowUp = null;
    private bool isVisible = true;


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

    private void toggleVisible()
    {
        isVisible = !isVisible;
        _rigidbody2d.simulated = isVisible;
        _playerPuppet.SetActive(isVisible);
        _walkParticles.SetActive(isVisible);
        _capsuleCollider2d.enabled = isVisible;
    }

    public void Respawn()
    {
        toggleVisible();
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        transform.position = respawnPoint;
        toggleVisible();
        _playerBlowUp.ResetBlowUpTimer();
        _puppetHead.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
