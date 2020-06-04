using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoint : MonoBehaviour
{
    public GameObject yohohoUI;
    Text UItext;
    public int point;
    // Start is called before the first frame update
    void Start()
    {
        UItext = this.transform.FindChild("Text").GetComponent<Text>();
        point = 0;
        UItext.text = "得点: " + point.ToString();
    }

    public void ResisterPoint(int point_)
    {
        point += point_;
        UItext.text = "得点: " + point.ToString();
    }
}
