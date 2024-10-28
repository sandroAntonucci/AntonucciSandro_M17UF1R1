using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Arrow : MonoBehaviour
{

    private Vector2 startPosition;
    public ArrowShooter shooter;

    [SerializeField] public float speed = 10f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private Collider2D arrowCollider;
    [SerializeField] private SpriteRenderer arrowSprite;


    // Sets the start position and velocity of the arrow
    private void Start()
    {
        startPosition = transform.position;
        rb.velocity = new Vector2(speed, 0);
    }

    // Starts arrow disabling process
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Coroutine to deactivate the object after particle effect finishes
        StartCoroutine(DisableArrow());
    }

    // Sets the arrow to inactive and puts itself to the shooting pool (to re-use objects instead of destroying and instantiating)
    private IEnumerator DisableArrow()
    {

        // Plays particles without affecting the player (no collision and no arrow vision)
        deathParticles.Play();
        arrowCollider.enabled = false;
        arrowSprite.enabled = false;

        yield return new WaitForSeconds(deathParticles.main.duration);

        // Resets arrow position and state
        gameObject.SetActive(false);
        arrowCollider.enabled = true;
        arrowSprite.enabled = true;
        transform.position = startPosition;
        shooter.arrowStack.Push(gameObject);

    }

    /* Note: The arrow doesn't disappear when it leaves the camera 
     * bounds because I prefer consistency across scenes. 
     * It will still reset when it collides with a wall. */
}
