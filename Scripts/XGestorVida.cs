using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine.UI;

public class XGestorVida : XGestorVidaBehavior
{
    [SerializeField] Jugador jugador;
    [SerializeField] GameObject[] puntosDeSpawn;
    [SerializeField] GameObject particulaKill;
    [SerializeField] Text t;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
            t.enabled = true;
    }
    public override void XActualizarVida(RpcArgs args)
    {
        jugador.i_vida += args.GetNext<int>();
        ComprobarVida();
    }

    public void ActualizarVida(int cantidadRecibida)
    {
        networkObject.SendRpc(RPC_X_ACTUALIZAR_VIDA, Receivers.All, cantidadRecibida);

    }


    public void ComprobarVida()
    {
        if (networkObject.IsOwner)
        {
            t.text = jugador.i_vida + "HP";
            if (jugador.i_vida == 0)
                Morir();
        }  
    }

    public override void XMorir(RpcArgs args)
    {
        GameObject part = Instantiate(particulaKill, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
        Destroy(part, 3);
    }

    void Morir()
    { 
        int random = Random.Range(0, 6);
        gameObject.transform.position = puntosDeSpawn[random].transform.position;
        networkObject.SendRpc(RPC_X_MORIR, Receivers.All);
        
        ActualizarVida(100);
        jugador.rifle.i_capacidad = 30;
        t.text = jugador.i_vida + "HP";

    }

}
