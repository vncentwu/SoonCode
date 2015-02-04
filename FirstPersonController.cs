using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	public float ms = 15.0f;
	public float mouseSens = 2.0f;
	public float maxDeg = 60.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//rotation
		float rotLR = Input.GetAxis ("Mouse X") * mouseSens;
		transform.Rotate (0, rotLR, 0);

		float rotUD = Input.GetAxis ("Mouse Y") * mouseSens;



		float currentDeg = Camera.main.transform.rotation.eulerAngles.x;
		float desiredDeg = currentDeg - rotUD;

		//Camera.main.transform.rotation = Quaternion.Euler (desiredDeg, 0, 0);


		//movement
		float forwardSpeed = Input.GetAxis("Vertical") * ms;
		float horizontalSpeed = Input.GetAxis ("Horizontal") * ms;


		Vector3 speed = new Vector3 (horizontalSpeed, 0, forwardSpeed);


		speed = transform.rotation * speed;

		CharacterController cc = GetComponent<CharacterController> ();

		//execution
		cc.SimpleMove (speed);

	}
}
