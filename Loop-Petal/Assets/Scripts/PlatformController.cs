using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    private PlayerController player;

    private GameObject[] BluePlatforms;
    private GameObject[] OrangePlatforms;

    // Use this for initialization
    void Awake() {

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        BluePlatforms = GameObject.FindGameObjectsWithTag("BluePlatform");
        OrangePlatforms = GameObject.FindGameObjectsWithTag("OrangePlatform");

    }

    // Update is called once per frame
    void FixedUpdate () {

        if (player.blueActive)
            ActivateBlue();
        else if (!player.blueActive)
            DeactivateBlue();

        if (player.orangeActive && player.orangeTimer == player.orangeTimerInit)
            RotateOrange();

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
