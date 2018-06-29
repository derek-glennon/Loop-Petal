using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetronomeController : MonoBehaviour {
    private float subractValue;
    private float newValue;
    private float nearestQuarter;
    private float intervalSubtractValue;
    private float intervalValue;

    private RectTransform rectTransform;

    //Image image;

    // Use this for initialization
    void Awake() {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        //Find the nearest 1/4 sec
        nearestQuarter = Mathf.Round(Time.time * 4.0f) / 4.0f;
        //Find what you must subtract by in order to get to (0,0.25) range
        subractValue = Time.time < nearestQuarter ? nearestQuarter - 0.25f : nearestQuarter;
        //Find what part of a (0,0.25) range you are on
        newValue = Time.time - subractValue;
        newValue = (newValue / 0.25f);
        //Find what part of a (0,1) interval you are on
        intervalSubtractValue = Time.time < Mathf.Round(Time.time) ? Mathf.Round(Time.time) - 1.0f : Mathf.Round(Time.time);
        intervalValue = Time.time - intervalSubtractValue;

        Vector3 temp = rectTransform.rotation.eulerAngles;

        //Set the rotation based on what part of a (0,1) interval you are on
        if (intervalValue >=  0.0f && intervalValue <= 0.25f)
            temp.z = 90.0f * (newValue);
        if (intervalValue >= 0.25f && intervalValue <= 0.50f)
            temp.z = 90.0f * (1.0f - newValue);
        if (intervalValue >= 0.50f && intervalValue <= 0.75f)
            temp.z = -90.0f * (newValue);
        if (intervalValue >= 0.75f && intervalValue <= 1.0f)
            temp.z = -90.0f * (1.0f - newValue);


        rectTransform.rotation = Quaternion.Euler(temp);
	}
}
