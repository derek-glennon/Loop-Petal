using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform {
    public bool isActive;
    public float holdTime = 0.5f;
    public int numberActive;
    public float timeAlive;
    public bool alive;

    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody2D rb2d;
    private float timePassed;
    private bool turnOff;

    public void Awake()
    {
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

        timeAlive = 0.0f;

        //Just remove this line eventually
        animator.SetBool("Active", true);
    }

    public override void Activate()
    {
        alive = true;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death") && turnOff)
        {
            turnOff = false;
            alive = false;
            rb2d.gravityScale = 0.0f;
            rb2d.drag = 10f;
            rb2d.angularDrag = 10f;
        }
    }
}
