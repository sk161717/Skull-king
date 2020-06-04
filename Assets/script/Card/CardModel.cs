using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    [SerializeField] Material black_m;
    [SerializeField] Material default_m;
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

    public int cardIndex;
    public bool isTrashed;
    private int thisTurnPlayer;
    private bool playable = true;

    private int player;
    public int Player
    {
        get { return player; }
        set { player = value; }
    }

    public void ToggleFace(bool showcase)
    {
        if (showcase)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.Instance.cardNotification.AddListener(ProcessTrash);
        EventManager.Instance.passWinner.AddListener(HighlightWinner);
        EventManager.Instance.cycleStart.AddListener(InitializeAtCycle);
        EventManager.Instance.alertThisTurnPlayer.AddListener(ResisterThisTurnPlayer);
        EventManager.Instance.publishUnPlayableCard.AddListener(ResisterUnplayable);
    }

    private void ProcessTrash(int index, int playerOfTrashedCard)
    {
        if (index == cardIndex)
        {
            isTrashed = true;
        }
    }

    void HighlightWinner(int winner)
    {
        if (isTrashed)
        {
            if (winner == player)
            {
                spriteRenderer.material = default_m;
            }
            else
            {
                spriteRenderer.material = black_m;
            }
        }
    }

    void InitializeAtCycle()
    {
        spriteRenderer.material = default_m;
        playable = true;
        if (isTrashed)
        {
            Destroy(this.gameObject);
        }
    }

    void TurnToBlack()
    {
        if (!playable)
        {
            spriteRenderer.material = black_m;
        }
    }

    void ResisterThisTurnPlayer(int noticedPlayer)
    {
        thisTurnPlayer = noticedPlayer;
        if (thisTurnPlayer == player)
        {
            TurnToBlack();
        }
        else
        {
            spriteRenderer.material = default_m;
        }
    }

    void ResisterUnplayable(int cardIndex_)
    {
        if (cardIndex_ == cardIndex)
        {
            playable = false;
        }
        TurnToBlack();
    }
}
