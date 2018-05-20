using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //The following was done with help from the 2D Platformer Demo


    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;


    public float moveForce = 365f;
    public float maxSpeed = 5f;

    public float jumpForce = 1000f;

    private Transform groundCheck;
    private bool onGround = false;
    private Animator anim;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake () {

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
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

        //Set the animator condition speed so it knows to change animation to running
        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        //If speed is less than max, set it
        if (horizontal * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * horizontal * moveForce);
            

        //If speed is greater than max, limit it to the max
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);


        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        if (jump)
            Jump();


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

}
