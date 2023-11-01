using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    //public Vector2 wallCheckDirection = Vector2.zero;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Animator animator;
    private CapsuleCollider2D touchingCol;


    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }


    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

   
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();

        //// Check that collider offset is 0,0
        //if (touchingCol.offset.x != 0)
        //{
        //    Debug.LogWarning("Recommended x offset of 0 for TouchingDirections collider on game object " + gameObject.name + ". Adjust the transform instead");
        //}
    }

    public void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;

        // Wall collisions sometimes show up as ceiling collisions if BoxCollider (I guess corners could show up as either or both)
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
