using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform student;
    public float miasteczkoX;
    public float miasteczkoY;
    void Start()
    {
       
    }

    void Update()
    {
        if(MathF.Abs(student.position.x) <= 60)
            transform.position = new Vector3(student.position.x, transform.position.y, transform.position.z);
        if (MathF.Abs(student.position.y) <= 25)
            transform.position = new Vector3(transform.position.x, student.position.y, transform.position.z);
    }
}
