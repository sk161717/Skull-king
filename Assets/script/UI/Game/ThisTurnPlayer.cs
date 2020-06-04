using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThisTurnPlayer : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        EventManager.Instance.alertThisTurnPlayer.AddListener(ShowThisTurnPLayer);
    }

    void ShowThisTurnPLayer(int player)
    {
        text.text = "This Turn:"+MagicNumberKeeper.Instance.nameList[player];
    }
}
