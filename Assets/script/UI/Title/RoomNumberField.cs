using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNumberField : MonoBehaviour
{
    InputField inputField;
    Image image;
    NetworkManager networkManager;
    [SerializeField] GameObject placeHolder=default;
    [SerializeField] GameObject textBox=default;
    [SerializeField] GameObject makeButton=default;
    [SerializeField] GameObject joinButton=default;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        inputField = GetComponent<InputField>();
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        TitleEventManager.Instance.joinLobby.AddListener(ActivateUI);
        InitInputField();
        DeactivateUI();
    }

    void DeactivateUI()
    {
        placeHolder.SetActive(false);
        textBox.SetActive(false);
        makeButton.SetActive(false);
        joinButton.SetActive(false);
        image.enabled = false;
    }

    void ActivateUI()
    {
        placeHolder.SetActive(true);
        textBox.SetActive(true);
        makeButton.SetActive(true);
        joinButton.SetActive(true);
        image.enabled = true;
    }

    public void MakeRoom()
    {
        string inputValue = inputField.text;
        if (JudgeValidty(inputValue))
        {
            networkManager.Host(inputValue);
        }
        else
        {
            Debug.Log("invalid roomnumber");
        }
        InitInputField();
    }

    public void JoinRoom()
    {
        string inputValue = inputField.text;
        if (JudgeValidty(inputValue))
        {
            networkManager.Guest(inputValue);
        }
        else
        {
            Debug.Log("invalid roomnumber");
        }
        InitInputField();
    }

    void InitInputField()
    {
        // 値をリセット
        inputField.text = "";
        // フォーカス
        inputField.ActivateInputField();
    }

    bool JudgeValidty(string number)
    {
        bool result = System.Text.RegularExpressions.Regex.IsMatch(number, "^[0-9]{4}$");
        return result;
    }
}
