using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlueSpawn : MonoBehaviour {
    public int numberActive;
    public bool alternating;
    public bool altSpawn;
    private bool altSpawnInit;
    [HideInInspector]
    public bool isActive;
    public GameObject BluePlatform;
    public BluePlatform[] bluePlatforms;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        altSpawnInit = altSpawn;

        for (int i = 0; i < numberActive; i++)
        {
            GameObject clone = Instantiate(BluePlatform, transform.position, transform.rotation) as GameObject;
            clone.transform.parent = transform;
        }

        bluePlatforms = GetComponentsInChildren<BluePlatform>();
		
	}

    public void CreatePlatform()
    {
        //There is probs a better way to do this

        //Find how many are currently active
        int currentlyActive = 0;
        foreach (BluePlatform bluePlatform in bluePlatforms)
        {
            if (bluePlatform.alive)
                currentlyActive++;
        }

        //If not all are active then activate one that is not 
        if (currentlyActive < numberActive)
        {
            for (int i = 0; i < bluePlatforms.Length; i++)
            {
                if (!bluePlatforms[i].alive)
                {
                    bluePlatforms[i].Activate();
                    return;
                }
            }
        }
        //If all are active, activate the one that was activated the longest ago
        else if (currentlyActive == numberActive)
        {
            float[] timeAlives = new float[bluePlatforms.Length];
            for (int i = 0; i < bluePlatforms.Length; i++)
            {
                timeAlives[i] = bluePlatforms[i].timeAlive;
            }
            float maxValue = timeAlives.Max();
            int MaxIndex = timeAlives.ToList().IndexOf(maxValue);
            bluePlatforms[MaxIndex].Activate();
            return;
        }
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
        foreach(BluePlatform bluePlatform in bluePlatforms)
        {
            bluePlatform.Deactivate();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
