using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    private PlayerController player;
    private TimingController Timer;

    private GameObject[] BluePlatforms;
    private GameObject[] OrangePlatforms;

    public  AnimationClip horizToVert;
    public AnimationClip vertToHoriz;

    // Use this for initialization
    void Awake() {

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Timer= GameObject.Find("TimingController").GetComponent<TimingController>();
        BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
        OrangePlatforms = GameObject.FindGameObjectsWithTag("OrangePlatform");


    }

    // Update is called once per frame
    void FixedUpdate () {

        if (player.blueActive)
            ActivateBlue();
        else
            DeactivateBlue();

        if (!player.blueLoop)
            DeactivateBlue();

        if (player.orangeActive && player.orangeLoop)
            RotateOrange();

        foreach (GameObject orangeplatform in OrangePlatforms)
        {
            orangeplatform.GetComponent<Animator>().speed = vertToHoriz.length / 1.0f;
        }

    }

    private void ActivateBlue()
    {
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            if (bluePlatform.GetComponent<BoxCollider2D>().enabled == false)
                bluePlatform.GetComponent<BoxCollider2D>().enabled = true;
            bluePlatform.GetComponent<Animator>().SetBool("Active", true);
        }
    }

    private void DeactivateBlue()
    {
        foreach (GameObject bluePlatform in BluePlatforms)
        {
            bluePlatform.GetComponent<BoxCollider2D>().enabled = false;
            bluePlatform.GetComponent<Animator>().SetBool("Active", false);
        }
    }

    private void RotateOrange()
    {
        foreach (GameObject orangePlatform in OrangePlatforms)
        {
            orangePlatform.GetComponent<Animator>().SetTrigger("Rotate");
        }
    }

}
