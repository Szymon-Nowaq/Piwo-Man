using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JaguarNew : MonoBehaviour
{
    public enum JaguarMode { Scatter, Chase, Frightened, Home }
    JaguarMode currentMode;

    public Transform student;
    public Movement movement { get; private set; }
    public Vector2 VDirection = Vector2.zero;
    Node node;
    int index;
    public int pktPokonanieJaguara = 100;
    public bool isOnNode = false;
    void Start()
    {
        currentMode = JaguarMode.Scatter;
        this.movement = GetComponent<Movement>();
        this.node = GetComponent<Node>();
    }

    void Update()
    {
        switch (currentMode)
        {
            case JaguarMode.Scatter:
                    movement.setDirection(VDirection);
                break;
            case JaguarMode.Chase:
                break;
            case JaguarMode.Frightened:
                break;
            case JaguarMode.Home:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            if (currentMode == JaguarMode.Frightened)
            {
                FindObjectOfType<GameManager>().JaguarPokonany(this);
            }
            else
            {
                FindObjectOfType<GameManager>().StudentZgon();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider node jaguar");
        node = other.GetComponent<Node>();
        index = UnityEngine.Random.Range(0, node.availableDirection.Count);
        //zeby ziomek se nie chodzi³ lewo prawo bo to dziwne lol
        if (node.availableDirection[index] == -movement.currentDirection && node.availableDirection.Count > 1)
        {
            index++;
            if (index >= node.availableDirection.Count)
            {
                index = 0;
            }
        }
        isOnNode = true;
        VDirection = node.availableDirection[index];
        //Debug.Log(VDirection);
    }
}