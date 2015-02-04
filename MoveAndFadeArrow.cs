using UnityEngine;
using System.Collections;

public class MoveAndFadeArrow : MonoBehaviour {

	// Use this for initialization
	public float fadeSpeed = 0.1f;
	public float moveSpeed = 1.0f;

	bool forward = true;

	void Start () {
		Color color = renderer.material.color;
		color.a=0;
		renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		Color color = renderer.material.color;
		if(forward)
			color.a = color.a + fadeSpeed * Time.deltaTime;
		else
			color.a = color.a - fadeSpeed * Time.deltaTime;

		if(color.a>=1)
			forward = false;

		if(color.a<=0)
			color.a=0;
		renderer.material.color = color;

		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);
			
	}
}
