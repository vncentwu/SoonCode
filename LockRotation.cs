using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 transform.rotation = Quaternion.Euler (0.0f, 0.0f, 270.0f); 
	}
}
