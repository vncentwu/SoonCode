using UnityEngine;
using System.Collections;

public class LightningStrikeScript : MonoBehaviour {

	bool expended = false;
	public float damage = 30.0f;
	public float knock_distance = 10.0f;
	public float knock_duration = 0.5f;
	public Vector3 originPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!expended)
		{
			transform.parent.gameObject.GetComponent<TrollController>().Hit (damage, PlatformerController.SINGLE_TARGET_MAGIC);
			KnockBack(transform.parent.gameObject);
			expended = true;
			Invoke ("DestroyLater", 5.0f);
		}
	}

	void DestroyLater()
	{
		Destroy (gameObject);
	}

	void KnockBack(GameObject targ)
	{
		int dir;
		if(originPosition.z > targ.transform.position.z)
			dir = -1;
		else
			dir = 1;
		targ.GetComponent<TrollController>().KnockBack (dir *knock_distance,  knock_duration);
		//Debug.Log ("KnockingBack");
	}

}
