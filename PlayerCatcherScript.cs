using UnityEngine;
using System.Collections;

public class PlayerCatcherScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name.Equals ("Player"))
		{
			other.gameObject.transform.position = transform.GetChild (0).position;
		}
	}

}

