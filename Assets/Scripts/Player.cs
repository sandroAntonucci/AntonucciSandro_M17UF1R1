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

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    // Start to apply initial gravity to the player
    private void Start()
    {
        RespawnPlayer();
    }

    // Makes the player wait for spawn before moving
    public IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        isSpawned = true;
    }

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
        animator.Play("Die");
    }

    public void RespawnPlayer()
    {
        
        isSpawned = false;
        rb.velocity = new Vector2(0f, gravity);
        transform.position = respawnPoint.transform.position;
        StartCoroutine(WaitForSpawn());
        

    }
}