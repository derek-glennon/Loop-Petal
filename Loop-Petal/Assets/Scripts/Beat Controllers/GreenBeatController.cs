using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBeatController : BeatController {
    private GameObject[] GreenPlatforms;
    public int intervalsPerBeat = 2;
    private int counter = 0;

    void Awake()
    {
        GreenPlatforms = GameObject.FindGameObjectsWithTag("GreenPlatform");
        counter = intervalsPerBeat;
    }

    protected override void PlayBeat()
    {
        timePassed = 0.0f;
        float currentTime = (float)Math.Round(Time.time * 2.0f) / 2.0f;
        nextTime = currentTime + beatInterval;
        if (counter == intervalsPerBeat)
        {
            counter = 0;
            BeatSource.Play();
        }
        beatMarker.Play();
        ActivateObstacles();
        counter++;
    }

    protected override void ActivateObstacles()
    {
        foreach (GameObject greenPlatform in GreenPlatforms)
        {
            greenPlatform.GetComponent<GreenPlatform>().Activate();
        }
    }

    protected override void DeactivateObstacles()
    {
        foreach (GameObject greenPlatform in GreenPlatforms)
        {
            greenPlatform.GetComponent<GreenPlatform>().Deactivate();
        }
    }
}
