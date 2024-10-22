using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{

    private bool hasTriggered = false;

    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private Vector3 prevPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;    
            Camera.main.transform.position = nextPosition;
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);

        }
        else
        {
            hasTriggered = false;
            Camera.main.transform.position = prevPosition;
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }

    }

}
