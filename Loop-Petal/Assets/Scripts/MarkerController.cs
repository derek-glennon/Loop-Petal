using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerController : MonoBehaviour {
    private Image image;
    private bool onScreen;
    Color color;

    public GameObject beatObject;
    private BeatController beatController;
    private ParticleSystem particleSystem;

	// Use this for initialization
	void Awake() {
        beatController = beatObject.GetComponent<BeatController>();
        particleSystem = GetComponent<ParticleSystem>();
        onScreen = false;
        image = GetComponent<Image>();
        color = image.color;
        color.a = 0.0f;
        image.color = color;
	}
	
	// Update is called once per frame
	void Update () {

        if (beatController.isPlaying && !onScreen)
        {
            onScreen = true;
        }
		
        if (onScreen)
        {
            color.a = beatController.timePassed;
            image.color = color;
        }

        if (!beatController.isPlaying && onScreen)
        {
            onScreen = false;
        }
	}

    public void Play()
    {
        this.particleSystem.Play();
    }
}
