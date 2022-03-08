using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public int i = 0;

    void Start()
    {
        //FindObjectOfType<XLogica>().InstanciaPersonaje(i);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jugador" && i <= 6)
        {
            i++;
        }
    }


}
