using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBeatController : BeatController {
    //private GameObject[] BluePlatforms;
    private GameObject[] BlueSpawns;

    void Awake()
    {
        //BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
        BlueSpawns = GameObject.FindGameObjectsWithTag("BlueSpawn");
    }

    protected override void ActivateObstacles()
    {
        //foreach (GameObject bluePlatform in BluePlatforms)
        //{
        //    bluePlatform.GetComponent<BluePlatform>().Activate();
        //}

        foreach (GameObject blueSpawn in BlueSpawns)
        {
            blueSpawn.GetComponent<BlueSpawn>().Spawn();
        }
    }

    protected override void DeactivateObstacles()
    {
        //foreach (GameObject bluePlatform in BluePlatforms)
        //{
        //    bluePlatform.GetComponent<BluePlatform>().Deactivate();
        //}

        foreach (GameObject blueSpawn in BlueSpawns)
        {
            blueSpawn.GetComponent<BlueSpawn>().Deactivate();
        }
    }
}
