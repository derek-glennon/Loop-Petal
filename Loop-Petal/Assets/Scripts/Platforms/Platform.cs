using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;
    protected Transform trans;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Activate()
    {
        Debug.Log("platform parent");
    }

    public virtual void Deactivate()
    {
        Debug.Log("platform parent");
    }
}
