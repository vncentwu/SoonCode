using UnityEngine;
using System.Collections;

public class TutorialPlatform1Script : MonoBehaviour {

	public float flashDuration = 0.8f;
	public string broadcastWhenReached = "Run1";

	bool flash;
	bool entered;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("ToggleVisibility", 0, flashDuration);



	}
	
	// Update is called once per frame
	void Update () {


	}
	
	public void ShowAll()
	{
		//transform.renderer.enabled = true;
		if(transform.GetChild (2).renderer!=null)
			transform.GetChild(2).renderer.enabled = true;
		if(transform.GetChild (3).light !=null)
			transform.GetChild (3).light.enabled = true;
		if(transform.GetChild (4).GetComponent<ParticleSystem>() !=null)
			transform.GetChild (4).GetComponent<ParticleSystem>().Play ();
		if(transform.GetChild (5).GetComponent<ParticleSystem>() !=null)
			transform.GetChild (5).GetComponent<ParticleSystem>().Play ();

		flash = true;

		//Debug.Log ("we got hre");

	}

	public void ShowNone()
	{
		if(transform.GetChild (2).renderer!=null)
			transform.GetChild(2).renderer.enabled = false;
		if(transform.GetChild (3).light !=null)
			transform.GetChild (3).light.enabled = false;
		if(transform.GetChild (4).GetComponent<ParticleSystem>() !=null)
		{
			transform.GetChild (4).GetComponent<ParticleSystem>().loop = false;
			transform.GetChild (4).GetComponent<ParticleSystem>().playbackSpeed = 3.5f;
		}
			
		if(transform.GetChild (5).GetComponent<ParticleSystem>() !=null)
		{
			transform.GetChild (5).GetComponent<ParticleSystem>().loop = false;
			transform.GetChild (5).GetComponent<ParticleSystem>().playbackSpeed = 3.5f;
		}
			
		
		flash = false;
	}

	void ToggleVisibility()
	{
		if(flash)
		{
			if(transform.GetChild (2).renderer!=null)
				transform.GetChild (2).renderer.enabled = !transform.GetChild (2).renderer.enabled;
			if(transform.GetChild (3).light !=null)
				transform.GetChild (3).light.enabled = !transform.GetChild (3).light.enabled;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name.Equals ("Player"))
		{
			if(!entered && flash)
			{
				entered = true;
				ConversationScript.broadcasts.Add (broadcastWhenReached);
				ShowNone ();
			}
		}
			
	}

}
