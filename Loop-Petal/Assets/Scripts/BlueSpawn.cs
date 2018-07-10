using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlueSpawn : MonoBehaviour {
    public bool alternating;
    public bool altSpawn;
    private bool altSpawnInit;
    [HideInInspector]
    public bool isActive;

    public GameObject BluePlatform;
    public GameObject GrowingBluePlatform;

    private GrowingBluePlatform growingBlue;

    public float timeTillDeath;
    public float holdTime;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        GameObject clone = Instantiate(GrowingBluePlatform, transform.position, transform.rotation) as GameObject;
        clone.transform.parent = transform;
        growingBlue = clone.GetComponent<GrowingBluePlatform>();

        altSpawnInit = altSpawn;
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGrowingPlatformScale(float timePassed)
    {
        growingBlue.SetScale(timePassed);
    }

    public void CreatePlatform()
    {
        GameObject clone = Instantiate(BluePlatform, transform.position, transform.rotation) as GameObject;
        clone.transform.parent = transform;
        clone.GetComponent<BluePlatform>().timeTillDeath = timeTillDeath;
        clone.GetComponent<BluePlatform>().holdTime = holdTime;
        clone.transform.localScale = transform.localScale;
    }

    public void Spawn()
    {
        if (!alternating)
        {
            CreatePlatform();
        }
        else if (alternating)
        {
            if (altSpawn == true)
            {
                CreatePlatform();
                altSpawn = false;
            }
            else
            {
                altSpawn = true;
            }
        }
        
    }

    public void Deactivate()
    {
        altSpawn = altSpawnInit;
    }

}
