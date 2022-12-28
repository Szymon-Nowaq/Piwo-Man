using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{
    public TextMeshProUGUI time, drunkBeers, jaguarsKilled, deaths;

    void Start()
    {
        time.text = GameManager.statsTime.ToString();
        drunkBeers.text = GameManager.statsBeers.ToString();
        jaguarsKilled.text = GameManager.statsKilled.ToString();
        deaths.text = GameManager.statsDeaths.ToString();
    }
}
