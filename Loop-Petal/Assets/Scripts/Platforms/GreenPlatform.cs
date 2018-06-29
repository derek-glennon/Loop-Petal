using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatform : Platform {
    public float selfPushForce;
    public float otherPushForce;
    public float speed;

    public GameObject[] BluePlatforms;

    private bool goingUp;
    private bool isActive;
    private Vector3 startPos;
    private Rigidbody2D rb2d;

    public void Awake()
    {
        selfPushForce = 3000f;
        otherPushForce = 500f;
        speed = 1f;

        goingUp = false;

        startPos = transform.position;
        rb2d = this.GetComponent<Rigidbody2D>();

    }

    public override void Activate()
    {
        isActive = true;
        transform.position = startPos;
        rb2d.velocity = Vector3.zero;

        rb2d.AddForce(new Vector2(0.0f, selfPushForce));

        //Change layer to the same as player so the player cannot jump to gain extra height
        gameObject.layer = 8;

    }

    public override void Deactivate()
    {
        //Turn layer back to Ground so the player can jump on platform
        gameObject.layer = 9;

        isActive = false;
    }

    public override void Update()
    {
        if (isActive && rb2d.velocity.y < 0.001f)
        {
            goingUp = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        }
        else
        {
            goingUp = true;
        }

        BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            Physics2D.IgnoreCollision(bluePlatform.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, otherPushForce));
        }
    }

}
