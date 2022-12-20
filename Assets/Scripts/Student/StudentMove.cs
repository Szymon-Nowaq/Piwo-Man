using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Experimental.Rendering.RayTracingAccelerationStructure;

[RequireComponent(typeof(Rigidbody2D))]
public class StudentMove : MonoBehaviour
{
    public float speed; // jak szybko student ucieka, po w�dce zrobimy speed++ i b�dzie boost
    public enum direction { left, up, none, right, down }; // nasz w�asny typ danych, kt�ry mo�e mie� 5 stan�w, NIE RUSZAC ICH INDEKS�W ZA ZADNEGO CHUJA BO WSZYSTKO SIE WYWALI
    public direction currentDirection = direction.none, buforDirection = direction.none; // zmienna przechowujaca "kierunki"
    public LayerMask obstacleLayer;
    public Vector2 Vdirection = Vector2.zero;
    public new Rigidbody2D rb { get; private set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 15.0f;
    }

    void Update()
    {
        buforDirection = setDirection();
        SetVector(buforDirection);
        if (!Occupied(Vdirection))
        {
            currentDirection = buforDirection;
        }
        else
            Debug.Log("sciana");
        if (Input.GetKeyDown(KeyCode.R))
            currentDirection = direction.none;
    }

    void FixedUpdate()
    {   
        switch (currentDirection)
        {
            case direction.left:
                rb.MovePosition(rb.position + Vector2.left * Time.fixedDeltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 180.0f);   // tutaj juz nam zmienia nie po wektorze tylko ustalony k�t rotacji
                break;
            case direction.right:
                rb.MovePosition(rb.position + Vector2.right * Time.fixedDeltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case direction.up:
                rb.MovePosition(rb.position + Vector2.up * Time.fixedDeltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 90.0f);
                break;
            case direction.down:
                rb.MovePosition(rb.position + Vector2.down * Time.fixedDeltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 270.0f);
                break;
            case direction.none:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    public direction setDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) // jak wcisniemy jakis klawisz to zmieniamy do "currentDirection" 
            return direction.left;
        if (Input.GetKey(KeyCode.RightArrow))
            return direction.right;
        if (Input.GetKey(KeyCode.DownArrow))
            return direction.down;
        if (Input.GetKey(KeyCode.UpArrow))
            return direction.up;
        if (Input.GetKey(KeyCode.Space))
            return direction.none;
        return buforDirection;
    }

    public void SetVector(direction current)
    {
        switch(current)
        {
            case direction.left:
                Vdirection = Vector2.left;
                break;
            case direction.right:
                Vdirection = Vector2.right;
                break;
            case direction.up:
                Vdirection = Vector2.up;
                break;
            case direction.down:
                Vdirection = Vector2.down;
                break;
            case direction.none:
                Vdirection = Vector2.zero;
                break;
        }
    }
    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
    public void ResetStudent()
    {
        currentDirection = direction.none;
        buforDirection = direction.none;
        transform.position = new Vector2(16.0f, 25.0f);
        transform.rotation = Quaternion.Euler(0, 0, 90.0f);
    }
}

