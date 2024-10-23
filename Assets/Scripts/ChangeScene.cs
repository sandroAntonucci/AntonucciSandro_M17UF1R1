using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{

    private bool hasTriggered = false;

    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private Vector3 prevPosition;
    [SerializeField] private Vector3 nextSpawnPoint;
    [SerializeField] private Vector3 prevSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            // Has triggered is used to detect if the scene is already changed
            hasTriggered = true;  
            
            // Changes camera position and collision position (for bugs in scene change detection)
            Camera.main.transform.position = nextPosition;
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);

            // Updates the player ability to change gravity (only used in the first scene change)
            collision.GetComponent<Player>().canFlipGravity = true;

            // Updates the respawn point of the player
            collision.GetComponent<Player>().respawnPoint = nextSpawnPoint;



        }
        else
        {
            hasTriggered = false;
            Camera.main.transform.position = prevPosition;
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            collision.GetComponent<Player>().respawnPoint = prevSpawnPoint;
        }

    }

}
