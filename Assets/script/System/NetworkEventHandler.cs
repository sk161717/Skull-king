using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType : byte
{
    CONSTRUCT, RESPONSETOCONSTRUCT, ROUNDSTART, PASSCARDDATA, YOHOHO, RESPONSETOYOHOHO,
    CYCLESTART, THROWTURN, SENDSCARYMARY, CARDNOTIFICATION,RESPONSETOSCARYMARY,
    BROADCASTMUSTFOLLOW, PASSWINNER, ROUNDEND, SETPLAYERNUM,BONUSPOINT,SENDNAME
}

public class NetworkEventHandler : SingletonMonoBehaviour<NetworkEventHandler>
{
    public bool isSolo;

    override protected void Awake()
    {
        isSolo = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().isSolo;
        if (!isSolo)
        {
            PhotonNetwork.OnEventCall += OnRaiseEvent;
        }
    }

    public void Construct()
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.CONSTRUCT, null, true, RaiseEventOptions.Default);
        }
    }

    public void ResponseToConstruct()
    {
        if (!isSolo)
        {
            var option = new RaiseEventOptions()
            {
                TargetActors = new int[] { 1 },
                Receivers = ReceiverGroup.All,
            };
            PhotonNetwork.RaiseEvent((byte)EEventType.RESPONSETOCONSTRUCT, null, true, option);
        }
    }

    public void RoundStart(int numberOfCard)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.ROUNDSTART, numberOfCard, true, RaiseEventOptions.Default);
        }
    }

    public void PassCardData(int cardIndex, int player)
    {
        if (!isSolo)
        {
            var option = new RaiseEventOptions()
            {
                TargetActors = new int[] { player },
                Receivers = ReceiverGroup.All,
            };
            PhotonNetwork.RaiseEvent((byte)EEventType.PASSCARDDATA, cardIndex, true, option);
        }
    }

    public void Yo_ho_ho()
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.YOHOHO, null, true, RaiseEventOptions.Default);
        }
    }

    public void ResponseToYohoho(int player, int expectation)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.RESPONSETOYOHOHO, expectation, true, RaiseEventOptions.Default);
        }
    }

    public void CycleStart()
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.CYCLESTART, null, true, RaiseEventOptions.Default);
        }
    }

    public void ThrowTurn(int player)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.THROWTURN, player, true, RaiseEventOptions.Default);
        }
    }

    public void CardNotification(int cardIndex, int player)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.CARDNOTIFICATION, cardIndex, true, RaiseEventOptions.Default);
        }
    }

    public void SendScaryMary(bool isPirate)
    {
        if (!isSolo)
        {
            var option = new RaiseEventOptions()
            {
                TargetActors = new int[] { 1 },
                Receivers = ReceiverGroup.All,
            };
            PhotonNetwork.RaiseEvent((byte)EEventType.SENDSCARYMARY, isPirate, true, option);
        }
    }

    public void BroadCastMustFollow(int color)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.BROADCASTMUSTFOLLOW, color, true, RaiseEventOptions.Default);
        }
    }

    public void PassWinner(int player)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.PASSWINNER, player, true, RaiseEventOptions.Default);
        }
    }

    public void RoundEnd()
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.ROUNDEND, null, true, RaiseEventOptions.Default);
        }
    }

    public void SetPlayerNum(int playerNum)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.SETPLAYERNUM, playerNum, true, RaiseEventOptions.Default);
        }
    }

    public void BonusPoint(int point)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.BONUSPOINT, point, true, RaiseEventOptions.Default);
        }
    }

    public void SendName(string name)
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.SENDNAME, name, true, RaiseEventOptions.Default);
        }
    }

    public void ResponseToScaryMary()
    {
        if (!isSolo)
        {
            PhotonNetwork.RaiseEvent((byte)EEventType.RESPONSETOSCARYMARY, null, true, RaiseEventOptions.Default);
        }
    }

    private void OnRaiseEvent(byte i_eventcode, object i_content, int i_senderid)
    {

        var eventType = (EEventType)i_eventcode;
        switch (eventType)
        {
            case EEventType.CONSTRUCT:
                NetworkDebugger.Instance.AddDebugText("receice Const");
                EventManager.Instance.construct.Invoke();
                break;
            case EEventType.RESPONSETOCONSTRUCT:
                NetworkDebugger.Instance.AddDebugText("receice resToConst");
                EventManager.Instance.responseToConstruct.Invoke();
                break;
            case EEventType.ROUNDSTART:
                NetworkDebugger.Instance.AddDebugText("receice roundstart");
                EventManager.Instance.roundStart.Invoke((int)i_content);
                break;
            case EEventType.PASSCARDDATA:
                NetworkDebugger.Instance.AddDebugText("receice passcardData");
                EventManager.Instance.passCardData.Invoke((int)i_content, PhotonNetwork.player.ID);
                break;
            case EEventType.YOHOHO:
                NetworkDebugger.Instance.AddDebugText("receice yohoho");
                EventManager.Instance.yo_ho_ho.Invoke();
                break;
            case EEventType.RESPONSETOYOHOHO:
                NetworkDebugger.Instance.AddDebugText("receice responsetToYohoho");
                EventManager.Instance.responseToyo_Ho_Ho.Invoke(i_senderid, (int)i_content);
                break;
            case EEventType.CYCLESTART:
                NetworkDebugger.Instance.AddDebugText("receice cyclestart");
                EventManager.Instance.cycleStart.Invoke();
                break;
            case EEventType.THROWTURN:
                NetworkDebugger.Instance.AddDebugText("receice throwTurn");
                EventManager.Instance.alertThisTurnPlayer.Invoke((int)i_content);
                break;
            case EEventType.CARDNOTIFICATION:
                NetworkDebugger.Instance.AddDebugText("receice cardNOtification");
                EventManager.Instance.cardNotification.Invoke((int)i_content, i_senderid);
                break;
            case EEventType.SENDSCARYMARY:
                NetworkDebugger.Instance.AddDebugText("receice SendScaryMary");
                EventManager.Instance.sendScaryMaryChoice.Invoke((bool)i_content);
                break;
            case EEventType.BROADCASTMUSTFOLLOW:
                NetworkDebugger.Instance.AddDebugText("receice BroadCastMustFollow");
                EventManager.Instance.broadcastMustFollow.Invoke((int)i_content);
                break;
            case EEventType.PASSWINNER:
                NetworkDebugger.Instance.AddDebugText("receice passwinner");
                EventManager.Instance.passWinner.Invoke((int)i_content);
                break;
            case EEventType.ROUNDEND:
                NetworkDebugger.Instance.AddDebugText("receice roundEnd");
                EventManager.Instance.roundEnd.Invoke();
                break;
            case EEventType.SETPLAYERNUM:
                MagicNumberKeeper.Instance.NumberOfPeople = (int)i_content;
                break;
            case EEventType.BONUSPOINT:
                NetworkDebugger.Instance.AddDebugText("receice bonuspoint");
                EventManager.Instance.bonusPoint.Invoke((int)i_content);
                break;
            case EEventType.SENDNAME:
                NetworkDebugger.Instance.AddDebugText("receice name");
                EventManager.Instance.sendName.Invoke((string)i_content,i_senderid);
                break;
            case EEventType.RESPONSETOSCARYMARY:
                NetworkDebugger.Instance.AddDebugText("receice response to scarymary");
                EventManager.Instance.responseToScaryMary.Invoke();
                break;
            default:
                break;
        }
    }
}
