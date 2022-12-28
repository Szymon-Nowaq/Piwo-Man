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
    public Student student;
    public Movement movement { get; private set; }
    public AnimatedSprite animation { get; private set; }
    Node node;
    public Tilemap nodes, sciany;
    public TileBase tileNode, tileBlack;
    public Vector2 VDirection = Vector2.zero, VbeforeDirection;
    public Vector3 HomePosition, vd3;
    public Vector3Int homeCords;
    public Vector2[] tabVector = { Vector2.right, Vector2.left, Vector2.down, Vector2.up };
    public int index, idx = 1;
    public float easyRndDur, easyChsDur, mediumRndDur, mediumChsDur, hardRndDur, hardChsDur, normalSpeed = 6.5f, homeSpeed = 3.0f;
    void Start()
    {
        switch (GameManager.level)
        {
            case GameManager.Level.easy:
                easyRndDur = 4.0f;
                easyChsDur = 1.0f;
                break;
            case GameManager.Level.medium:
                mediumRndDur = 2.0f;
                mediumChsDur = 2.0f;
                break;
            case GameManager.Level.hard:
                hardRndDur = 2.0f;
                hardChsDur = 3.0f;
                break;
        }
        VDirection = Vector2.up;
        this.movement = GetComponent<Movement>();
        this.node = GetComponent<Node>();
        this.animation = GetComponent<AnimatedSprite>();
    }
    void Update()
    {
        if (currentMode == JaguarMode.Home)
            isNextHome();
        vd3 = VDirection;
            
    }
    void FixedUpdate()
    {
        movement.setDirection(VDirection);
        if (movement.currentDirection == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        if (movement.currentDirection == Vector2.right || movement.currentDirection == Vector2.zero)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (movement.currentDirection == Vector2.up)
            transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        if (movement.currentDirection == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, 270.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            if (currentMode == JaguarMode.Frightened)
            {
                animation.isJagFri = false;
                FindObjectOfType<GameManager>().JaguarPokonany(this);
            }
            else if (currentMode != JaguarMode.Home)
                FindObjectOfType<GameManager>().StudentZgon();
            else
                transform.position = transform.position + vd3;
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
                distancesToHome.Add(Vector2.Distance((Vector2)this.transform.position + node.availableDirection[i], HomePosition));
            switch (currentMode)
            {
                case JaguarMode.Random:
                    index = UnityEngine.Random.Range(0, node.availableDirection.Count);
                    break;
                case JaguarMode.Chase:
                    int minI = 0;
                    for (int i = 1; i < node.availableDirection.Count; i++)
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
                    if (distancesToStudent[maxI] > 10)
                        index = UnityEngine.Random.Range(0, node.availableDirection.Count);
                    break;
                case JaguarMode.Home:
                    int minIH = 0;
                    for (int i = 1; i < node.availableDirection.Count; i++)
                    {
                        if (distancesToHome[i] < distancesToHome[minIH])
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
    public void ResetJaguar()
    {
        StopAllCoroutines();
        Invoke(nameof(Setnormalspeed),0.1f);
        currentMode = JaguarMode.Random;
        Invoke(nameof(SetRandom), 0.1f);
        this.transform.position = HomePosition;
        DestroyBlocks();
        VDirection = Vector2.up;
        Invoke(nameof(SetDirectionLR), 0.3f);
        Invoke(nameof(CreateBlocks), 0.5f);
    }

    public void CreateBlocks()
    {
        sciany.SetTile(homeCords + Vector3Int.up, tileBlack);
    }
    public void DestroyBlocks()
    {
        sciany.SetTile(homeCords + Vector3Int.up, null);
    }

    public void SetChase()
    {
        this.currentMode = JaguarMode.Chase;
        this.movement.SetSpeed(normalSpeed);
        switch (GameManager.level)
        {
            case GameManager.Level.easy:
                Invoke(nameof(SetRandom), easyChsDur);
                break;
            case GameManager.Level.medium:
                Invoke(nameof(SetRandom), mediumChsDur);
                break;
            case GameManager.Level.hard:
                Invoke(nameof(SetRandom), hardChsDur);
                break;
        }
    }
    public void SetRandom()
    {
        animation.isJagFri = false;
        this.currentMode = JaguarMode.Random;
        this.movement.SetSpeed(normalSpeed);
        switch (GameManager.level)
        {
            case GameManager.Level.easy:
                Invoke(nameof(SetChase), easyRndDur);
                break;
            case GameManager.Level.medium:
                Invoke(nameof(SetChase), mediumRndDur);
                break;
            case GameManager.Level.hard:
                Invoke(nameof(SetChase), hardRndDur);
                break;
        }
    }
    public void SetHome()
    {
        if (this.movement.Occupied(student.movement.currentDirection))
        {
            int i = 0;
            for (; this.movement.Occupied(tabVector[i]); i++)
                ;
            VDirection = tabVector[i];
            vd3 = tabVector[i];
            transform.position = transform.position + vd3;
        }
        else
            VDirection = student.movement.currentDirection;
        this.movement.SetSpeed(homeSpeed);
        CancelInvoke(nameof(SetChase));
        CancelInvoke(nameof(SetRandom));
        this.currentMode = JaguarMode.Home;
        DestroyBlocks();
    }

    public void SetFrightened()
    {
        CancelInvoke(nameof(SetChase));
        CancelInvoke(nameof(SetRandom));
        if (this.currentMode != JaguarMode.Home)
        {
            this.currentMode = JaguarMode.Frightened;
            animation.isJagFri = true;
            this.movement.SetSpeed(4);
            Invoke(nameof(SetRandom), 10.0f);
        }
    }

    public bool isNextHome()
    {
        int x = homeCords.x, y = homeCords.y + 2;
        if ((x - 1 <= transform.position.x && transform.position.x <= x + 2) && (y <= transform.position.y && transform.position.y <= y + 1))
        {
            transform.position = HomePosition;
            VDirection = Vector2.zero;
            this.movement.SetSpeed(normalSpeed);
            Invoke(nameof(ResetJaguar), 10);
            return true;
        }
        return false;
    }

    public bool isEnteringBase()
    {
        int x1 = 4, x2 =30, y = 25;
        if (((x1 - 1 <= transform.position.x && transform.position.x <= x1 + 1) && (y <= transform.position.y && transform.position.y <= y + 1)) ||
            ((x2 - 1 <= transform.position.x && transform.position.x <= x2 + 1) && (y <= transform.position.y && transform.position.y <= y + 1)))
        {
            return true;
        }
        return false;
    }

    public void SetDirectionLR()
    { 
        VDirection = tabVector[idx];
    }
    public void Setnormalspeed()
    {
        this.movement.SetSpeed(normalSpeed);
    }
}