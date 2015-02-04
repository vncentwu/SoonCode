using UnityEngine;
using System.Collections;

public class Simple2DCameraController : MonoBehaviour {

	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate (Vector3.forward * speed * 100* Time.deltaTime * Input.GetAxis ("Horizontal"));

	}
}
