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
    public bool blueActive = false;
    [HideInInspector]
    public bool blueLoop = false;
    [HideInInspector]
    public bool orangeActive = false;
    [HideInInspector]
    public bool orangeLoop = false;
    [HideInInspector]
    public bool rotateOrange = false;

    public float speed = 1.0f;
    public float jumpForce = 1000f;

    public bool bluePressed;

    public bool orangePressed;

    public Transform BlueNote;
    public Transform OrangeNote;


    private AudioSource BeatSource;
    public AudioClip JumpAudio;
    public AudioClip BlueAudio;
    public AudioClip OrangeAudio;


    private Transform groundCheck;
    private bool onGround = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private Animator mouthAnim;
    private bool checkpointSet = false;
    private Animator currentCheckpoint;
    private Transform emitter;

    private CircleCollider2D collider;

    //Cheat Codes
    private List<GameObject> Checkpoints;
    List<string> cheatInputs = new List<string>(new string[] { "1", "2", "3", "4", "5"});


    // Use this for initialization
    void Awake () {

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        mouthAnim = GameObject.Find("Mouth").GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        emitter = GameObject.Find("NoteEmitter").GetComponent<Transform>();
        BeatSource = GetComponent<AudioSource>();

        collider = GetComponent<CircleCollider2D>();

        bluePressed = false;
        orangePressed = false;

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
        
        // If the jump button is pressed and the player is grounded then the player should jump.
        if ( (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) ) && onGround)
            jump = true;

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


        ////Loop Buttons
        //if (other.gameObject.CompareTag("BlueButton"))
        //{
        //    bluePressed = true;
        //}
        //if (other.gameObject.CompareTag("OrangeButton"))
        //{
        //    orangePressed = true;
        //}

    }

    private void Die()
    {
        alive = false;
        anim.SetTrigger("Die");
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.gravityScale = 0.0f;
        rb2d.velocity = Vector3.zero;
        collider.enabled = false;
    }

    private void Respawn()
    {
        alive = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.identity;
        rb2d.gravityScale = 1.0f;
        transform.position = checkpoint.position;
        collider.enabled = true;
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

        BeatSource.clip = JumpAudio;
        BeatSource.Play();

        jump = false;
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
