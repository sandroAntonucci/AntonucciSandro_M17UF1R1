using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{

    private Player player;

    [SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Animator animator;
	[SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource gravityChangeSound;

    // Gets the player object on start
    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void Update()
    {

		animator.SetBool("isFlipping", !IsGrounded());

		if (Input.GetButtonDown("Fire1") && IsGrounded() && player.canFlipGravity && !PauseMenu.isPaused)
        {
            gravityChangeSound.Play();
            FlipVertical();
        }

    }

    // Flips the player vertically and changes gravity direction
    private void FlipVertical()
    {

        if(player.isSpawned == false) return;

        player.gravity *= -1;

        
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;
        

    }

    // Checks if the player is grounded
    private bool IsGrounded()
    {
        // Cast a ray downwards from the player's feet to check for ground

        RaycastHit2D hit;

        if(player.gravity < 0)
        {
            hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        }
        else
        {
            hit = Physics2D.Raycast(groundCheck.position, Vector2.up, 0.1f, groundLayer);
        }

        return hit.collider != null;
    }

}
