using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingController : MonoBehaviour {


    public float timer;
    private float maxTimerTime = 3.0f;

    List<float> ticksPos = new List<float>();
    float counter = 0.0f;

    public Image Background;
    public GameObject AnchorPoint;
    public Image Timeline;

    public Image Tick;

    private float velocity;
    private float fps;

    // Use this for initialization
    void Awake () {

        fps = 1.0f / Time.deltaTime;

        //Set Up the tick positions in seconds
        timer = 0.0f;
        while (counter <= maxTimerTime)
        { 
            ticksPos.Add(counter);
            counter += 0.5f;
        }

        //Set up Timeline
        Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        //Set Up Velocity
        velocity = Background.rectTransform.sizeDelta.x / maxTimerTime; //In units pos/sec
        velocity = velocity / fps;// divide by fps to get velocity in units pos/frame

        //Set Up Ticks
        for(float i = 1.0f; i < ticksPos.Count; i += 1.0f)
        {
            float realPos = (i / (ticksPos.Count)) * Background.rectTransform.sizeDelta.x;
            var TickClone = Instantiate(Tick, AnchorPoint.transform) as Image;
            TickClone.transform.SetParent(AnchorPoint.transform, false);
            TickClone.rectTransform.anchoredPosition = new Vector2(realPos, 0.0f);
        }

    }
	
	// Update is called once per frame
	void Update () {

        //Update timer and reset if it gets past the end of the background
        timer += Time.deltaTime;
        if (timer >= maxTimerTime)
            timer = 0.0f;

        //Update Timeline
        Timeline.rectTransform.anchoredPosition = new Vector2(Timeline.rectTransform.anchoredPosition.x + velocity, Timeline.rectTransform.anchoredPosition.y);

        //Reset Timeline if it goes too far
        if (Timeline.rectTransform.anchoredPosition.x > Background.rectTransform.sizeDelta.x)
        {
            Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        }


    }
}
