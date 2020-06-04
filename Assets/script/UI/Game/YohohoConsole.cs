using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YohohoConsole : MonoBehaviour
{
    [SerializeField]
    GameObject plus_ = default;
    [SerializeField]
    GameObject minus_ = default;
    public int count;
    public Text UItext;
    Vector3 plusPos;
    Vector3 minusPos;
    private bool plusFlag = false;
    private bool minusFlag = false;
    public YohohoUI yohohoUI;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SetPos();

        GameObject plus = Instantiate(plus_);
        plus.transform.parent = this.transform;
        RectTransform rectTransformplus = plus.GetComponent<RectTransform>();
        rectTransformplus.localPosition = plusPos;


        GameObject minus = Instantiate(minus_);
        minus.transform.parent = this.transform;
        RectTransform rectTransformminus = minus.GetComponent<RectTransform>();
        rectTransformminus.localPosition = minusPos;


        plus.GetComponent<Counter>().isPlus = true;
        plus.GetComponent<Counter>().Confirm();
        minus.GetComponent<Counter>().isPlus = false;
        minus.GetComponent<Counter>().Confirm();
        UItext = this.transform.FindChild("Text").GetComponent<Text>();
    }

    void Update()
    {
        UItext.text = count.ToString();
    }

    void SetPos()
    {
        Vector3 plusPos_ = new Vector3(0, 0, 0);
        Vector3 minusPos_ = new Vector3(0, 0, 0);
        plusPos_.x = plusPos_.x + 100f;
        minusPos_.x = minusPos_.x - 100f;
        plusPos = plusPos_;
        minusPos = minusPos_;
    }

    public void ConfirmPlusInstantiate()
    {
        NetworkDebugger.Instance.AddDebugText("plus confirm ");
        plusFlag = true;
        callParentFunc();
    }

    public void ConfirmMinusInstantiate()
    {
        NetworkDebugger.Instance.AddDebugText("minus confirm ");
        minusFlag = true;
        callParentFunc();
    }

    void callParentFunc()
    {
        if (plusFlag && minusFlag)
        {
            yohohoUI.ConfirmYohohoConsoleInstantiate();
        }
    }
}
