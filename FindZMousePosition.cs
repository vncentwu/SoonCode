using UnityEngine;
using System.Collections;

public class FindZMousePosition : MonoBehaviour {

	GameObject shower;
	GameObject selector;
	GameObject finder;
	GameObject blue;
	GameObject white;
	PlatformerController player;
	bool onWhite = true;

	// Use this for initialization
	void Start () {
		finder = (GameObject)Instantiate (Resources.Load ("Prefabs/Finder", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

		//blue = (GameObject)Instantiate (Resources.Load ("Prefabs/AreaSelector", typeof(GameObject)), Vector3.zero, Quaternion.identity);
		shower = (GameObject)Instantiate (Resources.Load ("Prefabs/CongAreaSelector", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
		blue = shower.transform.FindChild("AreaSelector").gameObject;
		white = shower.transform.FindChild("AreaSelectorWhite").gameObject;
		blue.renderer.enabled = false;
		white.GetComponent<ParticleSystem>().Simulate (1.0f);
		white.GetComponent<ParticleSystem>().Play ();
		//shower = white;
		finder.transform.parent = shower.transform;

		player = GameObject.Find ("Player").GetComponent<PlatformerController>();

	}

	void StartWhite()
	{

		if(!onWhite)
		{
			//Debug.Log ("start whitoe");
			blue.renderer.enabled = false;
			white.renderer.enabled = true;
			onWhite = true;
		}

	}

	void StartBlue()
	{
		if(onWhite)
		{
			//Debug.Log ("start blue");
			blue.renderer.enabled = true;
			white.renderer.enabled = false;
			onWhite = false;
		}

	}
	// Update is called once per frame
	void Update () {
	

		Ray ray= Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		RaycastHit hit;
		if(Physics.Raycast (ray, out hit))
		{
			//Debug.Log ("fucker");
			if(!hit.collider.transform.tag.Equals("CubeAss"))
			{
				//Debug.Log ("fucker2");
				RaycastHit downHit;
				if(Physics.Raycast (new Vector3(1052.083f, hit.point.y, hit.point.z), Vector3.down, out downHit, Mathf.Infinity, ~(1<<10)))
				{
					//Debug.Log ("fucker3");
					shower.transform.position = new Vector3(1052.083f, downHit.point.y, hit.point.z);
				}
				else
				 	shower.transform.position = new Vector3(1052.083f, hit.point.y, hit.point.z);
			}


			//Debug.Log (hit.transform.gameObject.name + hit.point);

		}
		if(player.mouseTarget==null)
			StartWhite ();
		else
			StartBlue ();

		player.mousePosition = shower.transform.position;
	}
}
