using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Student : MonoBehaviour
{
    public Movement movement { get; private set; }
    public Tilemap sciany;
    public Tile tilePoziomy, tileZakretPrawo, tileZakretLewo;
    public Student student;
    public Vector3Int homeCords;
    void Start()
    {
        movement = GetComponent<Movement>();
        homeCords = Vector3Int.FloorToInt(transform.position);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            movement.setDirection(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow))
            movement.setDirection(Vector2.right);
        if (Input.GetKey(KeyCode.DownArrow))
            movement.setDirection(Vector2.down);
        if (Input.GetKey(KeyCode.UpArrow))
            movement.setDirection(Vector2.up);
        if (Input.GetKey(KeyCode.Space))
            movement.setDirection(Vector2.zero);
        if (movement.currentDirection == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        if (movement.currentDirection == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (movement.currentDirection == Vector2.up)
            transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        if (movement.currentDirection == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, 270.0f);
        if (transform.position.y > 14.4)
            ZamurujPiwo();
    }
    public void ResetStudent()
    {
        transform.position = new Vector2(8.0f, 12.5f);
        transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        movement.setDirection(Vector2.zero);
        //OdmurujPiwo();
    }

    public void ZamurujPiwo()
    {
        sciany.SetTile(homeCords + Vector3Int.up, tilePoziomy);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.right, tilePoziomy);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left, tilePoziomy);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left + Vector3Int.left, tilePoziomy);
    }
    public void OdmurujPiwo()
    {
        sciany.SetTile(homeCords + Vector3Int.up, null);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.right, tileZakretPrawo);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left, null);
        sciany.SetTile(homeCords + Vector3Int.up + Vector3Int.left + Vector3Int.left, tileZakretLewo);
    }
}
