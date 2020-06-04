using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CardNotification : UnityEvent<int, int> { }
[System.Serializable]
public class AlertThisTurnPlayer : UnityEvent<int> { }
public class ResponseToyo_ho_ho : UnityEvent<int, int> { }
public class PublishCardTransform : UnityEvent<Transform, int, int> { }  //to alert transform and judge whether trashed
public class BroadcastMustFollow : UnityEvent<int> { }
public class PublishUnPlayableCard : UnityEvent<int> { }
public class PassCardData : UnityEvent<int, int> { }
public class PassWinner : UnityEvent<int> { }
public class RoundStart : UnityEvent<int> { }
public class SendScaryMaryChoice : UnityEvent<bool> { }
public class BonusPoint : UnityEvent<int> { }
public class SendName : UnityEvent<string,int> { }


public class EventManager : SingletonMonoBehaviour<EventManager>
{

    public SendScaryMaryChoice sendScaryMaryChoice = new SendScaryMaryChoice();
    public UnityEvent InvokeScaryMary = new UnityEvent();
    public UnityEvent responseToScaryMary = new UnityEvent();
    public UnityEvent yo_ho_ho = new UnityEvent();
    public UnityEvent construct = new UnityEvent();
    public RoundStart roundStart = new RoundStart();
    public UnityEvent cycleStart = new UnityEvent();
    public UnityEvent roundEnd = new UnityEvent();
    public UnityEvent responseToConstruct = new UnityEvent();
    public ResponseToyo_ho_ho responseToyo_Ho_Ho = new ResponseToyo_ho_ho();
    public CardNotification cardNotification = new CardNotification();
    public AlertThisTurnPlayer alertThisTurnPlayer = new AlertThisTurnPlayer();
    public PublishCardTransform publishCardTransform = new PublishCardTransform();
    public BroadcastMustFollow broadcastMustFollow = new BroadcastMustFollow();

    public PassCardData passCardData = new PassCardData();
    public PublishUnPlayableCard publishUnPlayableCard = new PublishUnPlayableCard();
    public PassWinner passWinner = new PassWinner();
    public BonusPoint bonusPoint = new BonusPoint();
    public SendName sendName = new SendName();
}
