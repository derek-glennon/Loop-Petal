using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMover : MonoBehaviour {

    public float ySpeed = 0.5f;
    public float xSpeedMag = 0.5f;
    public float dragSpeed = 0.1f;

    private float xSpeed;
    private bool isRight;
    private PlayerController player;
    private Rigidbody2D playerRB;
    private Animator anim;

	// Use this for initialization
	void Start() {

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        //Get the speed in the right direction and have it match the speed of the player if they are moving
        if (player.facingRight)
        {
            xSpeed = xSpeedMag;
            if (player.isMoving)
                xSpeed += player.speed;
            isRight = true;
        }
        else if (!player.facingRight)
        {
            xSpeed = -1.0f * xSpeedMag;
            if (player.isMoving)
                xSpeed -= player.speed;
            isRight = false;
        }
            

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        //Move up
        transform.position = new Vector3(transform.position.x + xSpeed * Time.deltaTime,transform.position.y + ySpeed * Time.deltaTime, transform.position.z);

        //Make sure the correct limit is being placed on the note
        if (isRight)
        {
            xSpeed -= dragSpeed;
            if (xSpeed < 0.0f)
                xSpeed = 0.0f;
        }
        else if (!isRight)
        {
            xSpeed += dragSpeed;
            if (xSpeed > 0.0f)
                xSpeed = 0.0f;
        }
            



        //Destroy after one animation
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlueNote") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            Destroy(gameObject);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("OrangeNote") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            Destroy(gameObject);





    }


}
