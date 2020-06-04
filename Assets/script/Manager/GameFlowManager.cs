using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    //static variables
    private enum STATE
    {
        CONSTRUCT, CYCLESTART, ROUNDSTART, WAINTINGFORYOHOHO, PASSTONEXTPLAYER, WAITINGFORCARD, CYCLEEND,HIGHLIGHT, ROUNDEND, IDLE
    }
    Deck deck;
    private int numberOfPlayer;
    public GameObject WinnerCalculator;
    //active variables
    private int defaultParent;
    private int parent;
    private int nextplayer;
    private int isAlreadyPlayed = 0;
    private int responseToYoHoHo = 0;
    private STATE state = STATE.CONSTRUCT;
    private bool isCardSubmitted = false;
    private int isConstructed = 0;
    private int remainCard;
    private float count;

    public int NumberOfPlayCard { get; private set; } = 1;



    // Start is called before the first frame update
    void Start()
    {
        defaultParent = 1;
        numberOfPlayer = MagicNumberKeeper.Instance.NumberOfPeople;
        deck = GetComponent<Deck>();
        EventManager.Instance.responseToyo_Ho_Ho.AddListener(GetResponse);
        EventManager.Instance.responseToConstruct.AddListener(ReceiveResponseToConstruct);
        EventManager.Instance.cardNotification.AddListener(ReceiveCardSubmit);
        EventManager.Instance.construct.Invoke();
        NetworkEventHandler.Instance.Construct(); 
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.CONSTRUCT:
                if (MagicNumberKeeper.Instance.isSolo)
                {
                    if (isConstructed == 1)
                    {
                        state = STATE.ROUNDSTART;
                    }
                }
                else
                {
                    if (isConstructed == numberOfPlayer)
                    {
                        state = STATE.ROUNDSTART;
                    }
                }
                break;
            case STATE.ROUNDSTART:
                EventManager.Instance.roundStart.Invoke(NumberOfPlayCard);
                NetworkEventHandler.Instance.RoundStart(NumberOfPlayCard);
                HandOut(NumberOfPlayCard);
                YoHoHo();
                parent = defaultParent;
                remainCard = NumberOfPlayCard;
                state = STATE.WAINTINGFORYOHOHO;
                break;
            case STATE.WAINTINGFORYOHOHO:
                if (responseToYoHoHo == numberOfPlayer)
                {
                    responseToYoHoHo = 0;
                    state = STATE.CYCLESTART;
                }
                break;
            case STATE.CYCLESTART:
                EventManager.Instance.cycleStart.Invoke();
                NetworkEventHandler.Instance.CycleStart();
                state = STATE.PASSTONEXTPLAYER;
                break;
            case STATE.PASSTONEXTPLAYER:
                if (isAlreadyPlayed == numberOfPlayer)
                {
                    isAlreadyPlayed = 0;
                    state = STATE.CYCLEEND;
                }
                else
                {
                    if (isAlreadyPlayed == 0)
                    {
                        nextplayer = parent;
                    }
                    ThrowTurn(nextplayer);
                    Debug.Log("nextplayer" + nextplayer);
                    state = STATE.WAITINGFORCARD;
                }

                break;
            case STATE.WAITINGFORCARD:
                if (isCardSubmitted == true)
                {
                    nextplayer = (nextplayer % numberOfPlayer) + 1;
                    isAlreadyPlayed++;
                    isCardSubmitted = false;
                    state = STATE.PASSTONEXTPLAYER;
                }
                break;
            case STATE.CYCLEEND:
                parent = JudgeWinner();
                remainCard--;
                state = STATE.HIGHLIGHT;
                break;
            case STATE.HIGHLIGHT:
                count+=Time.deltaTime;
                if (count > 5.0f)
                {
                    if (remainCard == 0)
                    {
                        state = STATE.ROUNDEND;
                    }
                    else
                    {
                        state = STATE.CYCLESTART;
                    }
                    count = 0f;
                }
                break;
            case STATE.ROUNDEND:
                EventManager.Instance.roundEnd.Invoke();
                NetworkEventHandler.Instance.RoundEnd();
                NumberOfPlayCard++;
                defaultParent = (defaultParent % numberOfPlayer) + 1;
                state = STATE.ROUNDSTART;
                break;
            case STATE.IDLE:
                break;
            default:
                break;
        }

    }

    private void ReceiveCardSubmit(int cardIndex, int playerOfTrashedCard)
    {
        isCardSubmitted = true;
    }

    private void ThrowTurn(int nextplayer)
    {
        EventManager.Instance.alertThisTurnPlayer.Invoke(nextplayer);
        NetworkEventHandler.Instance.ThrowTurn(nextplayer);
    }

    private void YoHoHo()
    {
        EventManager.Instance.yo_ho_ho.Invoke();
        NetworkEventHandler.Instance.Yo_ho_ho();
    }

    private void GetResponse(int player, int expectationOfWin)
    {
        responseToYoHoHo++;
    }

    private void ReceiveResponseToConstruct()
    {
        isConstructed++;
    }

    private int JudgeWinner()
    {
        return WinnerCalculator.GetComponent<WinnerCalculator>().JudgeWinner();
    } 
    private void HandOut(int numberOfPlayCard)
    {
        deck.Shuffle();
        int cardIndex = 0;
        for (int i = 1; i <= numberOfPlayer; i++)
        {
            int playerIndex = i;
            HandoutOnePlayer(cardIndex, playerIndex);
            cardIndex += numberOfPlayCard;
        }
    }
    private void HandoutOnePlayer(int cardIndex, int playerIndex)
    {
        for (int i = cardIndex; i < NumberOfPlayCard + cardIndex; i++)
        {
            if (MagicNumberKeeper.Instance.isSolo)
            {
                EventManager.Instance.passCardData.Invoke(deck.GetCard(i), playerIndex);
            }
            else
            {
                if (playerIndex == 1)
                {
                    EventManager.Instance.passCardData.Invoke(deck.GetCard(i), playerIndex);
                }
                else
                {
                    NetworkEventHandler.Instance.PassCardData(deck.GetCard(i), playerIndex);
                }
            }
        }       
    }
}
