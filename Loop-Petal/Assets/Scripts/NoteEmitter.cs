using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEmitter : MonoBehaviour {

    public Transform BlueNote;

    private PlayerController player;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {

        if (player.alive)
        {
            if (Input.GetKeyDown("[5]"))
            {
                if (!player.blueActive)
                {
                    //Transform clone;
                    //clone = Instantiate(BlueNote, transform.position, Quaternion.identity) as Transform;
                }

            }

        }

    }
}
