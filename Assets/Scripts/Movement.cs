using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Experimental.Rendering.RayTracingAccelerationStructure;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed; // jak szybko student ucieka, po w?dce zrobimy speed++ i b?dzie boost
    public Vector2 currentDirection = Vector2.zero, buforDirection = Vector2.zero, nextDirection = Vector2.zero, initialDirection = Vector2.zero; // zmienna przechowujaca "kierunki"
    public LayerMask obstacleLayer;
    public Vector2 Vdirection = Vector2.zero;
    public bool isGhost;
    public new Rigidbody2D rb { get; private set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 15.0f;
    }

    void Update()
    {
            buforDirection = nextDirection;
            if (!Occupied(buforDirection))
            {
                currentDirection = buforDirection;
            }
            if (Input.GetKeyDown(KeyCode.R))
                Stop();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentDirection * Time.fixedDeltaTime * speed);
                //transform.rotation = Quaternion.Euler(0, 0, 180.0f);   // tutaj juz nam zmienia nie po wektorze tylko ustalony k?t rotacji
               // transform.rotation = Quaternion.Euler(0, 0, 0);
              //  transform.rotation = Quaternion.Euler(0, 0, 90.0f);
              //  transform.rotation = Quaternion.Euler(0, 0, 270.0f);
    }

    public void setDirection(Vector2 fromInput)
    {
        nextDirection = fromInput;
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
    public void Stop()
    {
        currentDirection = Vector2.zero;
        buforDirection = Vector2.zero;
        nextDirection = Vector2.zero;
    }
}

