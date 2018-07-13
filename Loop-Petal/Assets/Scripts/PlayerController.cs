using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
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
    [HideInInspector]
    public bool canMove = true;


    public float speed = 1.0f;
    private float jumpSpeed = 4.5f;
    private float shortJumpSpeed = 2.5f;
    [HideInInspector]
    public bool isJumping = false;

    //public Transform BlueNote;
    //public Transform OrangeNote;

    private AudioSource BeatSource;
    public AudioClip JumpAudio;
    public AudioClip BlueAudio;
    public AudioClip OrangeAudio;


    private Transform groundCheck;
    private bool onGround = false;
    private Animator anim;
    private Rigidbody2D rigidbody2d;
    //private Animator mouthAnim;
    private bool checkpointSet = false;
    private Animator currentCheckpoint;
    //private Transform emitter;

    private CircleCollider2D colliderPlayer;
    private bool jumpCancel;

    //Cheat Codes
    private List<GameObject> Checkpoints;
    List<string> cheatInputs = new List<string>(new string[] { "1", "2", "3", "4", "5", "6"});


    // Use this for initialization
    void Awake () {

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        //mouthAnim = GameObject.Find("Mouth").GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //emitter = GameObject.Find("NoteEmitter").GetComponent<Transform>();
        BeatSource = GetComponent<AudioSource>();

        colliderPlayer = GetComponent<CircleCollider2D>();

        //Cheat Codes
        Checkpoints = new List<GameObject>();
        Checkpoints.Add(GameObject.Find("Checkpoint"));
        for (int i  = 1; i < cheatInputs.Count; i++)
        {
            Checkpoints.Add(GameObject.Find("Checkpoint (" + i.ToString() + ")"));
        }

    }
	
	// Update is called once per frame
	void Update () {

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (onGround)
        {
            isJumping = false;
        }
        
        // If the jump button is pressed and the player is grounded then the player should jump.
        if ( (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) ) && onGround && canMove)
            jump = true;

        // if player is jumping and they let go of jump, then the jump momentum is less
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow)) && !onGround && isJumping)
            jumpCancel = true;

        //If player is dead and the death animation has finished, then respawn
        if (!alive && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerNoiseDeath") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            Respawn();

        //Cheats to help me test stuff out
        for (int i = 0; i < cheatInputs.Count; i++)
        {
            if (Input.GetKeyDown(cheatInputs[i]))
            {
                transform.position = Checkpoints[i].transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        //Get Horizontal Input
        float horizontal = 0.0f;
        if (canMove)
            horizontal = Input.GetAxis("Horizontal");

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
            
        if (alive)
        {
            //If there is input, move the player
            if (horizontal != 0)
                GetComponent<Transform>().Translate(new Vector3(horizontal * speed, 0.0f, 0.0f) * Time.deltaTime);

            //Flip the Player if direction of movement is changed
            if (horizontal > 0 && !facingRight)
                Flip();
            else if (horizontal < 0 && facingRight)
                Flip();

            //Jump if able
            if (jump)
                Jump();

            // cancel the jump when the button is no longer pressed
            if (jumpCancel)
            {
                CancelJump();
            }
        }

        //Death Animation
        if (!alive)
        {
            transform.Translate(new Vector3(.1f, .1f, 0.0f) * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * Time.deltaTime * 50f);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Checkpoints
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

        //Death
        if (other.gameObject.CompareTag("Death"))
        {
            Die();
        }
    }

    private void Die()
    {
        alive = false;
        anim.SetTrigger("Die");
        rigidbody2d.constraints = RigidbodyConstraints2D.None;
        rigidbody2d.gravityScale = 0.0f;
        rigidbody2d.velocity = Vector3.zero;
        colliderPlayer.isTrigger = true;
    }

    private void Respawn()
    {
        alive = true;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.identity;
        rigidbody2d.gravityScale = 1.0f;
        transform.position = checkpoint.position;
        colliderPlayer.isTrigger = false;
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
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpSpeed);

        BeatSource.clip = JumpAudio;
        BeatSource.Play();

        jump = false;
        isJumping = true;
    }

    private void CancelJump()
    {
        if (rigidbody2d.velocity.y > shortJumpSpeed)
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, shortJumpSpeed);
        jumpCancel = false;
    }

    //public void EmitBlueNote()
    //{
    //        if (isMoving)
    //        {
    //            mouthAnim.SetTrigger("BeatBlue");
    //        }
    //        else
    //            anim.SetTrigger("BeatBlue");

    //        Transform clone;
    //        clone = Instantiate(BlueNote, emitter.position, Quaternion.identity) as Transform;
    //        BeatSource.clip = BlueAudio;
    //        BeatSource.Play();

    //}


    //public void EmitOrangeNote()
    //{

    //        if (isMoving)
    //        {
    //            mouthAnim.SetTrigger("BeatBlue");
    //        }
    //        else
    //            anim.SetTrigger("BeatBlue");

    //        Transform clone;
    //        clone = Instantiate(OrangeNote, emitter.position, Quaternion.identity) as Transform;
    //        BeatSource.clip = OrangeAudio;
    //        BeatSource.Play();
    //}

}
