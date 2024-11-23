using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlaskUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI flaskNo;
    [SerializeField] Sprite healthFlask;
    [SerializeField] Sprite magicFlask;
    [SerializeField] Sprite emptyHealthFlask;
    [SerializeField] Sprite emptyMagicFlask;
    [SerializeField] Image flaskImage;

    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        flaskNo.text = playerController.remainingRedFlasks.ToString();

    }


    public void switchFlask(bool redFlask)
    {
        if (redFlask)
        {
            FlaskNumber(redFlask);
            if(playerController.remainingRedFlasks > 0)
            {
                flaskImage.sprite = healthFlask;
            }
            else
            {
                flaskImage.sprite = emptyHealthFlask;
            }
        }
        else if (!redFlask)
        {
            FlaskNumber(redFlask);
            if (playerController.remainingBlueFlasks > 0)
            {
                flaskImage.sprite = magicFlask;
            }
            else
            {
                flaskImage.sprite = emptyMagicFlask;
            }
        }
    }

    public void FlaskNumber(bool redFlask)
    {
        if (redFlask)
        {
            flaskNo.text = playerController.remainingRedFlasks.ToString();
            if (playerController.remainingRedFlasks == 0)
            {
                flaskImage.sprite = emptyHealthFlask;
            }
        }
        else
        {
            flaskNo.text = playerController.remainingBlueFlasks.ToString();
            if (playerController.remainingBlueFlasks == 0)
            {
                flaskImage.sprite = emptyMagicFlask;
            }
        }
    }

}
