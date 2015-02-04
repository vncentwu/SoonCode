using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ConversationScript : MonoBehaviour {

	public ConversationFrame[] convos;
	public static List<string> broadcasts = new List<string>();

	public int currentFrame = 0;
	Camera portraitCamera;
	// Use this for initialization
	void Start () {
		portraitCamera = GameObject.Find ("PortraitCamera").camera;
		if(portraitCamera!=null)
			portraitCamera.enabled = false;
		//ShowStats.addMessage ("");
		//ShowStats.addMessage ("Talk to the NPC dude to view tutorial.");
		//ShowStats.addMessage ("-Work in progress-");
	}
	
	// Update is called once per frame
	void Update () {

		CheckConsume();
		if(Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1<<10)))
			{
				if(hit.transform.gameObject.Equals (gameObject))
				{
					ShowStats.StartConversation(this);
					portraitCamera.enabled = true;
				}
			}

		}
	}

	public static bool FindAndRemoveBroadcast(string x)
	{
		for(int i = 0; i<broadcasts.Count; i++)
		{
			if(x.Equals (broadcasts[i]))
			{
				broadcasts[i] = "null";
				return true;
			}
		}
		return false;
	}

	void CheckConsume()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1<<10)))
		{
			if(hit.transform.gameObject.Equals (gameObject))
			{
				ShowStats.mouseConsumed = true;
			}
		}
	}

	void OnMouseOver()
	{
		//Debug.Log ("over. over");
		ShowStats.mouseConsumed = true;
	}
	public void CameraOn()
	{
		portraitCamera.enabled = true;
	}

	public void CameraOff()
	{
		portraitCamera.enabled = false;
	}

	public void ApplyTarget(string tar)
	{
		if(tar.Equals (""))
			currentFrame = -1;
		for(int i = 0; i<convos.Length; i++)
		{
			if(tar.Equals (convos[i].name))
				currentFrame = i;
		}

	}
	public void ApplyBroadcast(string broadcast)
	{
		broadcasts.Add (broadcast);
	}


	public string GetMessage()
	{
		if(currentFrame<convos.Length && currentFrame>=0)
			return convos[currentFrame].message;
		else
			return "EMPTY";
	}

	public Responses[] GetResponses()
	{
		if(currentFrame<convos.Length)
			return convos[currentFrame].responses;
		else
			return null;
	}

	public string GetMessage(int frame)
	{
		if(frame<convos.Length)
			return convos[frame].message;
		else
			return "EMPTY";
	}




	[System.Serializable]
	public class ConversationFrame{

		public string name;
		public string message;
		public Responses[] responses;

	}

	[System.Serializable]
	public class Responses{
		public string target;
		public string response;
		public string broadcast;


	}
}
