using UnityEngine;
using System.Collections;

public class Finder : MonoBehaviour {

	GameObject current;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject.tag.Equals ("Enemy"))
		{
			other.transform.gameObject.GetComponent<TrollController>().OnSelectorEnter();
			//current.GetComponent<TrollController>().OnSelectorExit();
			//Debug.Log ("YOOOOOOOOOOOOOOO");

			//current = other.transform.gameObject;
		}
			


	}

	void OnTriggerExit(Collider other)
	{

		if(other.transform.gameObject.tag.Equals ("Enemy"))
		{
			//Debug.Log ("EEEEEXXXIT");
			other.transform.gameObject.GetComponent<TrollController>().OnSelectorExit();
		}
			
	}


}
