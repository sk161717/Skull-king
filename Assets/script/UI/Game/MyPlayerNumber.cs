using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerNumber : MonoBehaviour
{
    Text UItext;
    // Start is called before the first frame update
    void Start()
    {
        if (MagicNumberKeeper.Instance.isSolo)
        {
            Destroy(transform.parent.gameObject);
        }
        UItext = GetComponent<Text>();
        UItext.text= "you:"+MagicNumberKeeper.Instance.nameList[PhotonNetwork.player.ID];
    }
}
