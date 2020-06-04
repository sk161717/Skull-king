using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public enum COLOR
    {
        RED, BLUE, YELLOW, BLACK, NOCOLOR
    }

    public struct CardData
    {
        public int cardIndex;
        public COLOR cardColor;
    }

    public int player;
    public List<CardData> cardList = new List<CardData>();
    public List<int> playableIndex = new List<int>();
    private COLOR mustFollow;
    [SerializeField] GameObject card = default;
    private int cardCount;
    public Launcher launcher;
    // Start is called before the first frame update
    void Start()
    {
        if (!MagicNumberKeeper.Instance.isSolo)
        {
            player = PhotonNetwork.player.ID;
        }
        EventManager.Instance.passCardData.AddListener(ResisterCard);
        EventManager.Instance.cardNotification.AddListener(RemoveCard);
        EventManager.Instance.broadcastMustFollow.AddListener(ResisterMustFollow);
        EventManager.Instance.roundStart.AddListener(InitializeAtRound);
        EventManager.Instance.cycleStart.AddListener(InitializeAtCycle);
        NetworkDebugger.Instance.AddDebugText("Player Instantiated");
        launcher.PlayerConfirm();
    }
    private void InitializeAtCycle()
    {
        playableIndex.Clear();
        mustFollow = COLOR.NOCOLOR;
    }
    private void InitializeAtRound(int numOfCard)
    {
        cardCount = 0;
        cardList.Clear();
    }

    public void StorePlayableCard()
    {
        playableIndex.Clear();
        bool haveMustFollowCard = false;
        foreach (CardData item in cardList)
        {
            if (mustFollow == item.cardColor)
            {
                haveMustFollowCard = true;
            }
        }
        foreach (CardData item in cardList)
        {
            if (haveMustFollowCard)
            {
                if ((item.cardColor == COLOR.NOCOLOR) || (item.cardColor == mustFollow))
                {
                    playableIndex.Add(item.cardIndex);
                }
                else
                {
                    EventManager.Instance.publishUnPlayableCard.Invoke(item.cardIndex);
                }
            }
            else
            {
                playableIndex.Add(item.cardIndex);
            }
        }
    }

    private void ResisterCard(int cardIndex, int playerOfCard)
    {
        if (playerOfCard == player)
        {

            if (!MagicNumberKeeper.Instance.isSolo||playerOfCard==1)
            {
                float cardOffset = 1f;
                float co = cardOffset * cardCount; //オフセット幅の計算
                Vector3 temp = MagicNumberKeeper.Instance.HandCardPos() + new Vector3(co, 0f);             
                CardModel cardModel = Instantiate(card, temp, card.transform.rotation).GetComponent<CardModel>();
                cardModel.cardIndex = cardIndex;
                cardModel.Player = playerOfCard;
                cardModel.ToggleFace(true);
                cardCount++;
            }

            CardData cardData = new CardData();
            cardData.cardIndex = cardIndex;
            cardData.cardColor = JudgeColor(cardIndex);
            cardList.Add(cardData);
            NetworkDebugger.Instance.AddDebugText("added");
        }
    }

    private COLOR JudgeColor(int cardIndex)
    {
        if (cardIndex < 13)
        {
            return COLOR.RED;
        }
        else if (cardIndex < 26)
        {
            return COLOR.BLUE;
        }
        else if (cardIndex < 39)
        {
            return COLOR.YELLOW;
        }
        else if (cardIndex < 52)
        {
            return COLOR.BLACK;
        }
        else
        {
            return COLOR.NOCOLOR;
        }
    }


    private void RemoveCard(int cardIndex, int playerOfCard)
    {
        if (playerOfCard == player)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                if (cardList[i].cardIndex == cardIndex)
                {
                    cardList.RemoveAt(i);
                }
            }
        }
    }

    private void ResisterMustFollow(int mustFollow_)
    {
        mustFollow = (COLOR)mustFollow_;
        StorePlayableCard();
    }
}
