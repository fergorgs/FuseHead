using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{

    [SerializeField] private Animator animator = null;
    [SerializeField] private AudioEvent activateAudio = null;
    //[SerializeField] private float force = 10;

    private AudioSource _audioSource = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Activated");
            activateAudio?.Play(_audioSource);

            /* Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
            Vector2 vel = (Vector2)transform.up * force - rigidbody.velocity;
            rigidbody.velocity = Vector2.ClampMagnitude(vel, force * 1.5f);*/
        }
    }

}
