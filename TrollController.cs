using UnityEngine;
using System.Collections;

public class TrollController : MonoBehaviour {



	bool pacing;
	bool attacking;
	float stepsLeft;
	bool forward;
	public float speed = 5;
	float speedVar;
	Vector3 vel;
	public CharacterController cc;
	public float gravity = 20.0f;
	bool isFalling;
	public string name = "Troll";

	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public GameObject player;
	float semiTransparentValue = 0.60f;
	float opaqueValue = 1.0f;
	public bool sighted;

	public float sightDistance = 15.0f;
	public float sightHeightDistance = 5.0f;

	public float attackCooldown = 3.0f;
	public float attackCooldownVar = 0;
	public float expToGive = 50.0f;
	public float damage = 10.0f;
	public float attackRange = 3.0f;
	int dir = 1;
	public float knockBackVar = 0;
	public float knockRecoverRate = 1.0f;
	public float minMoneyDrop = 10;
	public float maxMoneyDrop = 100;
	public float healthRegenRate = 1;
	public bool selected = false;
	GameObject selecter;

	public bool entered;

	public string idleAnimation = "Idle_01";
	public string walkAnimation = "Walk";
	public string attack1Animation = "Attack_01";
	public string attack2Animation = "Attack_02";
	public string hitAnimation = "Hit";
	public string dieAnimation = "Die";
	public string meshName = "Troll";


	bool dead = false;
	bool stunned;

	// Use this for initialization
	void Start () {
		cc= GetComponent<CharacterController> ();
		forward = true;
		pacing = true;
		stepsLeft = Random.Range (10, 20);
		vel = new Vector3 (0, 0, 0);
		speedVar = speed;
		RefreshHealth ();
		animation [dieAnimation].layer = 2;
		animation [attack1Animation].layer = 1;
		animation [attack2Animation].layer = 1;
		animation [hitAnimation].layer = 1;
		player = GameObject.Find ("Player");
		Physics.IgnoreLayerCollision(12, 12);
		Physics.IgnoreLayerCollision(12, 13);
	}

	void CheckForPlayer()
	{
		float distanceZ = transform.position.z - player.transform.position.z;
		if (Mathf.Abs (transform.position.z - player.transform.position.z) < sightDistance && Mathf.Abs (transform.position.y - player.transform.position.y) < sightHeightDistance) {
						sighted = true;
				} else
					sighted = false;

		//Debug.Log ("sighted" + sighted);
	}

	// Update is called once per frame
	void Update () {

		GameObject other = transform.FindChild(meshName).gameObject;
		if(other!=null)
		{
			//Debug.Log (gameObject.name);
			Color textureColor = other.renderer.material.color;
			
			//Debug.Log ("entered? :" + entered);
			if(entered)
				textureColor.a = semiTransparentValue;
			else
				textureColor.a = opaqueValue;
			other.renderer.material.color = textureColor;
		}

		//Debug.Log ("lowered opactiy");

		Regen ();
		if(attackCooldownVar>0)
			attackCooldownVar-=Time.deltaTime;
		CheckForPlayer ();
		if(!sighted)
			AFKMove ();
		else
		{
			EngagePlayer();
		}
		
		transform.position = new Vector3 (1052.083f, transform.position.y, transform.position.z);
	}


	void Attack()
	{
		if(Mathf.Abs (transform.position.z - player.transform.position.z)<attackRange && Mathf.Abs(transform.position.y - player.transform.position.y)<attackRange && attackCooldownVar<=0)
		{
			animation[attack1Animation].wrapMode = WrapMode.Once;
			animation[attack2Animation].wrapMode = WrapMode.Once;
			float tester = Random.Range (10, 20);
			if(tester>=15)
			{
				Invoke ("NormalAttack", 0.5f);
				animation.CrossFade  (attack1Animation);
			}	
			else
			{
				animation.CrossFade  (attack2Animation);
				DoubleHit ();
			}
			attackCooldownVar = attackCooldown;
		}


	}

	void Regen()
	{
		health += healthRegenRate * Time.deltaTime;
		if(health>=maxHealth)
			health = maxHealth;
	}

	void NormalAttack()
	{
		if(!stunned)
			player.GetComponent<PlatformerController>().Hit(damage);
	}

	void DoubleHit()
	{
		if(!stunned)
		{
			player.GetComponent<PlatformerController>().Hit(damage/2);
			Invoke ("DoubleHit2", 1.0f);
		}

	}

	void DoubleHit2()
	{
		if(!stunned)
			player.GetComponent<PlatformerController>().Hit(damage/2);
	}
	void EngagePlayer()
	{
		if(Mathf.Abs (transform.position.z - player.transform.position.z)<attackRange && Mathf.Abs(transform.position.y - player.transform.position.y)<attackRange && attackCooldownVar<=0 && !stunned)
		{
			Attack ();
		}
		else if((transform.position.z - player.transform.position.z)>0)
		{
			dir = -1;
			if(forward == true){
				forward = false;
				Reverse ();
			}
		}
		else if((transform.position.z - player.transform.position.z)<0)
		{
			dir = 1;
			if(forward == false)
			{
				forward = true;
				Reverse ();
			}

		}
		int die;
		if(knockBackVar<0)
			die = -1;
		else
			die = 1;

		knockBackVar = (Mathf.Abs (knockBackVar)-knockRecoverRate*Time.deltaTime)*die;


		if(knockBackVar>0)
			vel = new Vector3(0, 0,  knockBackVar * Time.deltaTime);
		else
			vel = new Vector3(0, 0,  dir *speed * Time.deltaTime);
		vel.y = vel.y - gravity*Time.deltaTime;

		if(Mathf.Abs (transform.position.z - player.transform.position.z)>=attackRange*2)
			attackCooldownVar = 0;
		if(attackCooldownVar<=0)
			cc.Move (vel);
		animation.CrossFade  (walkAnimation);


		
	}

	public CharacterController GetCC()
	{
		return cc;
	}

	public void KnockBack(float dist, float duration)
	{
		knockBackVar = dist;
		Stun (duration/2.0f);
		Invoke ("StopKnockBack", duration);
	}

	public void StopKnockBack()
	{
		knockBackVar = 0;
	}
	void AFKMove()
	{
		if (pacing) {
			int dir;
			if(forward)
				dir = 1;
			else
				dir = -1;
			vel = new Vector3(0, 0,  dir *speedVar * Time.deltaTime);
			vel.y = vel.y - gravity*Time.deltaTime;
			cc.Move (vel);
			stepsLeft = stepsLeft - speedVar * Time.deltaTime;
			
			animation.CrossFade  (walkAnimation);
		}
		
		if(pacing && stepsLeft<=0)
		{
			speedVar = 0;
			Invoke ("MoveLater", 2.0f);

			pacing = false;
		}
	}

	public void OnSelectorEnter()
	{
		//Debug.Log ("entered");
		player.GetComponent<PlatformerController>().mouseTarget = gameObject;

		//GameObject instance = (GameObject) Instantiate (Resources.Load ("Prefabs/Selector", typeof(GameObject)));
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0, 0, 0);
		selecter = (GameObject)Instantiate (Resources.Load ("Prefabs/Selector", typeof(GameObject)), transform.position, rotation);
		selecter.GetComponent<ParticleSystem>().Simulate(2.0f);
		selecter.GetComponent<ParticleSystem>().Play ();
		//selecter.GetComponent<ParticleSystem>().time = 1.0f;
		selecter.transform.parent = gameObject.transform;
	}

	public void OnSelectorExit()
	{
		//Debug.Log ("exited");
		//bool destroyed = player.GetComponent<PlatformerController>().NullifyMouseTarget();
		player.GetComponent<PlatformerController>().mouseTarget = null;
		//if(destroyed)
			Destroy (selecter);

	}

	public void Stun(float duration)
	{
		stunned = true;
		Invoke ("Unstun", duration);
	}

	void Unstun()
	{
		stunned = false;
	}


	void MoveLater()
	{
		forward = !forward;
		stepsLeft = Random.Range (10,20);
		Reverse ();
		speedVar = speed;
		pacing = true;
	}
	void Reverse()
	{
		//Debug.Log ("Rotating");
		transform.Rotate (0, 180, 0);
	}
	void OnControllerColliderHit()
	{
		isFalling = false;
		vel.y = 0;
		//Debug.Log ("meow?!");
	}

	void RefreshHealth()
	{
		GetComponent<HealthBar> ().name = name;
		GetComponent<HealthBar> ().maxHealth = maxHealth;
		GetComponent<HealthBar> ().currentHealth = health;
	}

	public void Die()
	{
		if(dead)
			return;
		//GameObject instance = (GameObject) Instantiate (Resources.Load ("Prefabs/DeathBoom", typeof(GameObject)));
		dead= true;
		GameObject fire = (GameObject)Instantiate (Resources.Load ("Prefabs/DeathBoom", typeof(GameObject)), transform.position, Quaternion.identity);
		animation.CrossFade (dieAnimation);
		speedVar = 0;
		vel = new Vector3 (0, 0, 0);
		Invoke ("DestroyThis", 0.7f);
		Destroy (fire, 4.0f);
		PlatformerController.pc.ReportKill(gameObject);
		DropMoney ();
		//StartCoroutine (FadeFire (instance));
	}

	void DropMoney()
	{
		//Debug.Log ("Dropeed Money");
		GameObject money = (GameObject)Instantiate(Resources.Load ("Prefabs/Money", typeof(GameObject)), transform.position, Quaternion.identity);
		money.GetComponent<Money>().SetAmount((int)Random.Range (minMoneyDrop, maxMoneyDrop));
	}

	IEnumerator FadeFire(GameObject fire)
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (fire);
	}

	void DestroyThis()
	{
		Destroy (gameObject);
	}

	public void Hit(float damage)
	{
		health = health - damage;
		if(PlatformerController.oneHit)
			health = 0;
		PlatformerController.pc.ReportInflictedDamage(damage);
		animation[hitAnimation].wrapMode = WrapMode.Once;
		animation.CrossFade (hitAnimation);
		if (health <= 0) {
			Die ();
			health = 0;
		}
		RefreshHealth ();
	}
	public void Hit(float damage, int type)
	{
		health = health - damage;
		if(PlatformerController.oneHit)
			health = 0;
		PlatformerController.pc.ReportInflictedDamage(damage, type);
		animation[hitAnimation].wrapMode = WrapMode.Once;
		animation.CrossFade (hitAnimation);
		if (health <= 0) {
			Die ();
			health = 0;
		}
		RefreshHealth ();
		
	}
	public void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("TRIGGERED");
		//if(other.name.Equals ("Player"))
			//entered = true;

	}

	public void OnTriggerExit(Collider other)
	{
		//if(other.name.Equals ("Player"))
			//entered = false;
	}



}
