using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Begin : MonoBehaviour
{
    [Header("NetworkManager")]
    public NetworkManager networkManager;
    public InputField ipInput;

    [Header("Select Player")]
    public Canvas selectPlayer;
    public GameObject playerUIPrefab;

    public bool isHost = false;

    void Start()
    {
        ipInput.text = "localhost";

        if (!Application.isBatchMode)
        {
            networkManager.StartClient();
        }
    }

    public void goToSelectPlayer(bool isHost)
    {
        this.isHost = isHost;

        this.GetComponent<Canvas>().enabled = false;

        selectPlayer.enabled = true;
    }

    public void StartGame()
    {
        if (isHost) networkManager.StartHost();
        else
        {
            networkManager.networkAddress = ipInput.text;
            networkManager.StartClient();
        }
    }

    [SerializeField]
    private PotionData _PotionData = new PotionData()
    {
        potion_name = "acasdadas",
        value = 1,
        pos = new Vector3(1f, 1f, 1f),
        effect = new List<Effect>
            {
                new Effect()
                {
                    name = "123",
                    desc = "321"
                },
                new Effect()
                {
                    name = "sa",
                    desc = "3assa21"
                }
            }
    };

    public void AddNewPlayer(GameObject content)
    {
        Instantiate(playerUIPrefab, content.transform);

        string potion = JsonUtility.ToJson(_PotionData);

        GameManager.SaveJson(potion, "test123.json");

        string data = GameManager.ReadFromFile("test123.json");
        PotionData lod = JsonUtility.FromJson<PotionData>(data);
        Debug.Log(lod.value);
    }

    [System.Serializable]
    public class PotionData
    {
        public string potion_name;
        public int value;
        public Vector3 pos;
        public List<Effect> effect = new List<Effect>();
    }

    [System.Serializable]
    public class Effect
    {
        public string name;
        public string desc;
    }
}
