using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YohohoUI : MonoBehaviour
{
    [SerializeField]
    GameObject yohohoConsole = default;
    [SerializeField]
    GameObject decideButton = default;
    [SerializeField]
    GameObject expectationUI = default;
    [SerializeField]
    GameObject victoryUI = default;
    [SerializeField]
    GameObject pointUI = default;
    [SerializeField]
    GameObject nameUIObj = default;

    GameObject canvas;
    public UIManager uIManager;
    MagicNumberKeeper magicNumberKeeper;
    private int player;
    private int numberOfCard;
    public int Player
    {
        get { return player; }
        set { player = value; }
    }
    GameObject yohohoConsoleCopy, decideButtonCopy, expectationUICopy, victoryUICopy, pointUICopy,nameUICopy;

    private bool yohohoConsoleFlag = false;
    private bool decideButtonFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        magicNumberKeeper = MagicNumberKeeper.Instance;
        int UIIndex;

        if (MagicNumberKeeper.Instance.isSolo)
        {
            UIIndex = player;
            if (player == 1)
            {
                InstantiateYohohoUI();
            }
        }
        else
        {
            UIIndex= (player-PhotonNetwork.player.ID+magicNumberKeeper.NumberOfPeople) % magicNumberKeeper.NumberOfPeople + 1;
            if (PhotonNetwork.player.ID == player)
            {
                InstantiateYohohoUI();
            }
        }


        expectationUICopy = Instantiate(expectationUI);
        expectationUICopy.GetComponent<ShowExpect>().yohohoUI = this.gameObject;
        expectationUICopy.transform.parent = canvas.transform;
        RectTransform rectTransformExpect = expectationUICopy.GetComponent<RectTransform>();
        rectTransformExpect.localPosition = magicNumberKeeper.ExpectationPos(UIIndex);

        victoryUICopy = Instantiate(victoryUI);
        victoryUICopy.GetComponent<ShowVictory>().yohohoUI = this.gameObject;
        victoryUICopy.transform.parent = canvas.transform;
        RectTransform rectTransformVictory = victoryUICopy.GetComponent<RectTransform>();
        rectTransformVictory.localPosition = magicNumberKeeper.VictoryPos(UIIndex);

        pointUICopy = Instantiate(pointUI);
        pointUICopy.GetComponent<ShowPoint>().yohohoUI = this.gameObject;
        pointUICopy.transform.parent = canvas.transform;
        RectTransform rectTransformPoint = pointUICopy.GetComponent<RectTransform>();
        rectTransformPoint.localPosition = magicNumberKeeper.PointPos(UIIndex);

        nameUICopy = Instantiate(nameUIObj);
        nameUICopy.transform.parent = canvas.transform;
        RectTransform rectTransformName = nameUICopy.GetComponent<RectTransform>();
        rectTransformName.localPosition = MagicNumberKeeper.Instance.NamePos(UIIndex);
        NameUI nameUI = nameUICopy.transform.GetChild(0).GetComponent<NameUI>();
        nameUI.playerID = player;
        if (MagicNumberKeeper.Instance.nameList.ContainsKey(player))
        {
            nameUI.name = MagicNumberKeeper.Instance.nameList[player];
            nameUI.Activate();
        }


        EventManager.Instance.yo_ho_ho.AddListener(ActivateUI);
        EventManager.Instance.roundEnd.AddListener(ResisterPoint);
        EventManager.Instance.roundStart.AddListener(ResisterNumOfCard);
        NetworkDebugger.Instance.AddDebugText("YohohoUI Instantiated");
    }

    private void InstantiateYohohoUI()
    {
        yohohoConsoleCopy = Instantiate(yohohoConsole);
        yohohoConsoleCopy.transform.parent = canvas.transform;
        yohohoConsoleCopy.GetComponent<YohohoConsole>().yohohoUI = this;
        RectTransform rectTransformConsole = yohohoConsoleCopy.GetComponent<RectTransform>();
        rectTransformConsole.localPosition = magicNumberKeeper.ConsolePos();

        decideButtonCopy = Instantiate(decideButton);
        decideButtonCopy.GetComponent<DecideButton>().yohohoUI = this;
        decideButtonCopy.transform.parent = canvas.transform;
        RectTransform rectTransformdecide = decideButtonCopy.GetComponent<RectTransform>();
        rectTransformdecide.localPosition = magicNumberKeeper.DecideButtonPos();
        decideButtonCopy.GetComponent<DecideButton>().Confirm();
    }

    private void ActivateUI()
    {
        if (MagicNumberKeeper.Instance.isSolo)
        {
            if (player == 1)
            {
                yohohoConsoleCopy.SetActive(true);
                decideButtonCopy.SetActive(true);
            }
        }
        else
        {
            if (PhotonNetwork.player.ID == player)
            {
                NetworkDebugger.Instance.AddDebugText("ID:" + PhotonNetwork.player.ID);
                NetworkDebugger.Instance.AddDebugText("player:" + player);
                yohohoConsoleCopy.SetActive(true);
                decideButtonCopy.SetActive(true);
            }
        }
        
    }


    public void ResponseToYohoho()
    {
        EventManager.Instance.responseToyo_Ho_Ho.Invoke(player, yohohoConsoleCopy.GetComponent<YohohoConsole>().count);
        NetworkEventHandler.Instance.ResponseToYohoho(player, yohohoConsoleCopy.GetComponent<YohohoConsole>().count);

        yohohoConsoleCopy.SetActive(false);
        decideButtonCopy.SetActive(false);
    }

    private void ResisterPoint()
    {
        int listener_count = EventManager.Instance.roundEnd.GetPersistentEventCount();
        NetworkDebugger.Instance.AddDebugText("listener=" + listener_count);
        int expectation = expectationUICopy.GetComponent<ShowExpect>().expectation;
        int victory = victoryUICopy.GetComponent<ShowVictory>().victory;
        int bonus = victoryUICopy.GetComponent<ShowVictory>().bonus;
        int point;
        if (expectation == victory)
        {
            if (expectation == 0)
            {
                point = 10 * numberOfCard+bonus;
            }
            else
            {
                point = 20 * victory+bonus;
            }
        }
        else
        {
            if (expectation == 0)
            {
                point = (-10) * numberOfCard;
            }
            else
            {
                point = Mathf.Abs(expectation - victory) * (-10);
            }
        }
        pointUICopy.GetComponent<ShowPoint>().ResisterPoint(point);
    }

    private void ResisterNumOfCard(int numberOfCard_)
    {
        numberOfCard = numberOfCard_;
    }

    public void ConfirmYohohoConsoleInstantiate()
    {
        NetworkDebugger.Instance.AddDebugText("yohohoConsole confirm");
        yohohoConsoleFlag = true;
        CallParentFunc();
        yohohoConsoleCopy.SetActive(false);
    }

    public void ConfirmDecideButtonInstantiate()
    {
        NetworkDebugger.Instance.AddDebugText("decidebutton confirm");
        decideButtonFlag = true;
        CallParentFunc();
        decideButtonCopy.SetActive(false);
    }
    void CallParentFunc()
    {
        if (yohohoConsoleFlag && decideButtonFlag)
        {
            uIManager.Confirm();
        }
    }
}
