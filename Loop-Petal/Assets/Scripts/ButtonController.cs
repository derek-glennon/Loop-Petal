using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    private Animator animator;
    public GameObject beatObject;
    private BeatController beatController;
    private bool isPressed = false;

	// Use this for initialization
	void Start () {
        beatController = beatObject.GetComponent<BeatController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Pressed");
            isPressed = true;
            beatController.SetPlaying();
        }
    }


}
