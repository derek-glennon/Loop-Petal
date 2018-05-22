using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //The following was done with help from the 2D Platformer Demo


    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public Transform checkpoint;
    [HideInInspector]
    public bool alive = true;

    public float speed = 1.0f;
    public float jumpForce = 1000f;


    private Transform groundCheck;
    private bool onGround = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private Animator mouthAnim;
    private bool checkpointSet = false;
    private Animator currentCheckpoint;

    // Use this for initialization
    void Awake () {

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        mouthAnim = GameObject.Find("Mouth").GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && onGround)
            jump = true;

    }

    private void FixedUpdate()
    {
        //Get Horizontal Input
        float horizontal = Input.GetAxis("Horizontal");

        //The animation will change depending on Input, rather than speed also set isMoving
        if (horizontal != 0)
        {
            isMoving = true;
            anim.SetBool("IsMoving", true);
        }
        else if (horizontal == 0 && anim.GetBool("IsMoving") == true)
        {
            isMoving = false;
            anim.SetBool("IsMoving", false);
        }
            

        //If there is input, move the player
        if(horizontal != 0)
            GetComponent<Transform>().Translate(new Vector3(horizontal * speed, 0.0f, 0.0f) * Time.deltaTime);

        //If 5 is pressed play beat blue
        if (Input.GetKeyDown("[5]") == true)
        {
            if (isMoving)
            {
                mouthAnim.SetTrigger("BeatBlue");
            }
            else
                anim.SetTrigger("BeatBlue");
        }


        //Flip the Player if direction of movement is changed
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        //Jump if able
        if (jump)
            Jump();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            if (!checkpointSet)
            {
                checkpointSet = true;
                checkpoint = other.transform;
                other.GetComponent<Animator>().SetTrigger("Activate");
                currentCheckpoint = other.GetComponent<Animator>();
            }
            else if (checkpointSet && other.transform != checkpoint)
            {
                checkpoint = other.transform;
                currentCheckpoint.SetTrigger("Deactivate");
                other.GetComponent<Animator>().SetTrigger("Activate");
                currentCheckpoint = other.GetComponent<Animator>();
            }

        }

        if (other.gameObject.CompareTag("Death"))
        {
            Die();
        }
    }

    private void Die()
    {
        alive = false;
        transform.position = checkpoint.position;
        alive = true;
    }



    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        rb2d.AddForce(new Vector2(0f, jumpForce));

        jump = false;
    }

    private void SetCheckpoint()
    {

    }

}
