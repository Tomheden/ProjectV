using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class XMovimiento : XMovimientoBehavior
{
    Vector3 v3_movimiento;
    float f_velocidad = 6f;
    CharacterController cc_control;
    [SerializeField] GestorSensibilidad gestor;

    bool b_estaListaLaRed = false;
    float f_gravedad = -12;
    bool b_tocaSuelo;
    Vector3 v3_mov_y;
    
    protected override void NetworkStart()
    {
        base.NetworkStart();
        b_estaListaLaRed = true;
        cc_control = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!b_estaListaLaRed)
            return;

        Rotar();
        Movimiento();
        Caminar();
        ActualizarPosicion();
    }

    void Movimiento()
    {
        float f_x = Input.GetAxis("Horizontal");
        float f_z = Input.GetAxis("Vertical");
        if (networkObject.IsOwner)
        {
            v3_movimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * f_velocidad * Time.deltaTime;
            //b_tocaSuelo = Physics.Raycast(transform.position, Vector3.down, cc_control.height / 2 + 0.1f);

            if (!cc_control.isGrounded)
            {
                v3_mov_y.y += f_gravedad * Time.deltaTime;
            } else
            {
                v3_mov_y = Vector3.down;
                if (Input.GetKeyDown(KeyCode.Space))
                    v3_mov_y += new Vector3(0, 6, 0);
            }
            Vector3 v3_mov_x = transform.TransformVector(v3_movimiento).normalized * f_velocidad;
            cc_control.Move((v3_mov_x + v3_mov_y) * Time.deltaTime);
            
        } 
    }
    void Rotar()
    {
        transform.eulerAngles = transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * gestor.F_sensibilidad;
    }

    void Caminar()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            f_velocidad = 2;
        else
            f_velocidad = 6f;
    }

    void ActualizarPosicion()
    {
        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
        }

    }
}
