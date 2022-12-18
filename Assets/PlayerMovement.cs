using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private float horizontal;
    private float speed = 8f;
    public float jumpForce = 16f;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    const float k_GroundedRadius = .2f;

    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem _dustParticle;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsOwner) { return; }

        UpdateIsGrounded();
        /*if (wasGrounded && !isGrounded && m_Rigidbody2D.velocity.y < 0f)
        {
            isGrounded = true;
            Invoke(nameof(GhostJumpDelay), 0.001f * ghostJumpDelay);

        }*/

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && m_Rigidbody2D.velocity.y > 0f)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }
        m_Rigidbody2D.velocity = new Vector2(horizontal * speed, m_Rigidbody2D.velocity.y);
    }

    private void UpdateIsGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    //OnLandEvent.Invoke();
                    MakeDust();
                }
            }
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void MakeDust()
    {
        _dustParticle.Play();
    }
}
