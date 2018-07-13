using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScaleDecrease : MonoBehaviour {

    public float decreaseVolumerPerFrame;


    private AudioSource audioSource;
    private bool isActive;

    // Use this for initialization
    void Awake () {

        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            audioSource.Play();

            collision.gameObject.GetComponent<PlayerController>().canMove = false;

            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.001f;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            collision.gameObject.GetComponent<Rigidbody2D>().angularDrag = 4f;
        }
    }
}
