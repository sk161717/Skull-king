using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = PlayerPrefs.GetString("name");
        TitleEventManager.Instance.decideName.AddListener(UpdateName);
    }

    void UpdateName()
    {
        text.text = PlayerPrefs.GetString("name");
    }
}
