using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wodeczka : Piwko
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().WodkaWypita(this);
    }
}
