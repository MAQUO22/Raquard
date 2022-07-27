using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBatteryStation : MonoBehaviour
{
    public bool active = false;
    private PlayerController playerController;
    private BatteryCollect batteryCollect;

    void Start()
    {
        playerController = GameObject.Find("Robot").GetComponent<PlayerController>();
        batteryCollect = GameObject.Find("Battery").GetComponent<BatteryCollect>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(active == false && (GameObject.Find("BatteryStationTrigger").GetComponent<ActivateCutSceneBatteryStation>().cutScenePlayed == true))
            {
                if ((GameObject.Find("Robot").GetComponent<PlayerController>().batteries) >= 5)
                {
                    active = true;
                    playerController.batteries -= 5;
                    playerController.savePoint = gameObject.transform.position;
                    batteryCollect.batteryText.text = playerController.batteries.ToString();
                    batteryCollect.RefreshText();
                }
            }
            else if(active == true)
            {
                Debug.Log("Station already activated");
            }
        }
    }

   

}
