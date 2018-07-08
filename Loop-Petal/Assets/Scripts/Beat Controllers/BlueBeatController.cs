using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBeatController : BeatController {
    private GameObject[] BlueSpawns;

    void Awake()
    {
        BlueSpawns = GameObject.FindGameObjectsWithTag("BlueSpawn");
    }

    protected override void ActivateObstacles()
    {
        foreach (GameObject blueSpawn in BlueSpawns)
        {
            blueSpawn.GetComponent<BlueSpawn>().Spawn();
        }
    }

    protected override void DeactivateObstacles()
    {
        foreach (GameObject blueSpawn in BlueSpawns)
        {
            blueSpawn.GetComponent<BlueSpawn>().Deactivate();
        }
    }
}
