using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBounce : MonoBehaviour {
    public GameObject beatObject;
    private BeatController beatController;
    private RectTransform rectTransform;
    private float originialY;
    public float offsetY = 0.3f;
    public float smoothing = 1f;
    public Color beatColor;
    private bool isBouncing;

    // Use this for initialization
    void Start () {
        beatController = beatObject.GetComponent<BeatController>();
        rectTransform = GetComponent<RectTransform>();
        originialY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isBouncing && beatController.isPlaying)
        {
            isBouncing = true;
            foreach (SpriteRenderer sprite in this.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = beatColor;
            }
        }

        if (isBouncing)
        {
            // what percent of the time between beats has passed
            var beatPercent = beatController.timePassed / beatController.beatInterval;

            // bounce the image up on the beat and slowly back down
            this.transform.position = Vector3.Lerp(
            this.transform.position,
            new Vector3(
                this.transform.position.x,
                originialY + (offsetY * (1 - (beatController.timePassed / beatController.beatInterval)))
                ),
            smoothing * Time.deltaTime);
        }
    }
}
