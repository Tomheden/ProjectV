using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int i_vida = 100;
    public  Rifle rifle;
    float proxDisparo = 0f;
    
    
    void Start()
    {
        rifle = GetComponentInChildren<Rifle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= proxDisparo && rifle.networkObject.IsOwner)
        {
            proxDisparo = Time.time + 1.2f / rifle.f_velocidadDisparo;
            rifle.Disparar();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("CanvasSalir").GetComponent<Canvas>().enabled = true;
        }
    }

}
