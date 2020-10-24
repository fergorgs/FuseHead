using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Rigidbody2D _rigidbody = null;
    [SerializeField] private CharacterController2D _controller = null;

    private void Awake()
    {    
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();

        _controller.OnLandEvent.AddListener(TriggerLandEvent);
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed_X", _rigidbody.velocity.x);
        _animator.SetFloat("Speed_Y", _rigidbody.velocity.y);
    }

    public void TriggerLandEvent()
    {
        _animator.SetTrigger("Landed");
    }

}
