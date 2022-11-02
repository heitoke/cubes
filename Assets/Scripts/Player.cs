using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
// using Unity.VisualScripting;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetId))]
    public string Id;

    [SyncVar(hook = nameof(SetPosition))]
    public double position;

    public TextMesh textCount;

    [SyncVar(hook = nameof(SetCount))]
    public int count = 0;

    public Material material;

    [Header("Style Player")]
    [SyncVar(hook = nameof(SetColor))]
    public string hexColor;

    public Text textId;

    private void Start() {
        if (isLocalPlayer) {
            var camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMove>();
            camera._target = this.transform;
            camera.player = this;

            camera.SetCursor(false);

            Window window = GameObject.FindWithTag("Window").GetComponent<Window>();
            window.player = this;

            Connection connection = GameObject.FindWithTag("Connection").GetComponent<Connection>();
            connection.localPlayer = this;
            connection.CmdAddPlayer(this);

            camera.connection = connection;

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
        base.OnStartClient();
        Debug.Log("Client");
        var hud = GameObject.FindWithTag("HUD").GetComponent<Canvas>();
        hud.enabled = true;
        CmdSetId(GetRandomID());

        Connection connection = GameObject.FindWithTag("Connection").GetComponent<Connection>();
        CmdSetPosition(connection.listPlayers.Count + 1);
        if (connection.listPlayers.Count <= 2) return;
        var spawnPoint = GameObject.Find("SpawnPoint");
        spawnPoint.transform.position = new Vector3(0, connection.listPlayers.Count, 0);
    }

    #region

    private void SetId(string oldValue, string newValue)
    {
        // this.textId.text = newValue;
        Debug.Log(newValue);
        if (isLocalPlayer) GameObject.FindWithTag("PlayerId").GetComponent<Text>().text = newValue;
    }

    [Command]
    public void CmdSetId(string Id)
    {
        this.Id = Id;
    }

    #endregion

    #region

    private void SetPosition(double oldValue, double newValue)
    {
        if (isLocalPlayer) this.position = newValue;
    }

    [Command]
    public void CmdSetPosition(double position)
    {
        this.position = position;
    }

    #endregion

    private void SetCount(int oldCount, int newCount) {
        textCount.text = $"{newCount}";
    }

    [Command]
    public void CmdAddCount() {
        count++;
    }

    #region SetColor

    Color color;
    private void SetColor(string oldValue, string newValue)
    {
        if (ColorUtility.TryParseHtmlString(newValue, out color))
        {
            MeshRenderer mesh = this.GetComponent<MeshRenderer>();

            mesh.material.color = color;
        }
    }

    [Command]
    public void CmdSetColor(string hexColor)
    {
        this.hexColor = hexColor;
    }

    #endregion

    public static string GetRandomID()
    {
        string ID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if (rand < 26)
            {
                ID += (char)(rand + 65);
            }
            else
            {
                ID += (rand - 26).ToString();
            }
        }
        return ID;
    }
}