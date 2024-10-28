using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    public bool isSpawned = false;
    public float gravity = -16;
    public Vector2 respawnPoint;

    [SerializeField] public bool canFlipGravity = false;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem deathParticles;

    // Start to apply initial properties to the player (gravity, position, velocity)
    private void Start()
    {
        respawnPoint = transform.position;
        RespawnPlayer();
    }

    // Makes the player wait for spawn before moving
    public IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        isSpawned = true;
    }

    // Gets the player's input
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        FlipHorizontal();
    }

    // Updates the player's velocity
    private void FixedUpdate()
    {
        if (!isSpawned) return;
        rb.velocity = new Vector2(horizontal * speed, gravity);
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

    // Plays the dying animation which queues the respawn
    public void Die()
    {
        deathParticles.Play();
        GetComponent<SimpleFlash>().Flash();
        animator.Play("Die");
    }

    // Respawns the player
    public void RespawnPlayer()
    {

        isSpawned = false;

        // Flips the player and changes gravity if it dies going up
        if (gravity == 16)
        {
            gravity = -16;
            Vector3 localScale = transform.localScale;
            localScale.y *= -1;
            transform.localScale = localScale;
        }

        // Resets player position and velocity
        rb.velocity = new Vector2(0f, gravity);
        transform.position = respawnPoint;

        StartCoroutine(WaitForSpawn());
        

    }
}