using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassController : MonoBehaviour {

    public BeatController startBeat;

    public bool isPlaying = false;

    private AudioSource audioSource;

	// Use this for initialization
	void Awake() {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (startBeat.firstNotePlayed && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }

    }
}
