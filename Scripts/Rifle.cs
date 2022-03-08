using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine.UI;


public class Rifle : XRifleBehavior
{
    int i_daño = 10;
    float f_alcance = 100f;
    public int i_capacidad = 30;
    public float f_velocidadDisparo = 15f;
    [SerializeField] GameObject particula1;
    [SerializeField] Text t;
    [SerializeField] Text mira;
    bool estaLista = false;

    [SerializeField] GameObject go_disparo;

    public override void XDisparo(RpcArgs args)
    {
        CrearImpacto(args.GetNext<Vector3>(), args.GetNext<Quaternion>());
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            t.enabled = true;
            mira.enabled = true;
        }
            
        estaLista = true;
    }

    public void Disparar()
    {
        if (i_capacidad>0 && !b_recargando)
        {
            
            RaycastHit hit;
            if (Physics.Raycast(go_disparo.transform.position, go_disparo.transform.forward, out hit, f_alcance))
            {
                if (hit.collider.tag == "Jugador")
                {
             
                    XGestorVida gestorVida = hit.collider.GetComponent<XGestorVida>();
                    gestorVida.ActualizarVida(-i_daño);
                    if (hit.collider.GetComponent<Jugador>().i_vida==0)
                        GetComponentInParent<Kills>().EnemigoAsesinado();
                }
            }

            CrearImpacto(hit.point, Quaternion.LookRotation(hit.normal));
            networkObject.SendRpc(RPC_X_DISPARO, Receivers.Others, hit.point, Quaternion.LookRotation(hit.normal));

            i_capacidad--;
        } 
    }
    bool b_recargando = false;

    private void Update()
    {
        if (!estaLista)
            return;

        t.text = i_capacidad + "/30";
        if (Input.GetKeyDown(KeyCode.R) && i_capacidad < 30 || i_capacidad == 0 && !b_recargando)
            StartCoroutine(Recargar());
    }

    IEnumerator Recargar()
    {
        b_recargando = true;
        yield return new WaitForSeconds(1.75f);
        b_recargando = false;
        i_capacidad = 30;
    }
    public void SetParticula(GameObject particula)
    {
        particula1 = particula;
    }

    public void CrearImpacto(Vector3 v3_pos, Quaternion q_rot)
    {
        GameObject particula = Instantiate(particula1, v3_pos, q_rot);
        Destroy(particula, 2f);
    }

}
