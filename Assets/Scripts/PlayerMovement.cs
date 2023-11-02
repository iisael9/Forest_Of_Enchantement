using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Animator animator = null;

    [SerializeField] private float Speed;

    private bool isFacingRight = true; // Tracks player's facing direction

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementInput * Speed;

        // Set the IsRunning parameter in the Animator
        animator.SetBool("IsRunning", _movementInput.magnitude > 0);

        // Set the IsAttacking parameter in the Animator
        animator.SetBool("IsAttacking", false); // Set to false by default

        // Flip the player's sprite based on the direction of movement
        if (_movementInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (_movementInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        // Check for right mouse button click to trigger the attack animation
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            OnAttack();
        }
    }

    private void OnAttack()
    {
        // Handle the attack input here
        // You can add logic to trigger the attack animation here
        animator.SetBool("IsAttacking", true);
    }

    // Flip the player's sprite
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}