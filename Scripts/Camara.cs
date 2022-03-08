using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class Camara : XCamaraBehavior
{
    [SerializeField] GestorSensibilidad gestor;
    
    float f_x_rotacion = 0f;

    bool b_estaListaLaRed = false;
    Camera camera;


    protected override void NetworkStart()
    {
        base.NetworkStart();
        b_estaListaLaRed = true;
        camera = GetComponent<Camera>();

        if (networkObject.IsOwner)
            camera.enabled = true;
    }

    void Update()
    {
        if (!b_estaListaLaRed)
            return;

        MoverCamara();
        ActualizarRotacion();
    }

    void MoverCamara()
    {
        float f_mouseY = Input.GetAxis("Mouse Y") * gestor.F_sensibilidad * Time.deltaTime;

        f_x_rotacion -= f_mouseY;
        f_x_rotacion = Mathf.Clamp(f_x_rotacion, -90f, 90f);

        transform.localRotation = Quaternion.Euler(f_x_rotacion, 0f, 0f);
    }
    void ActualizarRotacion()
    {
        if (networkObject.IsOwner)
            networkObject.rotation = transform.rotation;
        else
            transform.rotation = networkObject.rotation;

    }
}
