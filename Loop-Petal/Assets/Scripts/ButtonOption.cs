using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour {
    private ButtonController parentButton;
    public int option;
    private Color startColor;
    private SpriteRenderer sprite;
    private ParticleSystem particle;

	// Use this for initialization
	void Start () {
        parentButton = this.transform.GetComponentInParent<ButtonController>();
        particle = this.GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (parentButton.selectedOption == option && particle.isStopped && parentButton.isActive)
        {
            particle.Play();
        } else if (parentButton.selectedOption != option && particle.isPlaying && parentButton.isActive)
        {
            particle.Stop();
        }
	}
}
