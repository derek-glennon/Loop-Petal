using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatform : Platform {
    public float selfPushForce;
    public float otherPushForce;
    public float otherVelocity;
    public float speed;
    public float delayTime;

    private Collider2D greenCollider2D;
    private Collider2D playerCollider;


    public float timePassed;

    public GameObject[] BluePlatforms;

    public bool isActive;
    private Vector3 startPosition;
    private Rigidbody2D rb2d;

    private Collider2D jumpCheck;


    public void Awake()
    {

        startPosition = transform.position;
        rb2d = this.GetComponent<Rigidbody2D>();

        greenCollider2D = GetComponent<Collider2D>();
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();

        jumpCheck = GetComponentInChildren<BoxCollider2D>();

    }

    public override void Update()
    {
        if (isActive)
        {
            timePassed += Time.deltaTime;

            if (timePassed >= delayTime)
            {
                isActive = false;
                timePassed = 0.0f;
            }

            if (greenCollider2D.IsTouching(playerCollider))
            {
                playerCollider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerCollider.gameObject.GetComponent<Rigidbody2D>().velocity.x, otherVelocity);
                if (playerCollider.gameObject.tag == "Player")
                {
                    playerCollider.gameObject.GetComponent<PlayerController>().isJumping = false;
                }
                isActive = false;
            }

        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);

            boxCollider.isTrigger = false;

            //Change layer to the same as player so the player cannot jump to gain extra height
            gameObject.layer = 9;
        }

        BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            Physics2D.IgnoreCollision(bluePlatform.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }


    }

    public override void Activate()
    {
        isActive = true;

        transform.position = startPosition;
        rb2d.velocity = Vector3.zero;

        rb2d.AddForce(new Vector2(0.0f, selfPushForce));

        boxCollider.isTrigger = true;

        //Change layer to the same as player so the player cannot jump to gain extra height
        gameObject.layer = 8;

    }

}
