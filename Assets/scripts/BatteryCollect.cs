using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryCollect : MonoBehaviour
{
    public TextMeshProUGUI batteryText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject.Find("Robot").GetComponent<PlayerController>().batteries++;
            RefreshText();
        }

    }

    public void RefreshText()
    {
        batteryText.text = GameObject.Find("Robot").GetComponent<PlayerController>().batteries.ToString();
    }
}
