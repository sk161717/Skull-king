using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public NetworkManager networkManager;

    public void OnClick()
    {
        networkManager.Launch();
    }
}
