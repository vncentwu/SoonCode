using UnityEngine;
using System.Collections;

public class SimpleChildCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.name.Equals ("Player"))
		   transform.parent.GetComponent<TrollController>().entered = true;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.name.Equals ("Player"))
			transform.parent.GetComponent<TrollController>().entered = false;
	}
}
