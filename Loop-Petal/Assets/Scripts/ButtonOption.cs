using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour {
    private ButtonController parentButton;
    public int option;
    private Color startColor;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        parentButton = this.transform.GetComponentInParent<ButtonController>();
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (parentButton.selectedOption == option && sprite.color != Color.black && parentButton.isActive)
        {
            sprite.color = Color.black;
        } else if (parentButton.selectedOption != option && sprite.color != startColor && parentButton.isActive)
        {
            sprite.color = startColor;
        }
	}
}
