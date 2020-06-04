using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaryMaryButton : MonoBehaviour
{
    public bool isPirate;
    public ScaryMaryUI scaryMaryUI;

    public void Onclick()
    {
        scaryMaryUI.SendChoice(isPirate);
    }
}
