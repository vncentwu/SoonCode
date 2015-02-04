using UnityEngine;
using System.Collections;

public class SlowlyRotate : MonoBehaviour {

	public float speed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (new Vector3(0, speed*Time.deltaTime, 0));
	}
}
