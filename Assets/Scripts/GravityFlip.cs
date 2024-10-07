using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{

    // Gravity is set to -16 to make the player "fall" faster
    private float gravity = -16;

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {

        Debug.Log("Gravity: " + gravity);

        if (Input.GetButtonDown("Fire1"))
        {
            FlipVertical();
        }
    }

    // Flips the player vertically and changes gravity direction
    private void FlipVertical()
    {
        Debug.Log("Flipping gravity");
        gravity *= -1;
        rb.velocity = new Vector2(rb.velocity.x, gravity);
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;

    }

}
