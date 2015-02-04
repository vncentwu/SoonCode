using UnityEngine;
using System.Collections;

public class HealScript : MonoBehaviour {

	public float healAmount = 30.0f;


	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", 6.0f);
	}

	void DestroySelf()
	{
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
