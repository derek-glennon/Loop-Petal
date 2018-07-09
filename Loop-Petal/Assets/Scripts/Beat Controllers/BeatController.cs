using System;
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
    [HideInInspector]
    public AudioSource BeatSource;
    public GameObject beatUI;
    [HideInInspector]
    public MarkerController beatMarker;
    [HideInInspector]
    public RectTransform beatTransform;

    public AudioClip BeatClip;
    public BeatType beatType = BeatType.None;
    public bool isPlaying;
    public bool playedThisFrame = false;
    public float beatInterval = 1f;
    public float timePassed = 0f;

    [HideInInspector]
    public float startTime, nextTime;

    private float timePassedOffBeat;
    

    // Use this for initialization
    void Start () {
        BeatSource = this.GetComponent<AudioSource>();
        BeatSource.clip = BeatClip;
        beatMarker = beatUI.GetComponent<MarkerController>();
        beatTransform = beatUI.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update () {
        if (isPlaying)
        {
            timePassed += Time.deltaTime;
        }
        //if beat interval is hit
        if (isPlaying && Time.time >= nextTime)
        {
            PlayBeat();
            playedThisFrame = true;
        } else
        {
            playedThisFrame = false;
        }
	}

    protected virtual void PlayBeat()
    {
        timePassed = 0.0f;
        float currentTime = (float)Math.Round(Time.time * 2.0f) / 2.0f;
        nextTime = currentTime + beatInterval;
        BeatSource.Play();
        beatMarker.Play();
        ActivateObstacles();
    }

    public virtual void SetPlaying(float offset=0f)
    {
        BeatSource.Stop();
        isPlaying = true;


        //set next timestamp beat will play
        nextTime = (Time.time + (beatInterval - (Time.time % beatInterval))) + offset;

        //set time passed
        timePassed = beatInterval - (nextTime - Time.time);


        //Set the Marker Position
        if ((nextTime / 0.5f) % 2 != 0)
            beatTransform.anchoredPosition = new Vector2(-beatMarker.startOffsetPosition, beatTransform.anchoredPosition.y);
        else
            beatTransform.anchoredPosition = new Vector2(beatMarker.startOffsetPosition, beatTransform.anchoredPosition.y);


    }

    public void StopPlaying()
    {
        isPlaying = false;

        timePassed = 0.0f;

        DeactivateObstacles();

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
