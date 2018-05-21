using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMine : MonoBehaviour {

    private Transform player;       // Reference to the player's transform.

    private Transform trans;

    // Use this for initialization
    void Awake () {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate () {

        trans.position = new Vector3(player.position.x, player.position.y, trans.position.z);


	}
}
