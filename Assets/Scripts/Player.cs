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

    public Material material;
    

    private void Start() {
        if (isLocalPlayer) {
            var camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMove>();
            camera._target = this.transform;

            camera.SetCursor(false);
        }

        material = GetComponent<MeshRenderer>().material;
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
        var hud = GameObject.FindWithTag("HUD").GetComponent<Canvas>();
        hud.enabled = true;
    }

    private void SetCount(int oldCount, int newCount) {
        textCount.text = $"{newCount}";
    }

    [Command]
    public void CmdAddCount() {
        count++;
    }
}