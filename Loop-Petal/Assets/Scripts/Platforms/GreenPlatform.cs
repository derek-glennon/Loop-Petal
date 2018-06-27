using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatform : Platform {
    public float pushForce;
    public float speed;

    private bool isActive;
    private Vector3 startPos;
    private Rigidbody2D rb2d;


    public void Awake()
    {
        pushForce = 500000f;
        speed = 1f;


        startPos = transform.position;
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    public override void Activate()
    {
        isActive = true;
        transform.position = startPos;

        rb2d.AddForce(new Vector2(0.0f, pushForce));

    }

    public override void Update()
    {
        if (isActive && rb2d.velocity.y < 0.1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        }

    }
}
