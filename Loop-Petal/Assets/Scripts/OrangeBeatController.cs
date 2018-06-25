using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrangeBeatController : BeatController {
    private GameObject[] OrangePlatforms;

    void Awake()
    {
        OrangePlatforms = GameObject.FindGameObjectsWithTag("OrangePlatform");
    }

    protected override void ActivateObstacles()
    {
        foreach (GameObject orangePlatform in OrangePlatforms)
        {
            orangePlatform.GetComponent<OrangePlatform>().Activate();
        }
    }
}
