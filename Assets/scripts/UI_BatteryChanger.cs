using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_BatteryChanger : MonoBehaviour
{
    public TMP_Text powerText;

    public Image image;
    public Sprite[] sprites;
    void UIChangePower()
    {
        switch (GameObject.Find("Robot").GetComponent<PlayerController>().power)
        {
            case 0:
                image.sprite = sprites[0];
                break;
            case 1:
                image.sprite = sprites[1];
                break;
            case 2:
                image.sprite = sprites[2];
                break;
            case 3:
                image.sprite = sprites[3];
                break;

            default:
                break;
        }
        powerText.text = GameObject.Find("Robot").GetComponent<PlayerController>().power.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIChangePower();
    }
}
