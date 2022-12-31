using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plot : MonoBehaviour
{

    private void Start()
    {
        Invoke(nameof(powrotmenu), 31);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        powrotmenu();
    }
    
    public void powrotmenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
