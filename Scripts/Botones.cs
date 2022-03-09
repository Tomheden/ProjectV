using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
    }

    public void Continuar()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("CanvasSalir").GetComponent<Canvas>().enabled = false;
    }
    
}
