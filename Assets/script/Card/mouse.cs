using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{


    //マウスでドラッグするスクリプト

    private Vector3 screenPoint;
    private Vector3 offset;
    private int player;
    private int cardIndex;
    private int thisTurnPlayer;
    private bool playable;

    void Start()
    {
        player = this.GetComponent<CardModel>().Player;
        cardIndex = this.GetComponent<CardModel>().cardIndex;
        EventManager.Instance.alertThisTurnPlayer.AddListener(ResisterThisTurnPlayer);
        EventManager.Instance.publishUnPlayableCard.AddListener(ResisterUnplayable);
        EventManager.Instance.cardNotification.AddListener(ResisterDelete);
        EventManager.Instance.cycleStart.AddListener(InitializeAtCycle);
    }

    void OnMouseDown()
    {
        if (thisTurnPlayer == player && playable)
        {
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (thisTurnPlayer == player && playable)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            transform.position = currentPosition;
        }
    }

    void OnMouseUp()
    {
        if (thisTurnPlayer == player && playable)
        {
            CardModel cardModel = GetComponent<CardModel>();
            int cardIndex_ = cardModel.cardIndex;
            int player = cardModel.Player;
            EventManager.Instance.publishCardTransform.Invoke(this.transform, cardIndex_, player);
        }
    }

    void ResisterThisTurnPlayer(int noticedPlayer)
    {
        thisTurnPlayer = noticedPlayer;
    }

    void ResisterUnplayable(int cardIndex_)
    {
        if (cardIndex_ == cardIndex)
        {
            playable = false;
        }
    }

    void ResisterDelete(int cardIndex_, int player)
    {
        if (cardIndex_ == cardIndex)
        {
            playable = false;
        }
    }
    
    void InitializeAtCycle()
    {
        playable = true;
    }
}
