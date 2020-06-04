using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkDebugger : SingletonMonoBehaviour<NetworkDebugger>
{
    [SerializeField]
    private Text m_eventLog = null;

    public void AddDebugText(string text)
    {
        m_eventLog.text += text + System.Environment.NewLine;
    }
}
