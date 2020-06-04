using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCalculator : MonoBehaviour
{
    private enum COLOR
    {
        RED, BLUE, YELLOW, BLACK, NOCOLOR
    }
    private COLOR mustFollow = COLOR.NOCOLOR;
    private int tempWinnerIndex;
    private int tempWinnerPlayerIndex;
    private bool isScaryMaryPirate = true;
    private bool scaryMaryFlag = true;  //マリーが提出されたらup.up状態でchoiceが来たら
    private bool isMermaidPlayed;
    private int mermaidPlayer;
    private bool isSkullKingPlayed;
    private int playedPirate;
    private int bonus;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        EventManager.Instance.cardNotification.AddListener(StockTrashedCardData);
        EventManager.Instance.cycleStart.AddListener(Initialize);
        EventManager.Instance.sendScaryMaryChoice.AddListener(ResisterScaryMary);

    }


    private void StockTrashedCardData(int cardIndex, int playerOfTrashedCard)
    {
        NetworkDebugger.Instance.AddDebugText("cardnotif");
        if (JudgeTempWinner(cardIndex,playerOfTrashedCard))
        {
            tempWinnerPlayerIndex = playerOfTrashedCard;
        }
        JudgeMustFollow(cardIndex);
    }

    private bool JudgeTempWinner(int cardIndex,int playerOfTrashedCard)
    {
        if (cardIndex < 39)                       //red,blue,yellow
        {
            if (tempWinnerIndex > 60 || (tempWinnerIndex == 60 && (!isScaryMaryPirate)))
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
            else if (JudgeColor(cardIndex) == JudgeColor(tempWinnerIndex))
            {
                if (cardIndex > tempWinnerIndex)
                {
                    tempWinnerIndex = cardIndex;
                    return true;
                }
            }
        }
        else if (cardIndex < 52)                   //black
        {
            if (tempWinnerIndex < 39)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
            else if (tempWinnerIndex < 52)
            {
                if (cardIndex > tempWinnerIndex)
                {
                    tempWinnerIndex = cardIndex;
                    return true;
                }
            }
            else if (tempWinnerIndex == 60)
            {
                if (!isScaryMaryPirate)
                {
                    tempWinnerIndex = cardIndex;
                    return true;
                }

            }
            else if (tempWinnerIndex > 60)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
        }
        else if (cardIndex < 54)                  //mermaid
        {
            if (!isMermaidPlayed)
            {
                isMermaidPlayed = true;
                mermaidPlayer = playerOfTrashedCard;
            }
            
            if (tempWinnerIndex < 52)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
            else if (tempWinnerIndex == 59)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
            else if (tempWinnerIndex == 60)
            {
                if (!isScaryMaryPirate)
                {
                    tempWinnerIndex = cardIndex;
                    return true;
                }
            }
            else if (tempWinnerIndex > 60)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
        }
        else if (cardIndex < 59 || (cardIndex == 60 && isScaryMaryPirate)) //pirate or scarymary=pirate
        {
            playedPirate++;
            if (tempWinnerIndex < 54)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
            else if (tempWinnerIndex == 60)
            {
                if (!isScaryMaryPirate)
                {
                    tempWinnerIndex = cardIndex;
                    return true;
                }
            }
            else if (tempWinnerIndex > 60)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
        }
        else if (cardIndex == 59)                             //skull king
        {
            isSkullKingPlayed = true;
            if (tempWinnerIndex < 52 || 53 < tempWinnerIndex)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
        }
        else                                                   //white flag or scarymary=white flag
        {
            if (tempWinnerIndex == 66)
            {
                tempWinnerIndex = cardIndex;
                return true;
            }
        }
        return false;
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
        else
        {
            return COLOR.NOCOLOR;
        }
    }

    private void JudgeMustFollow(int cardIndex)
    {
        if (mustFollow == COLOR.NOCOLOR)
        {
            if (cardIndex < 13)
            {
                mustFollow = COLOR.RED;
                EventManager.Instance.broadcastMustFollow.Invoke((int)mustFollow);
                NetworkEventHandler.Instance.BroadCastMustFollow((int)mustFollow);
            }
            else if (cardIndex < 26)
            {
                mustFollow = COLOR.BLUE;
                EventManager.Instance.broadcastMustFollow.Invoke((int)mustFollow);
                NetworkEventHandler.Instance.BroadCastMustFollow((int)mustFollow);
            }
            else if (cardIndex < 39)
            {
                mustFollow = COLOR.YELLOW;
                EventManager.Instance.broadcastMustFollow.Invoke((int)mustFollow);
                NetworkEventHandler.Instance.BroadCastMustFollow((int)mustFollow);
            }
            else if (cardIndex < 52)
            {
                mustFollow = COLOR.BLACK;
                EventManager.Instance.broadcastMustFollow.Invoke((int)mustFollow);
                NetworkEventHandler.Instance.BroadCastMustFollow((int)mustFollow);
            }
        }
    }
    public int JudgeWinner()
    {
        Debug.Log("wincard:" + tempWinnerIndex);
        Debug.Log("winner:" + tempWinnerPlayerIndex);
        if (isMermaidPlayed && isSkullKingPlayed)
        {
            tempWinnerPlayerIndex = mermaidPlayer;
            bonus = 50;
        }
        else if (tempWinnerIndex == 59)
        {
            bonus = 30 * playedPirate;
        }
        EventManager.Instance.passWinner.Invoke(tempWinnerPlayerIndex);
        NetworkEventHandler.Instance.PassWinner(tempWinnerPlayerIndex);
        EventManager.Instance.bonusPoint.Invoke(bonus);
        NetworkEventHandler.Instance.BonusPoint(bonus);
        return tempWinnerPlayerIndex;
    }

    private void Initialize()
    {
        mustFollow = COLOR.NOCOLOR;
        tempWinnerIndex = 66;
        isScaryMaryPirate = false;
        isMermaidPlayed = false;
        isSkullKingPlayed = false;
        mermaidPlayer = 0;
        playedPirate = 0;
        bonus = 0;
    }
    private void ResisterScaryMary(bool isPirate)
    {
        NetworkDebugger.Instance.AddDebugText("wincal isPIrate:" + isPirate);
        isScaryMaryPirate = isPirate;
        EventManager.Instance.responseToScaryMary.Invoke();
        NetworkEventHandler.Instance.ResponseToScaryMary();
    }
}
