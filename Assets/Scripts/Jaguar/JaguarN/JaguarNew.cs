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
    public Vector2 VDirection = Vector2.zero, HomePosition;
    public Vector3Int homeCords;
    public int pktPokonanieJaguara = 100, index, EasyRndDur = 10, EasyChsDur = 10, MediumRndDur = 8, MediumChsDur = 12, HardRndDur = 6, HardChsDur = 14;
    public bool active = true;
    void Start()
    {
        HomePosition = transform.position;
        this.movement = GetComponent<Movement>();
        this.node = GetComponent<Node>();
        homeCords = Vector3Int.FloorToInt(transform.position);
        Invoke(nameof(ZamurujHome), 0.25f);
    }

    void Update()
    {
        if (active)
        {
            movement.setDirection(VDirection);
            if (currentMode == JaguarMode.Random || currentMode == JaguarMode.Chase)
                LoopMode();
        }
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
                if (node.availableDirection[index] == -movement.currentDirection && node.availableDirection.Count > 1)
                {
                    index++;
                    if (index >= node.availableDirection.Count)
                    {
                        index = 0;
                    }
                }
                break;
            case JaguarMode.Chase:
                index = distancesToStudent.IndexOf(distancesToStudent.Min());
                break;
            case JaguarMode.Frightened:
                index = distancesToStudent.IndexOf(distancesToStudent.Max());
                break;
            case JaguarMode.Home:
                index = distancesToHome.IndexOf(distancesToHome.Min());
                break;
        }
        VDirection = node.availableDirection[index];
    }

    //easy: random - 10s, chase - 10s
    //medium: random - 8s, chase - 12s
    //hard: random - 6s, chase - 14s
    public void LoopMode()
    {
        switch(GameManager.level)
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
    public void ResetJaguar(JaguarNew jaguar)
    {
        this.transform.position = jaguar.HomePosition;
        OdmurujHome();
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
        currentMode = JaguarMode.Chase;
    }
    public void SetRandom()
    {
        currentMode = JaguarMode.Random;
    }
    public void SetHome(JaguarNew jaguar)
    {
        jaguar.currentMode = JaguarMode.Home;
    }
    public void SetFrightened()
    {
        currentMode = JaguarMode.Frightened;
    }

}