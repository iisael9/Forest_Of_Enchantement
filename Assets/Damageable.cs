using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (Health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    // Reference to the respawn point
    public Transform respawnPoint;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            if (animator != null)
            {
                animator.SetBool(AnimationStrings.isAlive, value);
                Debug.Log("IsAlive set " + value);
            }
        }
    }

    public bool LockVelocity
    {
        get { return animator != null ? animator.GetBool(AnimationStrings.lockVelocity) : false; }
        set { if (animator != null) animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            if (animator != null)
                animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);

            // Check if the player's health is zero or below
            if (Health <= 0)
            {
                // Respawn the player at the respawn point
                Respawn();
            }

            return true;
        }

        return false;
    }

    // Function to respawn the player at the respawn point
    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Health = MaxHealth;
            IsAlive = true;
        }
        else
        {
            Debug.LogError("Respawn point not assigned to Damageable script.");
        }
    }
}