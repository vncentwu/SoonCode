using UnityEngine;
using System.Collections;

public class StayTheFStill : MonoBehaviour {

	public Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = pos;
	}

}
