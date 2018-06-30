using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform {
    [HideInInspector]
    public bool isActive;
    [HideInInspector]
    public bool alive;

    public float holdTime;
    public int numberActive;
    public float timeAlive;

    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody2D rb2d;
    private float timePassed;
    private bool turnOff;
    private bool inDeath;

    public void Awake()
    {
        holdTime = 0.3f;

        //Not sure why but Unity freaked out when I didn't have these here even though they are in Platform's Start function?
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        startRot = transform.rotation;
        startPos = transform.position;
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.0f;

        isActive = false;

        inDeath = false;

        timeAlive = 0.0f;

        //Just remove this line eventually
        animator.SetBool("Active", true);
    }

    public override void Activate()
    {
        alive = true;
        inDeath = false;
        //isActive = !isActive;
        isActive = true;
        transform.position = startPos;
        transform.rotation = startRot;
        rb2d.gravityScale = 0.0f;
        rb2d.velocity = Vector3.zero;
        rb2d.angularVelocity = 0.0f;

        timePassed = 0.0f;
        timeAlive = 0.0f;

        rb2d.drag = 0f;


        if (!spriteRenderer.enabled)
            spriteRenderer.enabled = true;
        if (!boxCollider.enabled)
            boxCollider.enabled = true;



        //animator.SetBool("Active", isActive);
    }

    public override void Deactivate()
    {
        isActive = false;
        turnOff = true;
        //spriteRenderer.enabled = false;
        //boxCollider.enabled = false;
        //rb2d.gravityScale = 1.0f;
        //rb2d.velocity = Vector3.zero;

    }

    public override void Update()
    {
        timeAlive += Time.deltaTime;

        if (isActive)
        {
            if (timePassed < holdTime)
            {
                timePassed += Time.deltaTime;
            }
            else
            {
                isActive = false;
                rb2d.gravityScale = 1.0f;
            }
        }

        if (turnOff)
        {
            if (timePassed < holdTime)
            {
                timePassed += Time.deltaTime;
            }
            else
            {
                rb2d.gravityScale = 1.0f;
            }
        }

        if (rb2d.velocity == Vector2.zero && inDeath)
            alive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death") )//&& turnOff)
        {
            inDeath = true;
            turnOff = false;
            //alive = false;
            rb2d.gravityScale = 0.0f;
            rb2d.drag = 1f;
            rb2d.angularDrag = 1f;
        }
    }
}
