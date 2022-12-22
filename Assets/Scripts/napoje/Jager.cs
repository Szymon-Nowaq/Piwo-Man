using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jager : Piwko
{
    public int jagerHealth = 1;
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().JagerWypity(this);
    }
}
