using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 7f;
    public float runSpeed = 14f;
    public float airWalkSpeed = 7f;
    public float attackingMoveSpeed = 2f;
    public float jumpImpulse = 10f;
    public float groundDistance = 0.05f;


    TouchingDirections touchingDirections;
    Damageable damageable;


    public Collider2D groundCollider, wallCollider;

    Rigidbody2D rb;
    Vector2 moveInput;
    Animator animator;
    CapsuleCollider2D physCollider;


    [SerializeField]
    private bool _isFacingRight = true;

    internal bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            //Flip only if value is new
            if (_isFacingRight != value)
            {
                //flip local scale to make player face the other direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }



    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning { get 
        { 
            return _isRunning; 
        }  
        set 
        {
            _isRunning = value; 
            animator.SetBool(AnimationStrings.isRunning, value); 
        } }


    [SerializeField]
    private bool _isMoving = false;
   
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set 
        { 
            _isMoving = value; 
            animator.SetBool(AnimationStrings.isMoving, value); 
        } }


    
    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
                
            }
            else
            {
                return 0; 
            }
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //physCollider = GetComponent<CapsuleCollider2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        
    }

    // 10-29-23***********
    
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

// Unity InputSystem PlayerInput Actions
public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Allowed to switch direction
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero /*? true : false*/;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }



    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true ;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight=false ;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
            //animator.SetBool(/*AnimationStrings.*/"isRunning", _isRunning);
        }
        else if (context.canceled)
        {
            IsRunning = false;
            //animator.SetBool(/*AnimationStrings.*/"isRunning", _isRunning);
        }
    }

    // Jump keys pressed
    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            //if (IsAlive)
            //{
            //    // jumpTrigger = true;
            //    animator.SetTrigger(AnimationStrings.jump);
            //    animator.SetTrigger(AnimationStrings.ground_interrupt);

            //    rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            //}
        }
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }

    }

    public bool CanMove {  get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }
}
