using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedPlatform : MonoBehaviour {

    Collider2D player;

    BoxCollider2D boxCollider;

	// Use this for initialization
	void Awake () {

        player = GameObject.Find("Player").GetComponent<Collider2D>();

        boxCollider = GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(player, boxCollider);
    }

    // Update is called once per frame
    void Update () {

		
	}
}
