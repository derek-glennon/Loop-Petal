using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBeatController : BeatController {
    private GameObject[] GreenPlatforms;

    void Awake()
    {
        GreenPlatforms= GameObject.FindGameObjectsWithTag("GreenPlatform");
    }

    protected override void ActivateObstacles()
    {
        foreach (GameObject greenPlatform in GreenPlatforms)
        {
            greenPlatform.GetComponent<GreenPlatform>().Activate();
        }
    }
}
