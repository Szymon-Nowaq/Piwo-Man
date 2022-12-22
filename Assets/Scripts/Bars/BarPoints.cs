using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarPoints : MonoBehaviour
{
    public Slider slider;

    public void setPoints(int points)
    {
        slider.value = points;
    }

}
