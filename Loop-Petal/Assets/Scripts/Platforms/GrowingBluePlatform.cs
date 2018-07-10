using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBluePlatform : MonoBehaviour {

    public BeatController beatController;

    // Use this for initialization
    void Awake () {
        transform.localScale = new Vector2(0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void SetScale(float timePassed)
    {
        transform.localScale = new Vector2(timePassed, timePassed);
    }
}
