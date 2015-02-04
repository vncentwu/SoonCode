using UnityEngine;
using System.Collections;

public class FlashingRedArrow : MonoBehaviour {

	public float flashDuration = 0.8f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ToggleVisibility", 0, flashDuration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowAll()
	{
		transform.renderer.enabled = true;
		transform.GetChild(0).renderer.enabled = true;
		transform.GetChild (1).renderer.enabled = true;

	}

	void ToggleVisibility()
	{
		if(transform.renderer!=null)
			transform.renderer.enabled = !transform.renderer.enabled;
	}
}
