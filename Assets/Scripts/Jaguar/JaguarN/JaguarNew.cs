using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JaguarNew : MonoBehaviour
{
    public enum JaguarMode { Random, Chase, Frightened, Home }
    public JaguarMode currentMode;
    public Transform student;
    public Movement movement { get; private set; }
    Node node;
    public Tilemap sciany;
    public TileBase tilePoziomy, tileLacznik, tileZakretLewo, tileZakretPrawo;
    public Vector2 VDirection = Vector2.zero;
    public Vector3 HomePosition;
    public Vector3Int homeCords;
    public int pktPokonanieJaguara = 100, index;
    public float EasyRndDur = 10.0f, EasyChsDur = 10.0f, MediumRndDur = 8.0f, MediumChsDur = 12.0f, HardRndDur = 6.0f, HardChsDur = 14.0f;
    void Start()
    {
        currentMode = JaguarMode.Random;
        //HomePosition = this.transform.position;
        this.movement = GetComponent<Movement>();
        this.node = GetComponent<Node>();
        homeCords = Vector3Int.FloorToInt(transform.position);
        Invoke(nameof(ZamurujHome), 0.25f);
        Invoke(nameof(SetChase), EasyRndDur);
    }
    private void Update()
    {
       
    }
    void FixedUpdate()
    {
        movement.setDirection(VDirection);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            if (currentMode == JaguarMode.Frightened)
                FindObjectOfType<GameManager>().JaguarPokonany(this);
            else
                FindObjectOfType<GameManager>().StudentZgon();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        node = other.GetComponent<Node>();
        List<double> distancesToStudent;
        distancesToStudent = new List<double>();
        for (int i = 0; i < node.availableDirection.Count; i++)
            distancesToStudent.Add(Vector2.Distance((Vector2)this.transform.position + node.availableDirection[i], student.transform.position));
        List<double> distancesToHome;
        distancesToHome = new List<double>();
        for (int i = 0; i < node.availableDirection.Count; i++)
            distancesToStudent.Add(Vector2.Distance((Vector2)this.transform.position + node.availableDirection[i], HomePosition));
        switch (currentMode)
        {
            case JaguarMode.Random:
                index = UnityEngine.Random.Range(0, node.availableDirection.Count);
                break;
            case JaguarMode.Chase:
                int minI = 0;
                for(int i = 1; i < node.availableDirection.Count; i++)
                {
                    if (distancesToStudent[i] < distancesToStudent[minI])
                        minI = i;
                }
                index = minI;
                break;
            case JaguarMode.Frightened:
                int maxI = 0;
                for (int i = 1; i < node.availableDirection.Count; i++)
                {
                    if (distancesToStudent[i] > distancesToStudent[maxI])
                        maxI = i;
                }
                index = maxI;
                break;
            case JaguarMode.Home:
                int minIH = 0;
                for (int i = 1; i < node.availableDirection.Count; i++)
                {
                    if (distancesToHome[i] < distancesToStudent[minIH])
                        minIH = i;
                }
                index = minIH;
                break;
        }
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

    //easy: random - 10s, chase - 10s
    //medium: random - 8s, chase - 12s
    //hard: random - 6s, chase - 14s
    public void LoopMode()
    {
        if(this.currentMode == JaguarMode.Random || this.currentMode == JaguarMode.Chase)
        {
            switch (GameManager.level)
            {
                case GameManager.Level.easy:
                    Invoke(nameof(SetChase), EasyRndDur);
                    Invoke(nameof(SetRandom), EasyChsDur);
                    break;
                case GameManager.Level.medium:
                    Invoke(nameof(SetChase), MediumRndDur);
                    Invoke(nameof(SetRandom), MediumChsDur);
                    break;
                case GameManager.Level.hard:
                    Invoke(nameof(SetChase), HardRndDur);
                    Invoke(nameof(SetRandom), HardChsDur);
                    break;
            }
        }
    }
    public void ResetJaguar()
    {
        movement.Stop();
        this.transform.position = HomePosition;
        OdmurujHome();
        Invoke(nameof(ZamurujHome), 0.25f);
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

    public void SetChase()
    {
        this.currentMode = JaguarMode.Chase;
        switch (GameManager.level)
        {
            case GameManager.Level.easy:
                Invoke(nameof(SetRandom), EasyChsDur);
                break;
            case GameManager.Level.medium:
                Invoke(nameof(SetRandom), MediumChsDur);
                break;
            case GameManager.Level.hard:
                Invoke(nameof(SetRandom), HardChsDur);
                break;
        }
    }
    public void SetRandom()
    {
        this.currentMode = JaguarMode.Random;
        switch (GameManager.level)
        {
            case GameManager.Level.easy:
                Invoke(nameof(SetChase), EasyRndDur);
                break;
            case GameManager.Level.medium:
                Invoke(nameof(SetChase), MediumRndDur);
                break;
            case GameManager.Level.hard:
                Invoke(nameof(SetChase), HardRndDur);
                break;
        }
    }
    public void SetHome()
    {
        this.currentMode = JaguarMode.Home;
    }
    public void SetFrightened()
    {
        this.currentMode = JaguarMode.Frightened;
    }

}