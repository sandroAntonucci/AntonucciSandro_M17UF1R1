using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Player")) 
        {
            gameObject.GetComponent<PlayerBase>().Die();
        }
    }
}