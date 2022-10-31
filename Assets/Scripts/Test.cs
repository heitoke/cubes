using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Test : NetworkBehaviour
{

    [SyncVar(hook = nameof(SetCount))]
    public int count = 0;

    private void Start() {
        Debug.Log($"is - {isLocalPlayer}");
    }

    private void Update() {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.F)) {
            CmdAddCount();
            Debug.Log('F');
        }
    }


    public override void OnStartServer()
    {
        Debug.Log("Server");
    }

    public override void OnStartClient()
    {
        Debug.Log("Client");
        // CmdAddCount();
        // count += 2;
    }

    private void SetCount(int oldCount, int newCount) {
        Debug.Log($"Old - {oldCount}");
        Debug.Log($"New - {newCount}");
    }

    [Command]
    public void CmdAddCount() {
        var player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(1f, 1f, 1f);
    }
}