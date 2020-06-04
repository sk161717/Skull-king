using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    Text text;
    public int playerID;
    public string name;

    private void Start()
    {
        EventManager.Instance.sendName.AddListener(ResisterName);
    }

    public void Activate()
    {
        text = GetComponent<Text>();
        text.text = name;
    }

    void ResisterName(string name_, int id)
    {
        if (id == playerID)
        {
            name = name_;
        }
        Activate();

        NetworkDebugger.Instance.AddDebugText(name);
    }
}
