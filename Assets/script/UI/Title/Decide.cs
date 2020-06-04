using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decide : MonoBehaviour
{
   public void OnClick()
    {
        TitleEventManager.Instance.decideName.Invoke();
    }
}
