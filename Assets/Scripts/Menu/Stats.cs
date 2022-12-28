using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
