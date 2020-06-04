using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public bool isPlus;
    // Start is called before the first frame update
    public void Confirm()
    {
        if (isPlus)
        {
            transform.parent.gameObject.GetComponent<YohohoConsole>().ConfirmPlusInstantiate();
        }
        else
        {
            transform.parent.gameObject.GetComponent<YohohoConsole>().ConfirmMinusInstantiate();
        }

    }

    public void OnClick()
    {
        if (isPlus)
        {
            int count_ = transform.parent.gameObject.GetComponent<YohohoConsole>().count++;
            transform.parent.gameObject.GetComponent<YohohoConsole>().UItext.text = count_.ToString();

        }
        else
        {
            int count_ = transform.parent.gameObject.GetComponent<YohohoConsole>().count--;
            transform.parent.gameObject.GetComponent<YohohoConsole>().UItext.text = count_.ToString();
        }

    }
}
