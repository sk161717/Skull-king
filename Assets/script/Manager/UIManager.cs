using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject canvas;
    [SerializeField] GameObject trashUI = default;
    [SerializeField] GameObject ScaryMaryUI = default;
    [SerializeField] GameObject yohohoUI = default;
    [SerializeField] GameObject thisTurnPlayerUI = default;
    public Launcher launcher;
    private int numberOfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlayer = MagicNumberKeeper.Instance.NumberOfPeople;
        canvas = GameObject.Find("Canvas");
        MakeYohohoUI();
        MakeTakeAwayCardUI();
        MakeScaryMaryUI();
        MakeThisTurnPlayerUI();
    }

    private void MakeThisTurnPlayerUI()
    {
        GameObject Copy = Instantiate(thisTurnPlayerUI);
        Copy.transform.parent = canvas.transform;
        Copy.GetComponent<RectTransform>().localPosition = MagicNumberKeeper.Instance.ThisTurnPlayerPos();
    }

    private void MakeScaryMaryUI()
    {

        GameObject ScaryMaryUICopy = Instantiate(ScaryMaryUI);
    }

    private void MakeYohohoUI()
    {
        for (int i = 1; i <= numberOfPlayer; i++)
        {
            GameObject yohohoUICopy = Instantiate(yohohoUI);
            YohohoUI yohohoUI_component = yohohoUICopy.GetComponent<YohohoUI>();
            yohohoUI_component.uIManager = this;
            yohohoUI_component.Player = i;
            yohohoUICopy.SetActive(true);
        }
    }

    private void MakeTakeAwayCardUI()
    {
        for (int i = 1; i <= numberOfPlayer; i++)
        {
            if (MagicNumberKeeper.Instance.isSolo)
            {
                GameObject trashUICopy = Instantiate(trashUI, MagicNumberKeeper.Instance.CardTrashPos(i), trashUI.transform.rotation);
                TakeAwayCardUI takeAwayCardUI = trashUICopy.GetComponent<TakeAwayCardUI>();
                takeAwayCardUI.player = i;
            }
            else
            {
                GameObject trashUICopy = Instantiate(trashUI, MagicNumberKeeper.Instance.CardTrashPos(i), trashUI.transform.rotation);
                TakeAwayCardUI takeAwayCardUI = trashUICopy.GetComponent<TakeAwayCardUI>();
                takeAwayCardUI.player = (PhotonNetwork.player.ID+i-2)%numberOfPlayer+1;
            }     
        }
    }

    public void Confirm()
    {
        NetworkDebugger.Instance.AddDebugText("UImanager confirm ");
        NetworkDebugger.Instance.AddDebugText("numberofplayer:" + numberOfPlayer);
        launcher.UIManagerConfirm();
    }
}
