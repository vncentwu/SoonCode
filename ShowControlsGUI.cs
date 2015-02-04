using UnityEngine;
using System.Collections;

public class ShowControlsGUI : MonoBehaviour {


	public Texture2D controls;
	bool show = true;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown (KeyCode.F7))
			show = !show;
	}

	void OnGUI()
	{
		if(show)
			GUI.Label (new Rect (0, 0, 250.0f, 125.0f), controls);

	}

}
