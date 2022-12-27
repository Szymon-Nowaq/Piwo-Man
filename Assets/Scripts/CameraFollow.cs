using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Student student;
    public float miasteczkoX;
    public float miasteczkoY;
    void Start()
    {
       
    }

    void Update()
    {
        if(MathF.Abs(student.transform.position.x) <= 30)
            transform.position = new Vector3(student.transform.position.x, transform.position.y, transform.position.z);
        if (MathF.Abs(student.transform.position.y) <= 12.5f)
            transform.position = new Vector3(transform.position.x, student.transform.position.y, transform.position.z);
    }

    public void ResetCamera()
    {
        transform.position = student.homeCords;
    }
}
