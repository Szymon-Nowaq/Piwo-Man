using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirection { get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {
        this.availableDirection = new List<Vector2>();

        CheckAvailableDirections(Vector2.up);
        CheckAvailableDirections(Vector2.down);
        CheckAvailableDirections(Vector2.left);
        CheckAvailableDirections(Vector2.right);
        Debug.Log(availableDirection.Count);
    }

    private void CheckAvailableDirections(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0f, direction, 1.0f, this.obstacleLayer);
        
        if (hit.collider == null)
        {
            this.availableDirection.Add(direction);
        }
    }
}
