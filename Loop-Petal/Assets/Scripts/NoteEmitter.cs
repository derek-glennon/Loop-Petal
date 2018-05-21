using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEmitter : MonoBehaviour {

    public Transform BlueNote;
	
	// Update is called once per frame
	void Update () {

       if (Input.GetKeyDown("[5]"))
        {
            Transform clone = Instantiate(BlueNote, transform.position, Quaternion.identity) as Transform;
        }

    }
}
