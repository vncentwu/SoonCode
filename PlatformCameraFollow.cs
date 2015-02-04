using UnityEngine;
using System.Collections;
public class PlatformCameraFollow : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	// Update is called once per frame



	void Update ()
	{
		if (target)
		{
			//problem occurs that when increasing in altitude, the camera zooms in. This is likely because the 
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));

			//delta of vector(0.5f, 0.5f, point.z) finds the difference between the target and a position in the middle of the screen, with a z position

			delta = new Vector3(0, delta.y, delta.z);
			Vector3 destination = transform.position + delta;
			//Debug.Log ("delta" + delta);
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

		float dist = Input.GetAxis ("Mouse ScrollWheel");
		transform.Translate (0, 0, 4 * dist);

	}
}