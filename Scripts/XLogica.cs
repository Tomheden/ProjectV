using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine.UI;

public class XLogica : XLogicaBehavior
{
	[SerializeField] Transform[] t_posicion;
	[SerializeField] GameObject[] go_txtJugador;
    int i_spawn = 0;
    bool estaLista = false;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        i_spawn = networkObject.i_spawn;
        networkObject.SendRpc(RPC_X_USUARIO_CONECTADO, Receivers.All, 1);
        if (networkObject.i_spawn > 6)
            Destroy(GameObject.Find("BotonJugar"));
        estaLista = true;
    }

    public void InstanciaPersonaje()
    {
        if (!estaLista)
            return;
        if (networkObject.i_spawn <= 6)
        {
            NetworkManager.Instance.InstantiateXMovimiento(i_spawn, t_posicion[i_spawn].position);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameObject.Find("CanvasMenu").GetComponent<Canvas>().enabled = false;
        }
    }

    public void Observar()
    {
        GameObject.Find("BotonJugar").SetActive(false);
        GameObject.Find("BotonEspectear").SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public override void XUsuarioConectado(RpcArgs args)
    {
        networkObject.i_spawn += args.GetNext<int>();
    }
}