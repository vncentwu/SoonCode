using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public float damage = 5.0f;
	public float damageReset = 5.0f;
	public float comboMultiplier = 0.2f;
	public float knock_distance = 15.0f;
	public float knock_duration = 0.5f;
	public GameObject targ;
	public GameObject player;
	public float multiplierCap = 20;
	GameObject fireSword;
	GameObject cylinder;
	bool comboPower;

	// Use this for initialization
	void Start () {
		//Debug.Log ("At least we started..?");
		player = GameObject.Find ("Player");
		cylinder = GameObject.Find ("Cylinder001");
	}
	
	// Update is called once per frame
	void Update () {

		if(PlatformerController.combo>0)
			damage = damageReset + damageReset * (PlatformerController.combo * comboMultiplier);
		else
			damage = damageReset;
	
		if(PlatformerController.combo >= PlatformerController.maxCombo)
		{
			if(fireSword==null)
			{
				if(PlatformerController.forward)
				{
					fireSword = (GameObject)Instantiate (Resources.Load ("Prefabs/FireSword", typeof(GameObject)), cylinder.transform.position, Quaternion.identity);
					fireSword.transform.parent = cylinder.transform;
				}
				else
				{
					Quaternion rotation = Quaternion.identity;
					rotation.eulerAngles = new Vector3(0, 180, 0);
					fireSword = (GameObject)Instantiate (Resources.Load ("Prefabs/FireSword", typeof(GameObject)), cylinder.transform.position, rotation);
					fireSword.transform.parent = cylinder.transform;
				}

			}
			comboPower = true;
				
		}
		else
		{
			if(fireSword!=null)
				Destroy (fireSword);
			fireSword=null;
			comboPower = false;

		}
		//if(damage>multiplierCap*damageReset)
			//damage = multiplierCap*damageReset;
	}

	public void Attack()
	{
		audio.Play ();
		if (targ != null) {
			//Debug.Log ("kind of attacking");
			if (targ.tag == "Enemy" && transform.parent.parent.gameObject.GetComponent <PlatformerController>().isAttacking()) {
				//Debug.Log ("kind of attacking2");
				StartCoroutine(DeferredDamage (targ));

			}

		}
		//else
			//Invoke ("DelaySound", transform.parent.parent.gameObject.GetComponent<PlatformerController> ().AttackingTime);
	}

	public void FireSword()
	{

	}

	public void DelaySound()
	{
		audio.Play ();
	}


		


	IEnumerator DeferredDamage(GameObject targ)
	{
		float dmg =  damage;
		if(comboPower)
			dmg*=5;
		yield return new WaitForSeconds (transform.parent.parent.gameObject.GetComponent<PlatformerController> ().AttackingTime/2.0f);
		StartCoroutine(DeferredDamage2 (targ));
		if(targ!=null)
		{
			targ.GetComponent <TrollController>().Hit(dmg/2.0f, PlatformerController.NORMAL_ATTACK);


		}
			
		//Debug.Log ("reached");
	}

	IEnumerator DeferredDamage2(GameObject targ)
	{
		yield return new WaitForSeconds (transform.parent.parent.gameObject.GetComponent<PlatformerController> ().AttackingTime/2.0f);

		float dmg =  damage;
		if(comboPower)
		{
			dmg*=5;
			comboPower = false;
			PlatformerController.combo = 0;
		}
		audio.Play ();
		if(targ!=null)
		{
			targ.GetComponent <TrollController>().Hit(dmg/2.0f, PlatformerController.NORMAL_ATTACK);
			KnockBack (targ);
		}


		
		//Debug.Log ("reached");
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
	
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("we're in!");
		if(other.tag.Equals ("Enemy"))
			targ = other.gameObject;
		//Debug.Log ("THIS");


	}
	void OnTriggerStay(Collider other)
	{
		//Debug.Log ("we're in!");
		//targ = other.gameObject;
		
		
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.Equals (targ))
			targ = null;
	}

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log ("we hit something..");
	}




}
