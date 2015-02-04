using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	public float amount;
	public GameObject player;

	// Use this for initialization
	void Start () {
	


	}

	public void SetAmount(float amt)
	{
		amount = amt;
		if(amount<=50)
			CreateCoin ();
		else
			CreateBar ();
	}

	void CreateCoin()
	{
		Vector3 pos = transform.position;
		pos.y = pos.y + 0.5f;
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0, 0, 0);
		GameObject money = (GameObject)Instantiate (Resources.Load ("Prefabs/Coin"),pos , rotation);
		money.transform.parent = gameObject.transform;
	}

	void CreateBar()
	{
		Vector3 pos = transform.position;
		pos.y = pos.y + 0.5f;
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(180.0f, 0, 0);
		GameObject money = (GameObject)Instantiate (Resources.Load ("Prefabs/GoldBar"),pos , rotation);
		money.transform.parent = gameObject.transform;
	}



	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown (KeyCode.Z))
			PickUp();

	}

	public void PickUp()
	{
		if(player!=null)
		{
			player.GetComponent<PlatformerController>().GainMoney(amount);
			Destroy (gameObject);
		}

			
	}

	bool IsPickable()
	{
		if(player!=null)
			return true;
		else
			return false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals ("Player"))
			player = other.gameObject;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag.Equals ("Player"))
			player = null;
	}
}
