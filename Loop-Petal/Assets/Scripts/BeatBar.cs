using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour {
    public GameObject beatObject;
    private BeatController beatController;
    private RectTransform rectTransform;

    public float minSize = 100f;
    public float maxSize = 400f;

	// Use this for initialization
	void Start () {
        beatController = beatObject.GetComponent<BeatController>();
        rectTransform = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (beatController.isPlaying)
        {
            rectTransform.sizeDelta = new Vector2(
                maxSize - (beatController.timePassed/beatController.beatInterval)*(maxSize-minSize), 
                rectTransform.sizeDelta.y
                );
        }
	}
}
