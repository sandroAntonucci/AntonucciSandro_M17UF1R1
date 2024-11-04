using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bat : MonoBehaviour
{

    private Transform currentPoint;

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Start()
    {

        currentPoint = pointB.transform;
        
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
         
        // Changes the direction of the bat depending on the objective
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        // Changes the objective of the bat
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            FlipHorizontal();
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            FlipHorizontal();
        }

    }

    // Flips the bat horizontally
    private void FlipHorizontal()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
