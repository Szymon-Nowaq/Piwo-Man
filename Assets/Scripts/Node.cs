using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Node : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            Debug.Log("Node");
            //FindObjectOfType<StudentMove>().StopStudent();
        }
    }
}
