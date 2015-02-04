using UnityEngine;
using System.Collections;

public class SetOnFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals ("Player"))
						other.gameObject.GetComponent<PlatformerController> ().SetOnFire ();
	}

}
