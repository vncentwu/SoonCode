using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	GameObject target;
	bool teleportable;
	public string targetScene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(teleportable && Input.GetKey (KeyCode.X))
			Application.LoadLevel (targetScene);

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name.Equals ("Player"))
			teleportable = true;
		Debug.Log (teleportable);
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name.Equals ("Player"))
			teleportable = false;
	}
	


}
