using UnityEngine;
using System.Collections;

public class HorizontalFlashingRedArrow : MonoBehaviour {

	// Use this for initialization
	public static bool start;
	void Start () {

		InvokeRepeating("SpawnNewArrow", 1.0f, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {

			


	}



	void SpawnNewArrow()
	{
		if(start)
		{
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0, 0, -90);
			Instantiate (Resources.Load ("Prefabs/RedPath", typeof(GameObject)), transform.position, rotation);
		}

	}
}
