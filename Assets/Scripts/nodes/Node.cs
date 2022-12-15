using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected virtual void Stop()
    {
        FindObjectOfType<StudentMove>().TouchedNode();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            Stop();
        }
    }
}
