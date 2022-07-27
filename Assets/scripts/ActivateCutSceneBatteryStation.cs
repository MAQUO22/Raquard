using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateCutSceneBatteryStation : MonoBehaviour
{
    public PlayableDirector director;
    private PlayerController playerController;
    private ActivateBatteryStation batteryStation;
    public bool cutScenePlayed = false;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        playerController = GameObject.Find("Robot").GetComponent<PlayerController>();
        batteryStation = GameObject.Find("BatteryStation").GetComponent<ActivateBatteryStation>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D hitPos in collision.contacts)
        {

            if (hitPos.normal.x > 0 && cutScenePlayed == false)
            {
                if ((batteryStation.active == false) && (playerController.batteries >= 5))
                {
                    if (collision.gameObject.tag == "Player")
                    {
                        //playerController.alive = false;
                        director.Play();
                        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                        cutScenePlayed = true;
                    }
                }
            }
            else if((hitPos.normal.x > 0) && cutScenePlayed)
            {
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(cutScenePlayed == false)
    //    {
    //       gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    playerController.alive = true;
    //}
}
