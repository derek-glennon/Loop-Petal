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
    [HideInInspector]
    public bool blueActive = false;
    [HideInInspector]
    public bool orangeActive = false;
    [HideInInspector]
    public bool rotateOrange = false;

    public float blueTimer;
    public float orangeTimer;

    public float blueTimerInit = 0.95f;
    public float orangeTimerInit = 0.95f;

    public float speed = 1.0f;
    public float jumpForce = 1000f;

    public Transform BlueNote;
    public Transform OrangeNote;


    private Transform groundCheck;
    private bool onGround = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private Animator mouthAnim;
    private bool checkpointSet = false;
    private Animator currentCheckpoint;
    private Transform emitter;

    //Cheat Codes
    private List<GameObject> Checkpoints;
    List<string> cheatInputs = new List<string>(new string[] { "1", "2", "3", "4"});


    // Use this for initialization
    void Awake () {

        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        mouthAnim = GameObject.Find("Mouth").GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        emitter = GameObject.Find("NoteEmitter").GetComponent<Transform>();

        blueTimer = blueTimerInit;
        orangeTimer = orangeTimerInit;


        //Cheat Codes
        Checkpoints = new List<GameObject>();
        Checkpoints.Add(GameObject.Find("Checkpoint"));
        for (int i  = 0; i < cheatInputs.Count; i++)
        {
            Checkpoints.Add(GameObject.Find("Checkpoint (" + cheatInputs[i] + ")"));
        }

    }
	
	// Update is called once per frame
	void Update () {

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && onGround)
            jump = true;

        //If player is dead and the death animation has finished, then respawn
        if (!alive && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerNoiseDeath") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            Respawn();

        //Blue Timer
        if (blueActive)
            blueTimer -= Time.deltaTime;
        else if (!blueActive)
            blueTimer = blueTimerInit;

        //Orange Timer
        if (orangeActive)
            orangeTimer -= Time.deltaTime;
        else if (!orangeActive)
            orangeTimer = orangeTimerInit;



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

            //If 5 is pressed play blue note
            if (Input.GetKeyDown("[5]") == true)
                EmitBlueNote();

            //If 4 is pressed play orange note
            if (Input.GetKeyDown("[4]") == true)
                EmitOrangeNote();

            //Flip the Player if direction of movement is changed
            if (horizontal > 0 && !facingRight)
                Flip();
            else if (horizontal < 0 && facingRight)
                Flip();

            //Jump if able
            if (jump)
                Jump();
        }

        //Turning off notes
        if (blueTimer < 0.0f)
            blueActive = false;

        if (orangeTimer < 0.0f)
            orangeActive = false;


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
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.gravityScale = 0.0f;
        rb2d.velocity = Vector3.zero;
    }

    private void Respawn()
    {
        alive = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.identity;
        rb2d.gravityScale = 1.0f;
        transform.position = checkpoint.position;
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

    private void EmitBlueNote()
    {
        if (blueTimer == blueTimerInit)
        {
            blueActive = true;

            if (isMoving)
            {
                mouthAnim.SetTrigger("BeatBlue");
            }
            else
                anim.SetTrigger("BeatBlue");

            Transform clone;
            clone = Instantiate(BlueNote, emitter.position, Quaternion.identity) as Transform;
        }

    }


    private void EmitOrangeNote()
    {

        if (orangeTimer == orangeTimerInit)
        {
            orangeActive = true;
            rotateOrange = true;

            if (isMoving)
            {
                mouthAnim.SetTrigger("BeatBlue");
            }
            else
                anim.SetTrigger("BeatBlue");

            Transform clone;
            clone = Instantiate(OrangeNote, emitter.position, Quaternion.identity) as Transform;
        }
    }

}
