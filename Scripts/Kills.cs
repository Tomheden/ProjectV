using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Kills : XKillsBehavior
{
    [SerializeField] Text text;
    int i_contKills = 0;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
            text.enabled = true;
    }

    public void EnemigoAsesinado()
    {
        i_contKills++;
        text.text = i_contKills + " kills";

        Debug.Log(i_contKills);
    }
}
