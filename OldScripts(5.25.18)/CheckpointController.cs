using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    private Animator anim;

    private PlayerController player;

    private bool played = false;

	// Use this for initialization
	void Awake () {

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CheckpointActive") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !played)
        {
            played = true;
            GetComponent<AudioSource>().Play();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CheckpointIdle"))
            played = false;


    }
}
