using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSensibilidad : MonoBehaviour
{
    float f_sensibilidad = 150f;

    public float F_sensibilidad { get => f_sensibilidad; }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            f_sensibilidad += Mathf.Clamp(10f, 0, 1000);

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            f_sensibilidad = Mathf.Clamp(f_sensibilidad-10f,0, 1000);
    }
}
