using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wodeczka : Piwko
{
    public int ponits = 1;
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().WodkaWypita(this);
    }
}
