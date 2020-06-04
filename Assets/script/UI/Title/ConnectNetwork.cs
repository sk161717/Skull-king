using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectNetwork : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager=default;
    public void OnClick()
    {
        networkManager.ConnectNetwork();
    }
}
