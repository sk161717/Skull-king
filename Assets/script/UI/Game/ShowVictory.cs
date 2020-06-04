using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowVictory : MonoBehaviour
{
    public GameObject yohohoUI;
    Text UItext;
    public int victory;
    public int bonus;
    int winnerPlayer;
    int player;
    int bonusStack;

    // Start is called before the first frame update
    void Start()
    {
        UItext = this.transform.FindChild("Text").GetComponent<Text>();
        victory = 0;
        player= yohohoUI.GetComponent<YohohoUI>().Player;
        EventManager.Instance.passWinner.AddListener(ResisterWin);
        EventManager.Instance.cycleStart.AddListener(Initialize);
        EventManager.Instance.roundStart.AddListener(InitializeAtRound);
        EventManager.Instance.bonusPoint.AddListener(ResisterBonus);
        UItext.text = "勝利: " + victory.ToString();
    }
    private void InitializeAtRound(int numOfCard)
    {
        victory = 0;
        bonus = 0;
        UItext.text = "勝利: " + victory.ToString();
    }

    private void ResisterWin(int player_)
    {
        winnerPlayer = player_;
        if (player_ ==player)
        {
            victory++;
            UItext.text = "勝利: " + victory.ToString();
        }
        if (bonusStack != 0)
        {
            if (player_ == player)
            {
                bonus += bonusStack;
            }
        }
    }

    private void ResisterBonus(int bonus_)
    {
        if (winnerPlayer != -1)
        {
            if (winnerPlayer == player)
            {
                bonus += bonus_;
            }
        }
        else
        {
            bonusStack = bonus_;
        }
    }

    private void Initialize()
    {
        bonusStack = 0;
        winnerPlayer = -1;
    }
}
