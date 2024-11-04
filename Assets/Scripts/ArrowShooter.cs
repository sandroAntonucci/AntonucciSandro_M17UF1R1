using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public Stack<GameObject> arrowStack = new Stack<GameObject>();

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private float shootRate = 1.0f;
    [SerializeField] private float startDelay = 0f;

    private bool isShooting = false;  // Tracks shooting state
    private Coroutine shootingCoroutine = null;  // Reference to active coroutine

    private void Start()
    {
        animator.SetFloat("shootRate", shootRate);
    }

    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(startDelay);
        animator.SetBool("IsActive", true);
        isShooting = true;
    }

    private void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
        animator.SetBool("IsActive", false);
        isShooting = false;
    }

    private void OnBecameVisible()
    {
        // Avoid triggering in the editor scene view
        if (Camera.current == null || Camera.current.name == "SceneCamera") return;

        if (!isShooting)
        {
            shootingCoroutine = StartCoroutine(StartShooting());
        }
    }

    private void OnBecameInvisible()
    {
        if (isShooting)
        {
            StopShooting();
        }
    }

    // Shoots arrows at the end of every shoot animation
    private void ShootArrow()
    {
        if (arrowStack.Count > 0)
        {
            GameObject arrow = arrowStack.Pop();
            arrow.SetActive(true);

            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            arrowComponent.isDisabling = false;

            Vector2 direction = transform.up;
            arrowComponent.rb.velocity = direction * arrowComponent.speed;
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            arrowComponent.shooter = this;
        }

        shootSound.Play();
    }
}
