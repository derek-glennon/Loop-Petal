using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SongFinish : MonoBehaviour {

    public AudioMixer masterMixer;
    public float decreaseVolumerPerFrame;


    private AudioSource audioSource;
    private bool isActive;



	// Use this for initialization
	void Awake () {

        audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            DecreaseVolume("backgroundVolume", decreaseVolumerPerFrame);
            DecreaseVolume("gameplayNotesVolume", decreaseVolumerPerFrame);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            //audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = false;
            masterMixer.ClearFloat("backgroundVolume");
            masterMixer.ClearFloat("gameplayNotesVolume");
            audioSource.Stop();
        }

    }

    private void DecreaseVolume(string exposedParameter, float valuePerFrame)
    {
        float value;
        bool result = masterMixer.GetFloat(exposedParameter, out value);
        if (result)
        {
            masterMixer.SetFloat(exposedParameter, value - valuePerFrame);
        }
        else
        {
            Debug.Log("Exposed Parameter not found");
            return;
        }
    }
}
