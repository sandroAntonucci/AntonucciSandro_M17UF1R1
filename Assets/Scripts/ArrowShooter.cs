using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    
    public Stack<GameObject> arrowStack = new Stack<GameObject>();

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private float shootRate = 1.0f;
    [SerializeField] private float startDelay = 0f;

    private void Start()
    {
        animator.SetFloat("shootRate", shootRate);
        
    }

    // Coroutine to have diferent start times for shooters (used in some levels)
    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(startDelay);
        animator.SetBool("IsActive", true);
    }

    // Starts shooting when it's inside of camera bounds
    private void OnBecameVisible()
    {
        // Ignores the scene camera in the editor
        if (Camera.current.name == "SceneCamera")
            return;

        StartCoroutine(StartShooting());
    }

    // Stops shooting when it's outside of camera bounds
    private void OnBecameInvisible()
    {
        StopCoroutine(StartShooting());
        animator.SetBool("IsActive", false);
    }

    // Shoots arrows at the end of every shoot animation
    private void ShootArrow()
    {
        if(arrowStack.Count > 0)
        {
            GameObject arrow = arrowStack.Pop();
            
            arrow.SetActive(true);

            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            
            // Used to only call coroutine once if its already disabled (out of camera bounds)
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

    }


}
