using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void Easy()
    {
        GameManager.level = GameManager.Level.easy;
        SceneManager.LoadScene("Piwo-man");
    }
    public void Medium()
    {
        GameManager.level = GameManager.Level.medium;
        SceneManager.LoadScene("Piwo-man");
    }
    public void Hard()
    {
        GameManager.level = GameManager.Level.hard;
        SceneManager.LoadScene("Piwo-man");
    }
}
