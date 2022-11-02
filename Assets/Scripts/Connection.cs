using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Connection : NetworkBehaviour
{
    public readonly SyncList<Player> listPlayers = new SyncList<Player>();

    public Player localPlayer;

    [Command(requiresAuthority = false)]
    public void CmdAddPlayer(Player player)
    {
        listPlayers.Add(player);
        Debug.Log(listPlayers.Count);
    }
}
