using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	GameObject full;
	GameObject three;
	GameObject half;
	GameObject one;
	GameObject hit;
	public float shieldHealth = 0;
	public float maxShieldHealth = 20.0f;


	bool full_on;
	bool three_on;
	bool half_on;
	bool one_on;

	void Start () {
		full = transform.Find ("Full").gameObject;
		three = transform.Find ("Three").gameObject;
		half = transform.Find ("Half").gameObject;
		one = transform.Find ("One").gameObject;
		hit = transform.Find ("HitShield").gameObject;

		full.GetComponent<ParticleSystem>().loop = false;
		three.GetComponent<ParticleSystem>().loop = false;
		half.GetComponent<ParticleSystem>().loop = false;
		one.GetComponent<ParticleSystem>().loop = false;
		hit.GetComponent<ParticleSystem>().loop = false;

		full.GetComponent<ParticleSystem>().Stop ();
		three.GetComponent<ParticleSystem>().Stop ();
		half.GetComponent<ParticleSystem>().Stop ();
		one.GetComponent<ParticleSystem>().Stop ();
		hit.GetComponent<ParticleSystem>().Stop ();

		full.GetComponent<ParticleSystem>().playbackSpeed = 12.3f;
		three.GetComponent<ParticleSystem>().playbackSpeed = 12.3f;
		half.GetComponent<ParticleSystem>().playbackSpeed = 12.3f;
		one.GetComponent<ParticleSystem>().playbackSpeed = 12.3f;
		hit.GetComponent<ParticleSystem>().playbackSpeed = 12.3f;
	}
	
	// Update is called once per frame
	void Update () {
		if(shieldHealth/maxShieldHealth >= 1)
		{
			if(!full_on)
			{
				full_on = true;
				ShieldOn (full);
			}
			three_on = false;
			half_on = false;
			one_on = false;
		}
		else if(shieldHealth/maxShieldHealth >= 0.75f)
		{
			full_on = false;
			if(!three_on)
			{
				three_on = true;
				ShieldOn (three);
			}
			half_on = false;
			one_on = false;
		}
		else if(shieldHealth/maxShieldHealth >= 0.5f)
		{
			if(!half_on)
			{
				half_on = true;
				ShieldOn (half);
			}
			full_on = false;
			three_on = false;
			one_on = false;
		}
		else if(shieldHealth/maxShieldHealth >= 0.25f)
		{

			full_on = false;
			three_on = false;
			half_on = false;
			if(!one_on)
			{
				one_on = true;
				ShieldOn (one);

			}

		}
		else
		{
			full_on = false;
			three_on = false;
			half_on = false;
			one_on = false;
		}
		if(!full_on)
			ShieldOff (full);
		if(!three_on)
			ShieldOff (three);
		if(!half_on)
			ShieldOff (half);
		if(!one_on)
			ShieldOff (one);


	}

	void ShieldOn(GameObject shield)
	{
		shield.GetComponent<ParticleSystem>().loop = true;
		shield.GetComponent<ParticleSystem>().Play ();
		
		shield.GetComponent<ParticleSystem>().playbackSpeed = 1.0f;

	}

	void BriefHitParticle()
	{
		GameObject shield = (GameObject)Instantiate (Resources.Load ("Prefabs/HitShield"), full.transform.position, Quaternion.identity);
		shield.transform.parent = transform;

		shield.GetComponent<ParticleSystem>().Play();
		shield.GetComponent<ParticleSystem>().playbackSpeed = 10.0f;
		//Destroy (shield, 5.0f);

	}

	void ShieldOff(GameObject shield)
	{
		shield.GetComponent<ParticleSystem>().loop = false;
		shield.GetComponent<ParticleSystem>().playbackSpeed = 4.0f;
	}

	public float Hit(float damage)
	{
		shieldHealth -= damage;
		BriefHitParticle ();
		if(shieldHealth<0)
			return Mathf.Abs (shieldHealth);
		else
			return 0;
	}

	public void ApplyBasicShield()
	{
		shieldHealth = maxShieldHealth;
	}



}
