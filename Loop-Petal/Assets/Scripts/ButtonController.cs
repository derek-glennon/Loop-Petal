using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private Animator anim;

    private PlayerController player;

    private bool pressed;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        pressed = false;
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {


		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Pressed");
        }
    }


}
