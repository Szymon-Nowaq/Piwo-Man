using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JaguarNew : MonoBehaviour
{
    public enum JaguarMode { Scatter, Chase, Frightened, Home }
    JaguarMode currentMode;

    public Transform student;
    public Vector3Int homeCords;
    public Movement movement { get; private set; }
    public Tilemap sciany;
    public TileBase tilePoziomy, tileLacznik, tileZakretLewo, tileZakretPrawo;
    public Vector2 VDirection = Vector2.zero, HomePosition;
    Node node;
    int index;
    public int pktPokonanieJaguara = 100;
    void Start()
    {
        HomePosition = transform.position;
        currentMode = JaguarMode.Scatter;
        this.movement = GetComponent<Movement>();
        this.node = GetComponent<Node>();
        homeCords = Vector3Int.FloorToInt(transform.position);
        Invoke(nameof(ZamurujHome), 0.25f);
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
        node = other.GetComponent<Node>();
        index = UnityEngine.Random.Range(0, node.availableDirection.Count);
        if (node.availableDirection[index] == -movement.currentDirection && node.availableDirection.Count > 1)
        {
            index++;
            if (index >= node.availableDirection.Count)
            {
                index = 0;
            }
        }
        VDirection = node.availableDirection[index];
    }

    public void ZamurujHome()
    {
        sciany.SetTile(homeCords + Vector3Int.up, tilePoziomy);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left, tileLacznik);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.right, tileLacznik);
    }

    public void OdmurujHome()
    {
        sciany.SetTile(homeCords + Vector3Int.up, null);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left, tileZakretLewo);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.right, tileZakretPrawo);
    }

    public void ResetJaguar()
    {
        this.movement.setDirection(Vector2.zero);
        this.transform.position = HomePosition;
        OdmurujHome();
    }
}