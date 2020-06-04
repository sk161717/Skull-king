using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwayCardUI : MonoBehaviour
{
    public int player;
    private bool isScaryMarycalled;
    [SerializeField] GameObject card = default;
    GameObject cardCopy;

    void Start()
    {
        EventManager.Instance.publishCardTransform.AddListener(JudgeDistance);
        EventManager.Instance.responseToScaryMary.AddListener(ListenScaryMary);
        EventManager.Instance.cardNotification.AddListener(RenderingTrashedCard);
        EventManager.Instance.cycleStart.AddListener(DeleteCard);
        isScaryMarycalled = false;
    }

    private void DeleteCard()
    {
        Destroy(cardCopy);
    }

    private void RenderingTrashedCard(int cardIndex, int player_)
    {
        if (MagicNumberKeeper.Instance.isSolo)
        {
            if (player_ == player && player_ != 1)
            {
                cardCopy = Instantiate(card, this.transform.position, card.transform.rotation);
                CardModel cardModel = cardCopy.GetComponent<CardModel>();
                cardModel.cardIndex = cardIndex;
                cardModel.Player = player_;
                cardModel.isTrashed = true;
                cardModel.ToggleFace(true);
            }
        }
        else
        {
            if (player_ == player && player_ != PhotonNetwork.player.ID)
            {
                cardCopy = Instantiate(card, this.transform.position, card.transform.rotation);
                CardModel cardModel = cardCopy.GetComponent<CardModel>();
                cardModel.cardIndex = cardIndex;
                cardModel.Player = player_;
                cardModel.isTrashed = true;
                cardModel.ToggleFace(true);
            }
        }       
    }


    private void JudgeDistance(Transform cardTransform, int cardIndex, int playerOfTrashedCard)
    {
        float distance = Vector3.Distance(cardTransform.position, this.transform.position);
        if (distance < 0.5 && playerOfTrashedCard == player)
        {
            if (cardIndex != 60)
            {
                EventManager.Instance.cardNotification.Invoke(cardIndex, playerOfTrashedCard);
                NetworkEventHandler.Instance.CardNotification(cardIndex, playerOfTrashedCard);
            }
            else
            {
                isScaryMarycalled = true;
                EventManager.Instance.InvokeScaryMary.Invoke();
            }
        }
    }

    private void ListenScaryMary()
    {
        if (isScaryMarycalled)
        {
            EventManager.Instance.cardNotification.Invoke(60, player);
            NetworkEventHandler.Instance.CardNotification(60, player);
            isScaryMarycalled = false;
        }
    }
}
