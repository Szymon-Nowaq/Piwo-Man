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
    direction currentDirection = direction.none, buforDirection = direction.none; // zmienna przechowujaca "kierunki"

    public LayerMask scianyLayer;
    public Vector2 Vdirection = Vector2.zero;
    public Vector2 oldposition;
    public Vector2 newposition;

    public new Rigidbody2D rb { get; private set; }
    void Start()
    {
        transform.position = new Vector2(15.0f, 29.0f);
        rb = GetComponent<Rigidbody2D>();
        speed = 15.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // zatrzymanie studenta, wlasnorecznie spacja lub na skrzyzowaniu
            currentDirection = direction.none;
        if (currentDirection == direction.none) // jak stoimy (skrzyzowanie) to dopiero wtedy mo�emy sobie wybra� kierunek     
            currentDirection = setDirection();
        else // jak sie ruszamy, to mozemy zmienic kierunek na przeciwny
        {
            buforDirection = setDirection(); // ustalamy nowy kierunek, ale trzeba sprawdzic czy jest on przeciwny, wiec bufor
            if (isDirectionOpposite(buforDirection, currentDirection)) // fajna funkcja
            {
                currentDirection = buforDirection;
            }
        }
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
        return direction.none;
    }
    bool isDirectionOpposite(direction a, direction b)
    {
        if ((a + 3 == b) || (a - 3 == b)) // mmmmm ta uniwersalnosc
            return true;
        return false;
    }

    public void TouchedNode()
    {
        switch (currentDirection)
        {
            case direction.up:
                rb.MovePosition(rb.position + Vector2.up);
                break;
            case direction.down:
                rb.MovePosition(rb.position + Vector2.down);
                break;
            case direction.right:
                rb.MovePosition(rb.position + Vector2.right);
                break;
            case direction.left:
                rb.MovePosition(rb.position + Vector2.left);
                break;
        }
        StopStudent();
    }

    public void StopStudent()
    {
        currentDirection = direction.none;
    }

    public void GetNewPosition()
    {
        newposition = rb.position;
    }
}

