using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{

    [SerializeField] Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    [SerializeField] private float force;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Activated");
            //other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var rigidbody = other.GetComponent<Rigidbody2D>();
            var vel = (Vector2)transform.up * force - rigidbody.velocity;
            rigidbody.velocity = Vector2.ClampMagnitude(vel, force * 1.5f);
        }
    }

}
