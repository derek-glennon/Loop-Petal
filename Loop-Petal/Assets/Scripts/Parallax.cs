using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform camera;
    private Vector3 previousCameraPosition;

    // Called before Start
    void Awake()
    {
        camera = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        previousCameraPosition = camera.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++)
        {
            // find position x offset (difference between current camera and camera last frame) and multiply by the scale
            float parallax = (previousCameraPosition.x - camera.position.x) * parallaxScales[i];
            // set target x position
            float backgroundTargetPositionX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);
            // fade from current x position to target x position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
        }

        // set previous camera position at end of frame
        previousCameraPosition = camera.position;
	}
}
