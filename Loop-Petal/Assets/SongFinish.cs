using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SongFinish : MonoBehaviour {

    public AudioMixer masterMixer;

    private AudioSource audioSource;


	// Use this for initialization
	void Awake () {

        audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            masterMixer.SetFloat("backgroundVolume", -80f);
            masterMixer.SetFloat("gameplayNotesVolume", -80f);
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            masterMixer.ClearFloat("backgroundVolume");
            masterMixer.ClearFloat("gameplayNotesVolume");
        }

    }
}
