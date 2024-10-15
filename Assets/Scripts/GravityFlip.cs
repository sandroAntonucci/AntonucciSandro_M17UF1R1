using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{

    // Gravity is set to -16 to make the player "fall" faster
    private float gravity = -16;

	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Animator animator;
	[SerializeField] private Rigidbody2D rb;

    private void Update()
    {

		animator.SetBool("isFlipping", !IsGrounded());

		if (Input.GetButtonDown("Fire1") && IsGrounded())
        {
            FlipVertical();
        }

    }

    // Flips the player vertically and changes gravity direction
    private void FlipVertical()
    {
        gravity *= -1;
        rb.velocity = new Vector2(rb.velocity.x, gravity);
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;

    }

	// Checks if the player is grounded
	private bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}

}
