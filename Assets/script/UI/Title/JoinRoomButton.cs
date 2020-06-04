using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomButton : MonoBehaviour
{

    RoomNumberField roomNumberField;
    // Start is called before the first frame update
    void Start()
    {
        roomNumberField = transform.parent.gameObject.GetComponent<RoomNumberField>();
    }

    public void OnClick()
    {
        roomNumberField.JoinRoom();
    }
}
