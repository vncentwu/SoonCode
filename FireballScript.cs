using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {

	public float speed = 9.0f;
	public float damage = 50.0f;
	public float knock_distance = 10.0f;
	public float knock_duration = 0.5f;

	// Use this for initialization
	void Start () {
		//Debug.Log ("start");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(0,0, speed*Time.deltaTime));
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals ("Enemy"))
		{
			other.gameObject.GetComponent<TrollController>().Hit (damage, PlatformerController.SINGLE_TARGET_MAGIC);
			KnockBack (other.gameObject);
			Destroy (gameObject);
		}


	}

	void KnockBack(GameObject targ)
	{
		int dir;
		if(transform.position.z > targ.transform.position.z)
			dir = -1;
		else
			dir = 1;
		targ.GetComponent<TrollController>().KnockBack (dir *knock_distance,  knock_duration);
		//Debug.Log ("KnockingBack");
	}



}
