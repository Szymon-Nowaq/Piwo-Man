using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class CameraFollow : MonoBehaviour
{
    public Camera camera;
    public Transform targetObject;
    public Transform miasteczko;
    private Vector3 initialOffset;
    private Vector3 cameraPosition;
    void Start()
    {
        initialOffset = transform.position - targetObject.position;
        Debug.Log((int)camera.scaledPixelHeight);
        Debug.Log((int)camera.scaledPixelWidth);
    }
    void FixedUpdate()
    {
        if (((Mathf.Abs(camera.transform.position.x)) <= miasteczko.transform.localScale.x)
        && ((Mathf.Abs(camera.transform.position.y)) <= miasteczko.transform.localScale.y))
        {
            cameraPosition = targetObject.position + initialOffset;
            transform.position = cameraPosition;
        }
    }
}
