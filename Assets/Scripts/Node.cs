using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirection { get; private set; }
    public LayerMask obstacleLayer;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            Debug.Log("Node");
            //FindObjectOfType<StudentMove>().StopStudent();
        }
    }

    

    private void Start()
    {
        this.availableDirection = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0f, direction, 1.0f, this.obstacleLayer);
        
        if (hit.collider == null)
        {
            this.availableDirection.Add(direction);
        }
    }
}
