using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    public TextMesh textCount;

    [SyncVar(hook = nameof(SetCount))]
    public int count = 0;
    

    private void Start() {
        if (isLocalPlayer) {
            var camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMove>();
            camera._target = this.transform;
        }

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
        textCount.text = $"{newCount}";
    }

    [Command]
    public void CmdAddCount() {
        count++;
        // var player = GameObject.FindWithTag("Player");
        // player.transform.position = new Vector3(1f, 1f, 1f);
    }
}