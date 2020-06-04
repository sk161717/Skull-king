using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject GameFlowManager = default;
    [SerializeField] GameObject WinnerCalculator = default;
    [SerializeField] GameObject UIManager = default;
    [SerializeField] GameObject Player = default;
    [SerializeField] GameObject Agent = default;
    bool isUIManagerConstructed = false;
    bool isPlayerConstructed = false;
    int playerConstructed;
    bool isConstructCalled = false;

    // Start is called before the first frame update

    void Awake()
    {
        EventManager.Instance.construct.AddListener(ConstructCalled);
        EventManager.Instance.sendName.AddListener(ResisterName);
        playerConstructed = 0;
    }
    void Start()
    {
        if (MagicNumberKeeper.Instance.isSolo)
        {
            GameObject UIManagerCopy = Instantiate(UIManager);
            UIManagerCopy.GetComponent<UIManager>().launcher = this;

            for (int i = 1; i <= MagicNumberKeeper.Instance.NumberOfPeople; i++)
            {
                Player player = Instantiate(Player).GetComponent<Player>();
                player.launcher = this;
                player.player = i;
                if (i != 1)
                {
                    Agent agentCopy = Instantiate(Agent).GetComponent<Agent>();
                    agentCopy.agentID = i;
                    agentCopy.player = player;
                }
            }

            GameObject GameFlowManagerCopy = Instantiate(GameFlowManager);
            GameObject WinnerCalculatorCopy = Instantiate(WinnerCalculator);
            GameFlowManagerCopy.GetComponent<GameFlowManager>().WinnerCalculator = WinnerCalculatorCopy;
        }
        else
        {
            NetworkEventHandler.Instance.SendName(PlayerPrefs.GetString("name"));                           //名前を通知
            MagicNumberKeeper.Instance.nameList[PhotonNetwork.player.ID] = PlayerPrefs.GetString("name");

            int ID = PhotonNetwork.player.ID;

            GameObject UIManagerCopy = Instantiate(UIManager);
            UIManagerCopy.GetComponent<UIManager>().launcher = this;

            GameObject PlayerCopy = Instantiate(Player);
            PlayerCopy.GetComponent<Player>().launcher = this;

            if (ID == 1)
            {
                GameObject GameFlowManagerCopy = Instantiate(GameFlowManager);
                GameObject WinnerCalculatorCopy = Instantiate(WinnerCalculator);
                GameFlowManagerCopy.GetComponent<GameFlowManager>().WinnerCalculator = WinnerCalculatorCopy;
            }
        }
    }

    public void UIManagerConfirm()
    {
        NetworkDebugger.Instance.AddDebugText("uimanager_launcher confirm");
        isUIManagerConstructed = true;
        CallResponse();
    }

    public void PlayerConfirm()
    {
        NetworkDebugger.Instance.AddDebugText("player confirm");
        if (MagicNumberKeeper.Instance.isSolo)
        {
            playerConstructed++;
        }
        else
        {
            isPlayerConstructed = true;
        }
        CallResponse();
    }

    void CallResponse()
    {

        if (MagicNumberKeeper.Instance.isSolo)
        {
            if (isUIManagerConstructed &&isConstructCalled&&playerConstructed==MagicNumberKeeper.Instance.NumberOfPeople)
            {
                EventManager.Instance.responseToConstruct.Invoke();
                NetworkDebugger.Instance.AddDebugText("call responses1");
            }
        }
        else
        {
            if (isUIManagerConstructed && isPlayerConstructed && isConstructCalled)
            {
                if (PhotonNetwork.player.ID == 1)
                {
                    EventManager.Instance.responseToConstruct.Invoke();
                    NetworkDebugger.Instance.AddDebugText("call responses1");
                }
                else
                {
                    NetworkEventHandler.Instance.ResponseToConstruct();
                    NetworkDebugger.Instance.AddDebugText("call responses234");
                }
            }
        }
    }

    void ConstructCalled()
    {
        NetworkDebugger.Instance.AddDebugText("construct confirm");
        isConstructCalled = true;
        CallResponse();
    }

    void ResisterName(string name, int id)
    {
        MagicNumberKeeper.Instance.nameList[id] = name;
        NetworkDebugger.Instance.AddDebugText(name);
    }
}
