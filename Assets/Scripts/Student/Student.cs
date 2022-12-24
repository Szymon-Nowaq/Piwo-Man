using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public Movement movement;
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetStudent()
    {
        transform.position = new Vector2(16.0f, 25.0f);
        transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        this.movement.Stop();
    }
}
