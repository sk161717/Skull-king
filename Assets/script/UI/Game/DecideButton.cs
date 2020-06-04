using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideButton : MonoBehaviour
{
    public YohohoUI yohohoUI;
    public void OnClick()
    {
        yohohoUI.ResponseToYohoho();
    }

    public void Confirm()
    {
        yohohoUI.ConfirmDecideButtonInstantiate();
    }
}
