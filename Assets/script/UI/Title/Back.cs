using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    [SerializeField] GameObject configPanel;
    [SerializeField] GameObject normalPanel;

    public void OnClick()
    {
        normalPanel.SetActive(true);
        configPanel.SetActive(false);
    }
}
