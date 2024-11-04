using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector2 startPosition;
    public bool isDisabling = false; // Track if arrow is being disabled
    public ArrowShooter shooter;

    [SerializeField] public float speed = 10f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private Collider2D arrowCollider;
    [SerializeField] private SpriteRenderer arrowSprite;

    private void Start()
    {
        startPosition = transform.position;

        Vector2 direction = transform.up;
        rb.velocity = direction * speed;
    }

    // Dissables arrow when it collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisabling)
        {
            isDisabling = true;
            StartCoroutine(DisableArrow());
        }
    }
    
    // Disables arrow waiting for the particles effect to end
    private IEnumerator DisableArrow()
    {
        isDisabling = true;

        // Play particle effects and disable visibility and collisions
        deathParticles.Play();
        arrowCollider.enabled = false;
        arrowSprite.enabled = false;

        yield return new WaitForSeconds(deathParticles.main.duration);

        // Reset and deactivate the arrow
        gameObject.SetActive(false);
        arrowCollider.enabled = true;
        arrowSprite.enabled = true;
        transform.position = startPosition;

        shooter.arrowStack.Push(gameObject); // Return arrow to pool

        isDisabling = false; // Reset disabling status
    }

    // Disables arrow when out of camera bounds
    private void OnBecameInvisible()
    {
        // Only disable if not already disabling and if GameObject is active (without the check it gives an error when exiting the game)
        if (!isDisabling && gameObject.activeInHierarchy)
        {
            StartCoroutine(DisableArrow());
        }
    }
}

