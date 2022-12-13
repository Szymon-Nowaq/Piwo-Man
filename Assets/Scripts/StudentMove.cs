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
    public new Rigidbody2D rb { get; private set; }
    public GameObject[] nodes;
    void Start() 
    {
        transform.rotation = Quaternion.Euler(0, 0, 90.0f); // ustalamy poczatkow� rotacj�, jaka� dziwna funkcja ze stackoverflow, chuj wie jak dzia�a, wa�ne �e dzia�a, dajemy rotacje w X,Y,Z 
        rb = GetComponent<Rigidbody2D>();
        speed = 10.0f;
        nodes = Resources.LoadAll<GameObject>("Prefabs/Skrzyzowanie");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || isOnNode(nodes)) // zatrzymanie studenta, wlasnorecznie spacja lub na skrzyzowaniu
        {
            currentDirection = direction.none;
            setVector(currentDirection); // po kazdej zmianie enumowej zmiennej zmieniamy nasz wektor (potrzebne do systemu kolizji)
        }
        if (currentDirection == direction.none) // jak stoimy (skrzyzowanie) to dopiero wtedy mo�emy sobie wybra� kierunek     
        {
            currentDirection = setDirection();
            setVector(currentDirection);
        }
        else // jak sie ruszamy, to mozemy zmienic kierunek na przeciwny
        {
            buforDirection = setDirection(); // ustalamy nowy kierunek, ale trzeba sprawdzic czy jest on przeciwny, wiec bufor
            if (isDirectionOpposite(buforDirection, currentDirection)) // fajna funkcja
            {
                currentDirection = buforDirection;
                setVector(currentDirection);
            }
        }
        // Vector2 translation = Vdirection * speed * Time.deltaTime;
        //this.rigidbody.MovePosition(rigidbody.position + translation);
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

    public direction setDirection ()
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
    bool isDirectionOpposite(direction a , direction b)
    {
        if ((a + 3 == b) || (a - 3 == b)) // mmmmm ta uniwersalnosc
            return true;
        return false;
    }
    public bool isOnNode(GameObject[] nodes)
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            if (transform.position == nodes[i].transform.position)
                return true;
        }
        return false;
    }
    
    public void setVector(direction current)
    {
        switch (current)
        {
            case direction.left:
                Vdirection = new Vector2(-1, 0);
                break;
            case direction.right:
                Vdirection = new Vector2(1, 0);
                break;
            case direction.up:
                Vdirection = new Vector2(0, 1);
                break;
            case direction.down:
                Vdirection = new Vector2(0, -1);
                break;
            case direction.none:
                Vdirection = new Vector2(0, 0);
                break;
            default:
                break;
        }
    }
}