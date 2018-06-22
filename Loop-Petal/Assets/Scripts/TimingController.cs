using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingController : MonoBehaviour {


    public float timer;
    private float maxTimerTime = 3.0f;
    private float DeltaTime = 0.5f;
    private int numberTicks;
    private float firstTickPos = 0.5f;

    float[] ticksPos;

    public Image Background;
    public GameObject AnchorPoint;
    public Image Timeline;

    public Image Tick;
    public Image BlueMarker;
    public Image OrangeMarker;

    private float velocity;
    private float fps;

    private PlayerController player;

    private float[] playPosBlue;
    private float activePosBlue = 0.0f;
    public float bufferTimeBlue;

    private float[] playPosOrange;
    private float activePosOrange = 0.0f;
    public float bufferTimeOrange;


    public float detectionBuffer = 0.05f;

    // Use this for initialization
    void Awake () {

        bufferTimeBlue = 0.5f;
        bufferTimeOrange = 0.3f;

        float div = maxTimerTime / DeltaTime;
        numberTicks = (int) div;//+1 to include 0.0f
        ticksPos = new float[numberTicks];

        playPosBlue = new float[numberTicks / 2];
        playPosOrange = new float[numberTicks / 2];

        fps = 1.0f / Time.deltaTime;

        player = GameObject.Find("Player").GetComponent<PlayerController>();

        timer = 0.0f;

        //Set Up the tick positions in seconds
        float counter = firstTickPos;
        for (int i = 0; i < ticksPos.Length; i++)
        { 
            ticksPos[i] = counter;
            counter += 0.5f;
        }

        //Set up Timeline
        Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        //Set Up Velocity
        velocity = Background.rectTransform.sizeDelta.x / maxTimerTime; //In units pos/sec
        //velocity = velocity / fps;// divide by fps to get velocity in units pos/frame

        //Set Up Ticks
        for(int i = 0; i < ticksPos.Length; i++)
        {
            float realPos = (ticksPos[i] / maxTimerTime) * Background.rectTransform.sizeDelta.x;
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
        Timeline.rectTransform.anchoredPosition = new Vector2(Timeline.rectTransform.anchoredPosition.x + (velocity * Time.deltaTime), Timeline.rectTransform.anchoredPosition.y);

        //The movement of the timeline is basically the same as the timer(~e-8 difference)
        //Debug.Log(Mathf.Abs(((Timeline.rectTransform.anchoredPosition.x / Background.rectTransform.sizeDelta.x) * maxTimerTime) - timer));

        //Reset Timeline if it goes too far
        if (Timeline.rectTransform.anchoredPosition.x > Background.rectTransform.sizeDelta.x)
        {
            Timeline.rectTransform.anchoredPosition = new Vector2(0.0f, Timeline.rectTransform.anchoredPosition.y);

        }

        //Check if the player pressed any buttons and handle interactions
        CheckBluePressed();
        CheckOrangePressed();


        //Play the Notes
        PlayBlue();
        PlayOrange();


    }





    private void CheckBluePressed()
    {
        int roundIndex = 0;
        float roundTickPos;

        //Round the Player's beat inputs
        if (player.bluePressed)
        {
            player.bluePressed = false;

            if (!player.blueLoop)
            {
                //Loop through every space between ticks
                for (int i = 0; i < ticksPos.Length - 1; i++)
                {
                    //For every space between ticks
                    if (i != ticksPos.Length - 1)
                    {
                        //If the timer is in between two of the ticks
                        if (timer > ticksPos[i] && timer < ticksPos[i + 1])
                        {
                            //Find which tick the timer is closest to
                            float leftTickDist = Mathf.Abs(ticksPos[i] - timer);
                            float rightTickDist = Mathf.Abs(ticksPos[i + 1] - timer);

                            if (leftTickDist < rightTickDist)
                            {
                                roundTickPos = ticksPos[i];
                                roundIndex = i;
                            }
                            else
                            {
                                roundTickPos = ticksPos[i + 1];
                                roundIndex = i + 1;
                            }

                            player.blueLoop = true;

                        }

                    }
                }
                int even = 0;
                int odd = 1;
                //Find which indicies of ticks should be played
                for (int i = 0; i < playPosBlue.Length; i++)
                {
                    if (roundIndex % 2 == 0)//even indicies
                    {
                        playPosBlue[i] = ticksPos[even];
                        even += 2;
                    }
                    else//odd indicies
                    {
                        playPosBlue[i] = ticksPos[odd];
                        odd += 2;
                    }
                }
                //Assign Markers on the UI
                for (int i = 0; i < playPosBlue.Length; i++)
                {
                    float realPos = (playPosBlue[i] / maxTimerTime) * Background.rectTransform.sizeDelta.x;
                    var BlueMarkerClone = Instantiate(BlueMarker, AnchorPoint.transform) as Image;
                    BlueMarkerClone.transform.SetParent(AnchorPoint.transform, false);
                    BlueMarkerClone.rectTransform.anchoredPosition = new Vector2(realPos, -1.0f * (Tick.rectTransform.sizeDelta.y / 3));
                }


            }
            else if (player.blueLoop)
            {
                player.blueLoop = false;
                GameObject[] BlueMarkers = GameObject.FindGameObjectsWithTag("BlueMarker");
                foreach (GameObject BlueMarker in BlueMarkers)
                    Destroy(BlueMarker);
            }

        }
    }


    private void CheckOrangePressed()
    {
        int roundIndex = 0;
        float roundTickPos;

        //Round the Player's beat inputs
        if (player.orangePressed)
        {
            player.orangePressed = false;

            if (!player.orangeLoop)
            {
                //Loop through every space between ticks
                for (int i = 0; i < ticksPos.Length - 1; i++)
                {
                    //For every space between ticks
                    if (i != ticksPos.Length - 1)
                    {
                        //If the timer is in between two of the ticks
                        if (timer > ticksPos[i] && timer < ticksPos[i + 1])
                        {
                            //Find which tick the timer is closest to
                            float leftTickDist = Mathf.Abs(ticksPos[i] - timer);
                            float rightTickDist = Mathf.Abs(ticksPos[i + 1] - timer);

                            if (leftTickDist < rightTickDist)
                            {
                                roundTickPos = ticksPos[i];
                                roundIndex = i;
                            }
                            else
                            {
                                roundTickPos = ticksPos[i + 1];
                                roundIndex = i + 1;
                            }

                            player.orangeLoop = true;

                        }

                    }
                }
                int even = 0;
                int odd = 1;
                //Find which indicies of ticks should be played
                for (int i = 0; i < playPosOrange.Length; i++)
                {
                    if (roundIndex % 2 == 0)//even indicies
                    {
                        playPosOrange[i] = ticksPos[even];
                        even += 2;
                    }
                    else//odd indicies
                    {
                        playPosOrange[i] = ticksPos[odd];
                        odd += 2;
                    }
                }
                //Assign Markers on the UI
                for (int i = 0; i < playPosOrange.Length; i++)
                {
                    float realPos = (playPosOrange[i] / maxTimerTime) * Background.rectTransform.sizeDelta.x;
                    var OrangeMarkerClone = Instantiate(OrangeMarker, AnchorPoint.transform) as Image;
                    OrangeMarkerClone.transform.SetParent(AnchorPoint.transform, false);
                    OrangeMarkerClone.rectTransform.anchoredPosition = new Vector2(realPos, 0.0f);
                }


            }
            else if (player.orangeLoop)
            {
                player.orangeLoop= false;
                GameObject[] OrangeMarkers = GameObject.FindGameObjectsWithTag("OrangeMarker");
                foreach (GameObject OrangeMarker in OrangeMarkers)
                    Destroy(OrangeMarker);
            }

        }
    }

    private void PlayBlue()
    {
        //Play the notes at the right time
        if (player.blueLoop)
        {
            for (int i = 0; i < playPosBlue.Length; i++)
            {
                if ((timer > playPosBlue[i] - detectionBuffer && timer < playPosBlue[i] + detectionBuffer) && !player.blueActive)
                {
                    player.EmitBlueNote();
                    player.blueActive = true;
                    activePosBlue = playPosBlue[i];
                }
            }


            //Deactivate the Platforms after a set amount of time
            if (player.blueActive)
            {
                if (activePosBlue + bufferTimeBlue <= maxTimerTime)
                {
                    if (timer > activePosBlue + bufferTimeBlue)
                        player.blueActive = false;
                }
                //Loop buffer time back to start of timeline if needed
                else if ((activePosBlue + bufferTimeBlue) > maxTimerTime)
                {
                    float diff = (activePosBlue + bufferTimeBlue) - maxTimerTime;
                    if (timer > diff - detectionBuffer && timer < diff + detectionBuffer)
                        player.blueActive = false;
                }
            }


        }

    }

    private void PlayOrange()
    {
        //Play the notes at the right time
        if (player.orangeLoop)
        {
            for (int i = 0; i < playPosOrange.Length; i++)
            {
                if ((timer > playPosOrange[i] - detectionBuffer && timer < playPosOrange[i] + detectionBuffer) && !player.orangeActive)
                {
                    player.EmitOrangeNote();
                    player.orangeActive = true;
                    activePosOrange = playPosOrange[i];
                }
            }

            //Deactivate the Platforms after a set amount of time
            if (player.orangeActive)
            {
                if (activePosOrange + bufferTimeOrange<= maxTimerTime)
                {
                    if (timer > activePosOrange+ bufferTimeOrange)
                        player.orangeActive = false;
                }
                //Loop buffer time back to start of timeline if needed
                else if ((activePosOrange+ bufferTimeOrange) > maxTimerTime)
                {
                    float diff = (activePosOrange+ bufferTimeOrange) - maxTimerTime;
                    if (timer > diff - detectionBuffer && timer < diff + detectionBuffer)
                        player.orangeActive = false;
                }
            }

        }

    }


}
