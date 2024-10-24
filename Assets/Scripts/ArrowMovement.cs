using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;


    private void Start()
    {
        rb.velocity = new Vector2(speed, 0);
    }

}
