using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate()
    {
        Debug.Log("platform parent");
    }
}
