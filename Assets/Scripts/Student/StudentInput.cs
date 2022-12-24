using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentInput : Student
{
    void Update()
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
    }
}
