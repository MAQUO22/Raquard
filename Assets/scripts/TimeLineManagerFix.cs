using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManagerFix : MonoBehaviour
{
    bool fix = false;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerController;
    public PlayableDirector director;
    void Start()
    {
        playerController = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerController;
        }
    }
}
