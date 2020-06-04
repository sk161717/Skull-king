using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NameField : MonoBehaviour
{
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        TitleEventManager.Instance.decideName.AddListener(ResisterName); ;
        InitInputField();
    }


　　　void InitInputField()
    {
        // 値をリセット
        inputField.text = "";
        // フォーカス
        inputField.ActivateInputField();
    }

    void ResisterName()
    {
        string inputValue = inputField.text;
        PlayerPrefs.SetString("name", inputValue);
        PlayerPrefs.Save();
        InitInputField();
    }
}
