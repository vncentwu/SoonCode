using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public GameObject fire;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Enemy") {
						col.gameObject.tag = "Dead";
						GameObject fiyah = (GameObject)Instantiate (fire, col.transform.position, Quaternion.identity);
						fiyah.transform.parent = col.transform;
						

				} 
		Destroy (gameObject);
	}
}


