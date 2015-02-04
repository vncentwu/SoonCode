using UnityEngine;
using System.Collections;

public class SimpleCollisionCheck : MonoBehaviour {

	public bool grounded = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log (other.gameObject.name);
		//grounded = true;
	}

	void OnCollisionExit()
	{
		//grounded = false;
	}


	void OnTriggerEnter(Collider other)
	{

		if (!other.gameObject.name.Equals ("Player")) {
			grounded = true;
			Debug.Log ("enter:" + other.gameObject.name);
		}
		   

	}

	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.name.Equals ("Player")) {
			grounded = false;
			Debug.Log ("exit: " + other.gameObject.name);
		}

	}
}


