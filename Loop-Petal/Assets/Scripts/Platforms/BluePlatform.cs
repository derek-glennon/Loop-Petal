using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform {
    [HideInInspector]
    public bool isActive;
    [HideInInspector]
    public bool isAlive;

    public float timeTillDeath;
    private float deathTimer;

    public float holdTime;
    public float timeAlive;

    private Rigidbody2D rb2d;

    public void Awake()
    {
        //Not sure why but Unity freaked out when I didn't have these here even though they are in Platform's Start function?
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.0f;

        isAlive = true;
        timeAlive = 0.0f;

        //Just remove this line eventually
        animator.SetBool("Active", true);
    }

    public override void Update()
    {
        if (isAlive)
            timeAlive += Time.deltaTime;
        else
            deathTimer += Time.deltaTime;

        if (timeAlive >= holdTime)
        {
            //rb2d.gravityScale = 1.0f;
            rb2d.gravityScale = 0.75f;
        }

        if (deathTimer >= timeTillDeath)
            Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death") )
        {
            isAlive = false;
            rb2d.gravityScale = 0.0f;
            rb2d.drag = 1f;
            rb2d.angularDrag = 1f;
        }
    }
}
