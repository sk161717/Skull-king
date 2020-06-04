using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    [SerializeField] GameObject configPanel;
    [SerializeField] GameObject normalPanel;

    public void OnClick()
    {
        configPanel.SetActive(true);
        normalPanel.SetActive(false);       
    }
}
