using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayPlot()
    {
        ;
    }
    public void ShowStats()
    {
        SceneManager.LoadScene("Stats");
    }
}
