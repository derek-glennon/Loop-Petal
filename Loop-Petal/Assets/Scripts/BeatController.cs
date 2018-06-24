using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour {
    private AudioSource BeatSource;
    public AudioClip BeatClip;
    public bool isPlaying;
    public float timeInterval = 1f;
    private float timePassed = 0f;
    private float startTime;

    // Use this for initialization
    void Start () {
        BeatSource = this.GetComponent<AudioSource>();
        BeatSource.clip = BeatClip;
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (isPlaying && timePassed >= timeInterval)
        {
            timePassed = 0f;
            BeatSource.Play();
            ActivateObstacles();
        }
	}

    public void SetPlaying()
    {
        isPlaying = true;

        //round current time to nearest half second
        startTime = (float)Math.Round(Time.time*2)/2;

        //set time passed 
        timePassed = Time.time > startTime ? Time.time - startTime : timeInterval - (startTime - Time.time);
    }

    protected virtual void ActivateObstacles() {
        //do things on the beat here
        Debug.Log("parent");
    }
}
