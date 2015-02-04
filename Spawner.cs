using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public string prefabName = "Troll";
	float countdown = 0;
	public float countdownReset = 10.0f;
	public float range = 20.0f;
	public float spawnLimit = 5.0f;

	// Use this for initialization
	void Start () {
		countdown = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		countdown-=Time.deltaTime;
		if(countdown<=0)
		{
			SpawnMob();
		}
	}

	void SpawnMob()
	{
		countdown = countdownReset;
		float distance = Random.Range (-range, range);


		Vector3 origin = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
		Vector3 targ = new Vector3(transform.position.x, transform.position.y,  (transform.position.z + distance));
		
		RaycastHit hit;
		bool go = gameObject.transform.childCount<=spawnLimit;
		if(go)
		{
			if(Physics.Raycast (origin, (targ - origin).normalized, out hit, Mathf.Infinity, ~(1<<10)))
			{
				GameObject troll = (GameObject)Instantiate (Resources.Load ("Prefabs/Troll Updated"), hit.point, Quaternion.identity);
				//transform.position = hit.point;;
				
			}
		}

	}


}
