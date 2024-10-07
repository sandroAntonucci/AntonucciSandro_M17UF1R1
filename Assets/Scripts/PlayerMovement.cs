using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    private bool isSpawned = false;
    private float initialGravity = -16f;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start to apply initial gravity to the player
    private void Start()
    {
        StartCoroutine(WaitForSpawn());
        rb.velocity = new Vector2(0f, initialGravity);
    }

    // Makes the player wait for spawn before moving
    private IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        isSpawned = true;
    }

    private void Update()
    {
        if (!isSpawned) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isFlipping", !IsGrounded());
        FlipHorizontal();
    }

    // Checks if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Updates the player's velocity
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));                                                                                                                                            
    }

    // Flips the player horizontally
    private void FlipHorizontal()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}