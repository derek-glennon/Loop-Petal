﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeatType
{
    None = 0,
    Blue = 1,
    Orange = 2,
    Green = 3
}

public class BeatController : MonoBehaviour {
    private AudioSource BeatSource;
    public AudioClip BeatClip;
    public BeatType beatType = BeatType.None;
    public bool isPlaying;
    public bool playedThisFrame = false;
    public float beatInterval = 1f;
    public float timePassed = 0f;
    public bool oneBeat;
    private float startTime;
    private float nextTime;

    private float nextOffBeat;
    private float timePassedOffBeat;
    public GameObject beatUI;
    private MarkerController beatMarker;
    

    // Use this for initialization
    void Start () {
        BeatSource = this.GetComponent<AudioSource>();
        BeatSource.clip = BeatClip;
        beatMarker = beatUI.GetComponent<MarkerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            timePassed += Time.deltaTime;

        }

        if (isPlaying && Time.time >= nextTime)//timePassed >= beatInterval)
        {
            //Debug.Log(timePassed);
            timePassed = 0.0f;
            float currentTime = (float)Math.Round(Time.time * 2.0f) / 2.0f;
            nextTime = currentTime + beatInterval;
            if (oneBeat)
                BeatSource.Play();
            ActivateObstacles();
            beatMarker.Play();
            playedThisFrame = true;
        } else
        {
            playedThisFrame = false;
        }
	}

    public void SetPlaying()
    {
        isPlaying = true;

        //round current time to nearest half second
        startTime = (float)Math.Round(Time.time*2.0f)/2.0f;

        nextTime = Time.time < startTime ? startTime : startTime + beatInterval;

        //nextOffBeat = nextTime + 0.5f;

        //set time passed 
        timePassed = Time.time > startTime ? Time.time - startTime : beatInterval - (startTime - Time.time);

        if (!oneBeat)
            BeatSource.Play();
    }

    public void StopPlaying()
    {
        isPlaying = false;

        startTime = 0.0f;

        timePassed = 0.0f;

        DeactivateObstacles();

        if (!oneBeat)
            BeatSource.Stop();
    }

    protected virtual void ActivateObstacles() {
        //do things on the beat here
        Debug.Log("parent beat controller");
    }

    //Use to turn things off if that is needed for the platform
    protected virtual void DeactivateObstacles()
    {
        //Debug.Log("parent beat controller");

    }

    public BeatType JustPlayed ()
    {
        //return the beat color if it was played this frame
        if (playedThisFrame)
        {
            return beatType;
        }
        else
        {
            return BeatType.None;
        }
    }
}
