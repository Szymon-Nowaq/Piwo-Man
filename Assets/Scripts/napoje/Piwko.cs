using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piwko : MonoBehaviour
{
    public int points = 1;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PiwoWypite(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            Eat();
        }
    }
}
