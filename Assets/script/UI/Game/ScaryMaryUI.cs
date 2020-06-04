using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryMaryUI : MonoBehaviour
{
    [SerializeField]
    GameObject PirateButton = default;
    [SerializeField]
    GameObject WhiteFlagButton = default;
    GameObject canvas;
    GameObject pirateButton;
    GameObject whiteFlagButton;
    MagicNumberKeeper magicNumberKeeper;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        magicNumberKeeper = GameObject.Find("MagicNumberKeeper").GetComponent<MagicNumberKeeper>();

        pirateButton = Instantiate(PirateButton);
        pirateButton.transform.parent = canvas.transform;
        ScaryMaryButton pirateButton_ = pirateButton.GetComponent<ScaryMaryButton>();
        RectTransform rectTransformPirate = pirateButton_.GetComponent<RectTransform>();
        rectTransformPirate.localPosition = magicNumberKeeper.ScaryMaryPos(true);
        pirateButton_.isPirate = true;
        pirateButton_.scaryMaryUI = this;

        whiteFlagButton = Instantiate(WhiteFlagButton);
        whiteFlagButton.transform.parent = canvas.transform;
        ScaryMaryButton whiteFlagButton_ = whiteFlagButton.GetComponent<ScaryMaryButton>();
        RectTransform rectTransformWhiteFlag = whiteFlagButton_.GetComponent<RectTransform>();
        rectTransformWhiteFlag.localPosition = magicNumberKeeper.ScaryMaryPos(false);
        whiteFlagButton_.isPirate = false;
        whiteFlagButton_.scaryMaryUI = this;

        pirateButton.SetActive(false);
        whiteFlagButton.SetActive(false);
        EventManager.Instance.InvokeScaryMary.AddListener(ActivateButton);
        EventManager.Instance.sendScaryMaryChoice.AddListener(DeactivateButton);
    }

    private void ActivateButton()
    {
        pirateButton.SetActive(true);
        whiteFlagButton.SetActive(true);
    }

    public void SendChoice(bool isPirate)
    {
        if (MagicNumberKeeper.Instance.isSolo)
        {
            EventManager.Instance.sendScaryMaryChoice.Invoke(isPirate);
        }
        else
        {
            if (PhotonNetwork.player.ID == 1)
            {
                EventManager.Instance.sendScaryMaryChoice.Invoke(isPirate);
            }
            else
            {
                NetworkDebugger.Instance.AddDebugText("scaUI isPIrate:" + isPirate);
                EventManager.Instance.sendScaryMaryChoice.Invoke(isPirate);
                NetworkEventHandler.Instance.SendScaryMary(isPirate);
            }
        }
    }

    private void DeactivateButton(bool isPirate)
    {
        pirateButton.SetActive(false);
        whiteFlagButton.SetActive(false);
    }
}
