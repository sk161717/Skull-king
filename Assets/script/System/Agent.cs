using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public int agentID;
    private int numberOfCards;
    private int thisTurnPlayer;
    public Player player;
    System.Random res = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.roundStart.AddListener(ResisterVariable);
        EventManager.Instance.alertThisTurnPlayer.AddListener(ChooseCard);
        EventManager.Instance.yo_ho_ho.AddListener(Yohoho);
        EventManager.Instance.InvokeScaryMary.AddListener(ChoiseScaryMary);
    }

    void ResisterVariable(int numberOfPlayCard)
    {
        numberOfCards = numberOfPlayCard;       
    }

    void Yohoho()
    {
        EventManager.Instance.responseToyo_Ho_Ho.Invoke(agentID,res.Next(0,numberOfCards+1));
    }

    void ChooseCard(int playerID)
    {
        thisTurnPlayer = playerID;
        if (playerID == agentID)
        {
            int rand;
            while (true)
            {
                System.Random res = new System.Random();
                int lenOfCardList = player.cardList.Count;
                Debug.Log(lenOfCardList);
                rand = res.Next(0, lenOfCardList);
                if (IsIncluded(player.cardList[rand].cardIndex))
                {
                    break;
                }
            }
            NetworkDebugger.Instance.AddDebugText("trashcard: " + player.cardList[rand].cardIndex + ",player: " + playerID);
            EventManager.Instance.cardNotification.Invoke(player.cardList[rand].cardIndex, playerID);           
        }
    }

    bool IsIncluded(int cardIndex)
    {
        int len = player.playableIndex.Count;
        if (len == 0)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < player.playableIndex.Count; i++)
            {
                if (player.playableIndex[i] == cardIndex)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void ChoiseScaryMary()
    {
        if (thisTurnPlayer == agentID)
        {
            EventManager.Instance.sendScaryMaryChoice.Invoke(true);
        }        
    }
}
