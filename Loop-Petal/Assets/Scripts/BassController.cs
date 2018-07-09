using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassController : MonoBehaviour {

    public ButtonController firstButton;

    public bool isPlaying = false;

    private AudioSource audioSource;

	// Use this for initialization
	void Awake() {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (firstButton.isPressed && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }

    }
}
