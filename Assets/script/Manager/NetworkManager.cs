using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private int maxPlayers_ = 6;
    private int playersOfSolo = 4;
    int people;
    public int ID;
    public bool isSolo;
    [SerializeField] GameObject startUI = default;
    [SerializeField] GameObject Launcher = default;
    GameObject startUICopy;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void Start()
    {
        isSolo = false;
    }

    public void ConnectNetwork() {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }


    public void Host(string roomName)
    {
        NetworkDebugger.Instance.AddDebugText("host called");
        RoomOptions op = new RoomOptions() { isVisible = true, maxPlayers = (byte)maxPlayers_ };
        PhotonNetwork.CreateRoom(roomName, op, TypedLobby.Default);
    }

    public void Guest(string roomName)
    {
        NetworkDebugger.Instance.AddDebugText("guest called");
        PhotonNetwork.JoinRoom(roomName);
    }

    public void Solo()
    {
        isSolo = true;
        SceneManager.LoadScene("MainScene");
    }

    void OnJoinedLobby()
    {
        TitleEventManager.Instance.joinLobby.Invoke();
    }

    void OnJoinedRoom()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnPhotonCreateRoomFailed()
    {
        NetworkDebugger.Instance.AddDebugText("create room failed");
    }

    void OnPhotonJoinRoomFailed()
    {
        NetworkDebugger.Instance.AddDebugText("join room failed");
    }

    void OnSceneChanged(Scene pastScene, Scene thisScene)
    {
        if (thisScene.name == "MainScene")
        {
            if (isSolo)
            {
                GenerateStartUI();
            }
            else
            {
                EventManager.Instance.sendName.AddListener(ResisterName);
                string name = PlayerPrefs.GetString("name");

                if (PhotonNetwork.player.ID == 1)
                {
                    GenerateStartUI();
                    MagicNumberKeeper.Instance.nameList[1] = name;
                }
                else
                {
                    NetworkEventHandler.Instance.SendName(name);  //入室アラート
                }
                NetworkDebugger.Instance.AddDebugText(name);
            }          
            NetworkDebugger.Instance.AddDebugText(ID.ToString() + " player joined");
        }
    }

    public void Launch()
    {
        if (isSolo)
        {
            MagicNumberKeeper.Instance.isSolo = true;
            MagicNumberKeeper.Instance.NumberOfPeople = playersOfSolo;
            NetworkEventHandler.Instance.isSolo = true;
            startUICopy.SetActive(false);
            Instantiate(Launcher);
        }
        else
        {
            people = PhotonNetwork.room.PlayerCount;
            MagicNumberKeeper.Instance.NumberOfPeople = people;
            MagicNumberKeeper.Instance.isSolo = false;
            NetworkEventHandler.Instance.SetPlayerNum(people);
            startUICopy.SetActive(false);
            PhotonNetwork.Instantiate("Launcher", Vector3.zero, Quaternion.identity, 0);
        }
    }

    void GenerateStartUI()
    {
        startUICopy = Instantiate(startUI);
        startUICopy.GetComponent<StartUI>().networkManager = this;
        GameObject canvas = GameObject.Find("Canvas");
        startUICopy.transform.parent = canvas.transform;
        RectTransform rectTransformStart = startUICopy.GetComponent<RectTransform>();
        rectTransformStart.localPosition = MagicNumberKeeper.Instance.StartUIPos();
    }

    void ResisterName(string name,int id)
    {
        MagicNumberKeeper.Instance.nameList[id]=name;
        NetworkDebugger.Instance.AddDebugText(name);
    }
}