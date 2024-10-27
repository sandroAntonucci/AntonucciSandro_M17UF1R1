using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{

    private Vector2 startPosition;
    public ArrowShooter shooter;

    [SerializeField] public float speed = 10f;
    [SerializeField] public Rigidbody2D rb;


    private void Start()
    {
        startPosition = transform.position;
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);

        transform.position = startPosition;

        shooter.arrowStack.Push(gameObject);
    }

}
