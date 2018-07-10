using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    private Animator animator;
    public GameObject beatObject;
    private BeatController beatController;
    [HideInInspector]
    public bool isActive;

    public bool isPressed = false;
    public int numberOfOptions = 1;
    public int selectedOption = -1;
    public float offset = 0.5f;

	// Use this for initialization
	void Start () {
        beatController = beatObject.GetComponent<BeatController>();
        animator = GetComponent<Animator>();
        isActive = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Used in ButtonOption
            if (!isActive)
                isActive = true;

            animator.SetTrigger("Pressed");
            isPressed = true;
            selectedOption = (selectedOption + 1) % numberOfOptions;
            beatController.SetPlaying(selectedOption * offset);
        }
    }


}
