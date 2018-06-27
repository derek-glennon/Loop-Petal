using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBeatController : BeatController {
    private GameObject[] BluePlatforms;

    void Awake()
    {
        BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
    }

    protected override void ActivateObstacles()
    {
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            bluePlatform.GetComponent<BluePlatform>().Activate();
        }
    }

    protected override void DeactivateObstacles()
    {
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            bluePlatform.GetComponent<BluePlatform>().Deactivate();
        }
    }
}
