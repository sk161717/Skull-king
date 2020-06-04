using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offline : MonoBehaviour
{
    [SerializeField] NetworkManager networkManager;

    public void OnClick()
    {
        networkManager.Solo();
    }
}
