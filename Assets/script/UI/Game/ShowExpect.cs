using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowExpect : MonoBehaviour
{
    public GameObject yohohoUI;
    Text UItext;
    public int expectation;
    // Start is called before the first frame update
    void Start()
    {
        UItext = this.transform.FindChild("Text").GetComponent<Text>();
        expectation = 0;
        UItext.text = "予想: " + expectation.ToString();
        EventManager.Instance.responseToyo_Ho_Ho.AddListener(ResisterYohoho);
        EventManager.Instance.cycleStart.AddListener(ShowYohoho);
    }

    private void ResisterYohoho(int player_, int expectation_)
    {
        int player = yohohoUI.GetComponent<YohohoUI>().Player;
        if (player_ == player)
        {
            expectation = expectation_;
        }
        
    }

    private void ShowYohoho()
    {
        UItext.text = "予想: " + expectation.ToString();
    }
}
