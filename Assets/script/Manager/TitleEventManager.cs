using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TitleEventManager : SingletonMonoBehaviour<TitleEventManager>
{
    public UnityEvent joinLobby = new UnityEvent();
    public UnityEvent decideName = new UnityEvent();
}
