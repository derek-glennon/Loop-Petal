using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMine : MonoBehaviour {

    private Transform player;       // Reference to the player's transform.

    private Vector2 velocity;

    public float smoothTimeY = 0.05f;
    public float smoothTimeX = 0.05f;

    // Use this for initialization
    void Awake () {

        player = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void FixedUpdate () {

        //trans.position = new Vector3(player.position.x, player.position.y, trans.position.z);
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

    }
}
