using UnityEngine;
using System.Collections;

public class FireBlastScript : MonoBehaviour {

	public float damage = 50.0f;
	public float knock_distance = 10.0f;
	public float knock_duration = 0.5f;
	public float life_duration = 6.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		life_duration -= Time.deltaTime;
		if(life_duration<=0)
			Invoke ("DestroyLater", 2.0f);

	}

	void DestroyLater()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{

		
		
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag.Equals ("Enemy") && life_duration<4.5f)
		{
			other.gameObject.GetComponent<TrollController>().Hit (damage * Time.deltaTime, PlatformerController.AOE_DOT_MAGIC);
			KnockBack (other.gameObject);
		}
	}

	void KnockBack(GameObject targ)
	{
		int dir;
		if(transform.position.z > targ.transform.position.z)
			dir = -1;
		else
			dir = 1;
		targ.GetComponent<TrollController>().KnockBack (dir *knock_distance * Time.deltaTime,  knock_duration*Time.deltaTime);
		//Debug.Log ("KnockingBack");
	}

}
