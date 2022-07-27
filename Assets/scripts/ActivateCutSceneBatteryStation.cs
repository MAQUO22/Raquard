using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateCutSceneBatteryStation : MonoBehaviour
{
    public PlayableDirector director;
    private PlayerController playerController;
    private ActivateBatteryStation batteryStation;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        playerController = GameObject.Find("Robot").GetComponent<PlayerController>();
        batteryStation = GameObject.Find("BatteryStation").GetComponent<ActivateBatteryStation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((batteryStation.active == false) && (playerController.batteries >= 5))
        {
            if (collision.gameObject.tag == "Player")
            {
                playerController.alive = false;
                director.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.alive = true;
    }
}
