using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TimelineController : MonoBehaviour {
    public float beatInterval = 0.5f;
    public float timelineSpan = 3f;
    private float timePassed;
    private float timelineStartTime;

    public GameObject TimelineStartPoint;
    public Image Background;
    public Image Timeline;

    private Object tickPrefab;
    private GameObject[] beatControllers;

    private float velocity;

    // Use this for initialization
    void Awake () {
        beatControllers = GameObject.FindGameObjectsWithTag("BeatController");

        //Set up Timeline
        Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        //Set Up Velocity
        velocity = Background.rectTransform.sizeDelta.x / timelineSpan; //In units pos/sec

    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (timePassed >= beatInterval)
        {
            timePassed = 0f;
            SpawnTick();
        }

        //Update Timeline
        Timeline.rectTransform.anchoredPosition = new Vector2(Timeline.rectTransform.anchoredPosition.x + (velocity * Time.deltaTime), Timeline.rectTransform.anchoredPosition.y);

        //The movement of the timeline is basically the same as the timer(~e-8 difference)
        //Debug.Log(Mathf.Abs(((Timeline.rectTransform.anchoredPosition.x / Background.rectTransform.sizeDelta.x) * timelineSpan) - timer));

        //Reset Timeline if it goes too far
        if (Timeline.rectTransform.anchoredPosition.x > Background.rectTransform.sizeDelta.x)
        {
            Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        }
    }

    private void SpawnTick()
    {
        //spawn new tick at start of timeline
        GameObject Tick = Instantiate(tickPrefab, TimelineStartPoint.transform.position, TimelineStartPoint.transform.rotation) as GameObject;
        //set velocity
        Tick.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0f);
        
        var beatsPlaying = new List<BeatType>(); //list of beats playing on this tick
        foreach (GameObject beatController in beatControllers)
        {
            // fill beatsPlaying list
        }
        //set those beats so they show up
        //Tick.GetComponent<TickController>().SetBeats(beatsPlaying);
    }
}
