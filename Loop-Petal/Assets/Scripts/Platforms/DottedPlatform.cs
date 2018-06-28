using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedPlatform : MonoBehaviour {

    CircleCollider2D player;

    BoxCollider2D boxCollider;

	// Use this for initialization
	void Awake () {

        player = GameObject.Find("Player").GetComponent<CircleCollider2D>();

        boxCollider = GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(player, boxCollider);
    }

    // Update is called once per frame
    void Update () {

		
	}
}
