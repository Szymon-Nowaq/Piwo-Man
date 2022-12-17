using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody2D))]
public class hindusMovement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        //transform.position = new Vector2(15.0f, 29.0f);
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) // jak wcisniemy jakis klawisz to zmieniamy do "currentDirection" 
            SetDirection(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow))
            SetDirection(Vector2.right);
        if (Input.GetKey(KeyCode.DownArrow))
            SetDirection(Vector2.down);
        if (Input.GetKey(KeyCode.UpArrow))
            SetDirection(Vector2.up);
        if (Input.GetKey(KeyCode.Space))
            direction = Vector2.zero;
        // Try to move in the next direction while it's queued to make movements
        // more responsive
    }

    private void FixedUpdate()
    {
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + translation);
        SetRotation();
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            Debug.Log("kolizja");
            nextDirection = direction;
        }
    }

    public void SetRotation()
    {
        if(direction == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, 270.0f);
        if (direction == Vector2.up)
            transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        if (direction == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 0.0f);
        if (direction == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

}