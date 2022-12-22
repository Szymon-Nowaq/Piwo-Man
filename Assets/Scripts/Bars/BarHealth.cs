using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarHealth : MonoBehaviour
{
    public Slider slider;

    public void setHealth(int health)
    {
        slider.value = health;
    }
    
}
