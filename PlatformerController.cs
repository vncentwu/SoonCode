using UnityEngine;
using System.Collections;
using System;

public class PlatformerController : MonoBehaviour {

	public static PlatformerController pc; 
	public float ms = 5.0f;
	public float sidems = 2.0f;
	bool idle = false;
	bool walking = false;
	bool running = false;
	bool jumping = false;
	bool attacking = false;
	public static bool forward = false;
	public float gravity = 20.0F;
	CharacterController cc;
	public float jumpHeight = 8.0F;
	private bool isFalling = false;
	Vector3 speed;
	public float sprintSpeed = 2.5f;
	public float attackingMovementSlow = 0.25f;
	float attackingMovementSlowVar = 1.0f;
	private float sprintSpeedVar = 1;
	GameObject closestEnemy;
	public float checkDistance = 15.0f;
	public float jumpDelay = 0.2f;
	public GameObject weapon;
	public float AttackingTime = 1.3f;
	GameObject grounder;
	public bool justSpawned = true;
	GameObject spawnParticle;
	GameObject fire;
	bool onfire;
	bool drowned;
	public ShieldScript shield;
	public static float maxHealth = 100.0f;
	public static float currentHealth = 100.0f;
	public static float maxMana = 100.0f;
	public static float currentMana = 100.0f;
	public float fireDamage = 5.0f;
	public float attackCost = 15.0f;
	public float manaRegenRate = 0.5f;
	public float healthRegenRate = 0.1f;
	public float maxManaRegenRate = 0.5f;
	public float maxHealthRegenRate = 0.1f;
	public float teleportDistance = 5.0f;
	public GameObject mouseTarget;
	public Vector3 mousePosition = Vector3.zero;
	bool teleporting;
	public static bool mute;

	public float fireballCost = 5.0f;
	public float lightningStrikeCost = 5.0f;
	public float teleportCost = 5.0f;
	public float fireBlastCost = 10.0f;
	public float iceBlastCost = 10.0f;
	public float healCost = 20.0f;

	public float mouseTargetCountdown = 3.0f;
	float mouseTargetCountdownVar = 0;
	//bool colliding;
	public GameObject mouseTargetFree;
	public float damageDealt = 0;
	public static float currentExp = 0;
	public static float level = 1;
	public static float combo = 0;
	public static float maxCombo = 5;
	public static bool canCombo;
	public static float comboCountdown = 5.0f;
	public static float comboCountdownVar = 5.0f;
	public static float latestDamage = 0;
	public static float biggestHit = 0;
	public static float money = 0;
	public GameObject spawnPoint;
	public bool pulsing;

	public static bool godmode = false;
	public static bool oneHit = false;
	public static bool previousDamage;
	public static bool mode_3d;
	public static bool mode_mana;
	public static bool paused = false;
	public static bool killEverything = false;
	public static int enemiesKilledByCommand = 0;

	float distToGround;

	public static int NORMAL_ATTACK = 0;
	public static int SINGLE_TARGET_MAGIC = 1;
	public static int AOE_MAGIC = 2;
	public static int DOT_MAGIC = 3;
	public static int AOE_DOT_MAGIC = 4;

	Vector3 grav = Vector3.zero;
	// Use this for initialization
	void Start () {
		idle = true;
		animation.Play ("idle");
		forward = true;
		animation.Stop ();
		cc = GetComponent<CharacterController> ();
		speed = new Vector3 (0, 0, 0);
		grounder = transform.Find ("GroundChecker").gameObject;
		distToGround = collider.bounds.extents.y;
		//Debug.Log ("distance" +distToGround);
		if(justSpawned)
		{
			CreateSpawnParticle();
		}
		pc = gameObject.GetComponent<PlatformerController>();
		Physics.IgnoreLayerCollision(8, 12);
		shield = transform.Find ("Shield").gameObject.GetComponent<ShieldScript>();
		InvokeRepeating ("CheckBroadcasts", 0, 0.25f);

	}

	bool IsDead()
	{
		if(currentHealth<=0)
			return true;
		else
			return false;
	}

	void CheckBroadcasts()
	{
		if(ConversationScript.FindAndRemoveBroadcast("test"))
			currentHealth = maxHealth/2.0f;
	}

	void CreateSpawnParticle()
	{
		//if (spawnParticle!=null && spawnParticle.GetComponent<ParticleSystem>().loop)
			//return;
		//Debug.Log ("PARTICLES");
		//GameObject instance = (GameObject) Instantiate (Resources.Load ("Prefabs/SpawnParticles", typeof(GameObject)));
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(-90, 0, 0);
		spawnParticle = (GameObject)Instantiate (Resources.Load ("Prefabs/SpawnParticles", typeof(GameObject)), transform.position, rotation);
		spawnParticle.GetComponent<ParticleSystem>().time = 2.0f;
		spawnParticle.transform.Find ("Ray").gameObject.GetComponent<ParticleSystem>().Simulate(1.0f);
		spawnParticle.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().Simulate (1.0f);
		spawnParticle.transform.Find ("Ring").gameObject.GetComponent<ParticleSystem>().Simulate (1.0f);
		spawnParticle.transform.Find ("Ray").gameObject.GetComponent<ParticleSystem>().Play ();
		spawnParticle.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().Play ();
		spawnParticle.transform.Find ("Ring").gameObject.GetComponent<ParticleSystem>().Play ();

		Invoke ("StopSpawnParticles", 3.0f);
	}
	void Reverse()
	{
		//Debug.Log ("Rotating");
		//transform.Rotate (0, 180, 0);
	}


	void ResumeAnimations()
	{
		if (idle)
						animation.Play ("idle");
		if (walking)
						animation.Play ("Walk");
		if (running)
						animation.Play ("Run");

	}

	void SetAnimationLayers()
	{
		animation.wrapMode = WrapMode.Loop;
		animation ["Attack"].wrapMode = WrapMode.Once;
		animation ["Attack"].layer = 2;
		animation ["Jump"].wrapMode = WrapMode.Once;
		animation ["Jump"].layer = 1;



	}

	// Update is called once per frame
	void Update () {

		//Debug.Log ("damage"+damageDealt);

		if (Input.GetKeyDown (KeyCode.F5)) {
			Application.LoadLevel ("platformer");	
		}
		if (Input.GetKeyDown (KeyCode.F6)) {
			Hit (100.0f);	
			CheckDeath ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Fireball ();	
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			LightningStrike ();	
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			FireBlast ();	
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			IceBlast();
		}
		if(Input.GetKeyDown (KeyCode.Alpha5))
		{
			Heal ();
		}
		if(Input.GetKeyDown (KeyCode.Alpha6))
		{
			Shield ();
		}
		if(Input.GetKeyDown (KeyCode.Q))
		{
			Teleport ();
		}
		if(Input.GetKeyDown (KeyCode.F1))
		{
			mute = !mute;
		}
		if(Input.GetKeyDown (KeyCode.I))
	 	{
			ShowStats.addMessage("Inventory is not yet finished.");
		}

		UpdateHealth ();
		CheckIfDrowned ();
		CheckDeath ();
		CheckCombo ();
		CheckMute();
		LowHealthEffect ();
		CheckKillEverything();
		if (onfire && !godmode)
			currentHealth -= fireDamage*Time.deltaTime;
		//Debug.Log ("Health: " + currentHealth);

		GameObject tempClosest = FindClosestEnemy ();
		if((tempClosest==null && closestEnemy!=null))
			closestEnemy.GetComponent<HealthBar>().show=false;
		if(tempClosest!=null && !tempClosest.Equals (closestEnemy))
		   {
			if(closestEnemy!=null)
			{
				closestEnemy.GetComponent<HealthBar>().show = false;
				Debug.Log (closestEnemy.name);
			}
				

		}
		closestEnemy = tempClosest;

		
		InteractClosest ();

		SetAnimationLayers ();
		float forwardSpeed = Input.GetAxis("Horizontal") * ms * sprintSpeedVar * attackingMovementSlowVar;
		float sidewaySpeed = Input.GetAxis("Vertical") * sidems * sprintSpeedVar * attackingMovementSlowVar;
		if(!mode_3d)
			sidewaySpeed = 0;

		if(IsDead ())
			forwardSpeed = 0;
		AddMana (Mathf.Abs (forwardSpeed)/50.0f);
		if (forwardSpeed != 0)
			justSpawned = false;
		if (!justSpawned) {

			StopSpawnParticles();
		}

		if(forwardSpeed ==0 && sidewaySpeed!=0)
		{
			if(!walking)
			{

				//Debug.Log ("sideway");
				animation.CrossFade ("Walk");
				audio.Play ();
			}
			walking = true;
			
		}
		else if (forwardSpeed > 0 || sidewaySpeed !=0) {
						if(!walking){
							
							animation.CrossFade ("Walk");
							audio.Play ();
						}
						walking = true;
						if(!forward)
						{
							Reverse();
							forward = true;
						}
						
		} else if (forwardSpeed < 0 ) {
						if(!walking){
							
							animation.CrossFade ("Walk");
							audio.Play ();
						}
						walking = true;
						if(forward)
						{
							Reverse();
							forward = false;
						}
						
				}
					else {
						walking = false;
						idle = true;
						audio.Stop ();
				}

		if (Input.GetKey (KeyCode.LeftShift) && !idle) {
			sprintSpeedVar = sprintSpeed;

				animation.CrossFade ("Run");
			running = true;

		} else {
			sprintSpeedVar = 1;
			running = false;
			if(walking)
				animation.CrossFade ("Walk");
			else if(idle)
				animation.CrossFade ("idle");
			if(attacking)
				animation.CrossFade("Attack");
		}



		if (Input.GetButtonUp ("Fire1")  && !ShowStats.mouseConsumed) {
			Attack();
		}

		if (!walking && !running && !jumping && !attacking) {
						if(!idle){
						
						animation.CrossFade ("idle");
						}	
						idle = true;
				} else {
						idle = false;
				}

		speed.z = forwardSpeed;
		speed.x = -sidewaySpeed;
		//speed = transform.rotation * speed;
		 
		if (Input.GetButton ("Jump") && !isFalling && !IsDead ()) {

			//animation.Stop ("Run");
			//animation.Stop ("idle");
			//animation.Stop ("Walk");
			Invoke ("Jump", jumpDelay);
			isFalling = true;
			jumping = true;
			animation.CrossFade ("Jump");
			//Invoke ("ResumeAnimations", 1.0f);
			//Debug/.Log ("jumpcalled");
			//QAnimation ();
		}
			speed.y = speed.y - (gravity * Time.deltaTime);
		//execution
		cc.Move (speed * Time.deltaTime);

		if(forwardSpeed>0 && sidewaySpeed >0)
			transform.localEulerAngles = new Vector3(0, -45, 0);
		else if(forwardSpeed>0 && sidewaySpeed <0)
			transform.localEulerAngles = new Vector3(0, 45, 0);
		else if(forwardSpeed<0 && sidewaySpeed >0)
			transform.localEulerAngles = new Vector3(0, 225, 0);//180 and -90=270 = 225
		else if(forwardSpeed<0 && sidewaySpeed <0)
			transform.localEulerAngles = new Vector3(0, 135, 0);
		else if(forwardSpeed>0)
			transform.localEulerAngles = new Vector3(0, 0, 0);
		else if(forwardSpeed<0)
			transform.localEulerAngles = new Vector3(0, 180, 0);
		else if(sidewaySpeed>0)
			transform.localEulerAngles = new Vector3(0, -90, 0);
		else if(sidewaySpeed<0)
			transform.localEulerAngles = new Vector3(0, 90, 0);

		//Debug.Log (speed);
		if(!mode_3d)
			transform.position = new Vector3 (1052.083f, transform.position.y, transform.position.z);
		CheckIfGrounded ();
	}

	void Jump()
	{
		speed.y = jumpHeight;
		isFalling = true;
		jumping = false;
	}

	void Attack()
	{
		animation.CrossFade("Attack");
		//Debug.Log ("fire!");
		
		attacking = true;
		attackingMovementSlowVar = attackingMovementSlow;
		//currentMana -= attackCost;
		if(weapon!=null){
			
			WeaponController weap = weapon.GetComponent<WeaponController>();
			weap.Attack ();
		}
		Invoke ("stopAttack", AttackingTime);
	}

	void Teleport()
	{
		//teleporting = true;
		//transform.Translate (new Vector3(0, 0.2f, teleportDistance));
		//Invoke ("OffTeleport", 0.2f);
		//Debug.Log ("teleported");
		int dir;
		if(forward)
			dir = 1;
		else
			dir = -1;

		//Debug.Log ("teleport");

		Vector3 origin = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
		Vector3 targ = new Vector3(transform.position.x, transform.position.y,  (transform.position.z + dir*teleportDistance));

		RaycastHit hit;
		//Debug.Log("angle"+ (targ - origin).normalized);
		bool go = UseMana(teleportCost);
		if(go)
		{
			if(Physics.Raycast (origin, (targ - origin).normalized, out hit, Mathf.Infinity, ~(1<<10)))
			{
				ShowTeleportParticles();
				//Debug.Log ("played position: " + transform.position + "vs target: " + hit.point);
				transform.position = hit.point;
				//Debug.Log (hit.collider.gameObject.name);
				Invoke ("ShowTeleportParticles", 0.05f);
				
			}
		}
	}



	void NudgeTeleport()
	{
		transform.Translate (new Vector3(0, 0.2f, 0));
	}

	void OffTeleport()
	{
		teleporting = false;
	}


	void QAnimation()
	{
		if (walking)
						animation.PlayQueued ("Walk");
		if (idle)
						animation.PlayQueued ("idle");
		if (running)
						animation.PlayQueued ("Run");

	}

	void ShowTeleportParticles()
	{
		GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/ShortTeleport", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
		Destroy (nop, 5.0f);
	}

	void OnControllerColliderHit(ControllerColliderHit ColliderHit)
	{
		if(teleporting)
		{
			//Debug.Log ("telepaht" + ColliderHit.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.y);
			//Debug.Log ("name" +  ColliderHit.gameObject.name);
			transform.Translate (0, ColliderHit.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.y, 0);

		}
			
		//Debug.Log (ColliderHit.gameObject.name);
		//if (ColliderHit.point.y <= transform.position.y && !jumping) {
			//isFalling = false;
			////Debug.Log ("Us: " + transform.position.y + " Them: " + ColliderHit.collider.gameObject.transform.position.y);
			//speed.y = 0;
		//}


		//Debug.Log ("meow?!");
	}

	void CheckCombo()
	{
		comboCountdownVar-=Time.deltaTime;
		if(!(comboCountdownVar>=0))
			combo-=Time.deltaTime/3.0f;

		if(combo<=0)
			combo = 0;

		if(combo>=maxCombo)
		{
			canCombo =true;
		}
		else
			canCombo = false;
	}

	public void AddCombo()
	{
		if(combo<maxCombo)
			combo++;
		if(combo>=maxCombo)
		{
			combo = maxCombo;
			canCombo =true;
			comboCountdownVar = comboCountdown;
		}
	}

	void CheckIfGrounded()
	{
		if (cc.isGrounded && !jumping) {
			isFalling = false;
			//Debug.Log ("Us: " + transform.position.y + " Them: " + ColliderHit.collider.gameObject.transform.position.y);
			speed.y = 0;
		}
		
		

	}	



	void OnTriggerEnter(Collider other)
	{
				//Debug.Log ("WOW");
	}
	public bool isAttacking()
	{
		return attacking;
	}

	void DeathLightning()
	{

		//GameObject instance = (GameObject) Instantiate (Resources.Load ("Prefabs/DeathThunder", typeof(GameObject)));
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(270, 0, 0);
		
		GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/DeathThunder", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y, transform.position.z), rotation);
		//fire.transform.parent = gameObject.transform;
		//Debug.Log("LIGHTNING PLS");
		ToggleVisibilityOff();
		//transform.position = new Vector3 (1052.083f, -1000, transform.position.z);
	}

	void FireBlast()
	{
		bool go = UseMana (fireBlastCost);
		if(go)
		{
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(-90, 0, 0);
				GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/FireBlastAttack", typeof(GameObject)), mousePosition, rotation);
		}
	}

	void IceBlast()
	{
		bool go = UseMana (iceBlastCost);
		if(go)
		{
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(-90, 0, 0);
			GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/IceBlastAttack", typeof(GameObject)), new Vector3(mousePosition.x, mousePosition.y + 5.0f, mousePosition.z), rotation);
		}
	}



	void Fireball()
	{
		bool go = UseMana(fireballCost);
		if(go)
		{
			Quaternion rotation = Quaternion.identity;
			if(forward)
				rotation.eulerAngles = new Vector3(0, 0, 0);
			else
				rotation.eulerAngles = new Vector3(0, 180, 0);	
			float mult;
			if(forward)
				mult = 0.5f;
			else
				mult = -0.5f;
			GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/FireballAttack", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y + 2.0f*cc.height/3.0f, transform.position.z + mult), rotation);
		}
	//Debug.Log("FIREBALL PLS");
	}

	void LightningStrike()
	{
		//Debug.Log (mouseTarget);
		if(mouseTarget!=null)
		{
			bool go = UseMana (lightningStrikeCost);
			if(go)
			{
				GameObject strike = (GameObject)Instantiate (Resources.Load ("Prefabs/LightningStrikeAttack", typeof(GameObject)),
				                    	new Vector3(mouseTarget.transform.position.x, mouseTarget.transform.position.y + 20.0f*mouseTarget.GetComponent<TrollController>().cc.height/3.0f, mouseTarget.transform.position.z), Quaternion.identity);	
				strike.transform.parent = mouseTarget.transform;
				strike.GetComponent<LightningStrikeScript>().originPosition = transform.position;
				//Debug.Log (strike.transform.position);
			
			}
		}

	}

	void Heal()
	{
		bool go = UseMana (healCost);
		if(go)
		{
			GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/Heal", typeof(GameObject)), transform.position, Quaternion.identity);
			nop.transform.parent = gameObject.transform;
			currentHealth += nop.GetComponent<HealScript>().healAmount;
			if(currentHealth>maxHealth)
				currentHealth = maxHealth;
		}


	}



	void CheckDeath()
	{
		if (currentHealth <= 0) {
			if(currentHealth != -3.14f)
			{
				healthRegenRate = 0;
				manaRegenRate = 0;
				//Debug.Log (currentHealth);
				Invoke ("Respawn", 3.0f);
				DeathLightning();
			}
			currentHealth = -3.14f;

			FadeToGrayscale();
		}

			
	}

	void Respawn()
	{
		transform.position = spawnPoint.transform.position;
		currentHealth = maxHealth;
		currentMana = maxMana;
		justSpawned = true;
		RemoveGrayscale ();
		CreateSpawnParticle ();
		ResetOverlay();
		//Debug.Log ("Respawn");
		drowned = false;
		healthRegenRate = maxHealthRegenRate;
		manaRegenRate = maxManaRegenRate;
		ToggleVisibilityOn();
	}

	public bool UseMana(float cost)
	{
		if(mode_mana)
			return true;
		else if(currentMana-cost >=0)
		{
			currentMana-=cost;
			return true;
		}
		else
			return false;

	}


	public void stopAttack()
	{
		attacking = false;
		attackingMovementSlowVar = 1;
	}

	public void CheckIfDrowned()
	{
		if (transform.position.y < 100 && !drowned && !godmode) {
			currentHealth = 0;
			drowned = true;
		}
	}

	void ToggleVisibilityOn()
	{
		Renderer[] renderers  = gameObject.GetComponentsInChildren<Renderer> ();
		foreach(Renderer x in renderers)
		{
			x.enabled = true;
		}
	}

	void ToggleVisibilityOff()
	{
		Renderer[] renderers  = gameObject.GetComponentsInChildren<Renderer> ();
		foreach(Renderer x in renderers)
		{
			x.enabled = false;
		}
	}

	void UpdateHealth()
	{
		if (currentMana > 0)
			currentMana -= manaRegenRate * Time.deltaTime;
		if (currentHealth < maxHealth)
			currentHealth += healthRegenRate * Time.deltaTime;


		HealthBar[] bars = GetComponents<HealthBar> ();

		bars[0].currentHealth = currentHealth;
		bars[0].maxHealth = maxHealth;
		//GetComponent<HealthBar> ().currentHealth = currentHealth;
		//GetComponent<HealthBar> ().maxHealth = maxHealth;
		bars[1].currentHealth = currentMana;
		bars[1].maxHealth = maxMana;
		//GetComponent<HealthBar> ().currentHealth = currentHealth;
		//GetComponent<HealthBar> ().maxHealth = maxHealth;
	}

	//returns true if it was nullified
	public bool NullifyMouseTarget()
	{
		bool countingDown = mouseTargetCountdownVar >=0; 
		if(!countingDown)
			mouseTarget = null;
		return !countingDown;

	}

	void InteractClosest()
	{
		if(mouseTarget!=null)
			mouseTarget.GetComponent<HealthBar> ().show = true;
		else if (closestEnemy != null)
			closestEnemy.GetComponent<HealthBar> ().show = true;
	}	

	public void SetOnFire()
	{
		//Debug.Log ("I'M ON FIREEEEE");

		onfire = true;
		if (fire != null)
			StartCoroutine (ExtinguishFireNow (fire));
		GameObject instance = (GameObject) Instantiate (Resources.Load ("Prefabs/PlayerFire", typeof(GameObject)));
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0, 0, 0);

		fire = (GameObject)Instantiate (instance, new Vector3(transform.position.x + 0.129f, transform.position.y + 1.175f, transform.position.z), Quaternion.identity);
		fire.transform.parent = gameObject.transform;
		Invoke ("ExtinguishFire", 3.0f);
		//spawnParticle.GetComponent<ParticleSystem>().time = 2.0f;
	}

	IEnumerator ExtinguishFireNow(GameObject fir)
	{
		yield return new WaitForSeconds (1.0f);
		fir.GetComponent<ParticleSystem>().loop = false;
		fir.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().loop = false;
		fir.transform.Find ("Glow").gameObject.GetComponent<ParticleSystem>().loop = false;
		fir.transform.Find ("Smoke").gameObject.GetComponent<ParticleSystem>().loop = false;
		
		fir.GetComponent<ParticleSystem>().playbackSpeed = 2.4f;
		fir.transform.Find ("Smoke").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.4f;
		fir.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.3f;
		fir.transform.Find ("Glow").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.2f;
	}

	void Shield()
	{
		if(shield!=null)
			shield.GetComponent<ShieldScript>().ApplyBasicShield();
	}

	void ExtinguishFire()
	{
		if (fire != null) {
			fire.GetComponent<ParticleSystem>().loop = false;
			fire.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().loop = false;
			fire.transform.Find ("Glow").gameObject.GetComponent<ParticleSystem>().loop = false;
			fire.transform.Find ("Smoke").gameObject.GetComponent<ParticleSystem>().loop = false;
			
			fire.GetComponent<ParticleSystem>().playbackSpeed = 2.4f;
			fire.transform.Find ("Smoke").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.4f;
			fire.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.3f;
			fire.transform.Find ("Glow").gameObject.GetComponent<ParticleSystem>().playbackSpeed = 2.2f;
			onfire = false;
			fire = null;
		}

	}

	void SetGrayscale()
	{
		Camera.main.gameObject.GetComponent<GrayscaleEffect> ().effectAmount = 1.0f;
	}

	void RemoveGrayscale()
	{
		Camera.main.gameObject.GetComponent<GrayscaleEffect> ().effectAmount = 0f;
	}
	void FadeToGrayscale()
	{
		if (Camera.main.gameObject.GetComponent<GrayscaleEffect> ().effectAmount < 1.0f) {
			Camera.main.gameObject.GetComponent<GrayscaleEffect> ().effectAmount += .5F * Time.deltaTime;
			//FadeToGrayscale();

		}
			
	}

	public void GainMoney(float amount)
	{
		money +=amount;
		ShowStats.addMessage ("Picked up " + amount + " coins.");
	}

	public void Hit(float dmg)
	{
		if(godmode)
			return;
		if(shield!=null)
		{
			if(shield.shieldHealth >= 0)
			{
				dmg = shield.Hit (dmg);
			}
		}

		if(!(currentHealth<=0))
			currentHealth = currentHealth - dmg;
		CheckDeath();


	}

	float intensityTarget =  2.5f;
	float intensityStart = 0.0f;
	float intensity = 0;
	float intensitySpeed = 0.3f;
	float intensityTime = 0;
	bool intensityForward = true;
	void LowHealthEffect()
	{
		if(currentHealth/maxHealth < 0.2f)
			pulsing = true;
		else if(intensity<=intensityStart)
			pulsing = false;

		intensitySpeed = (1/(currentHealth/maxHealth))/10.0f;
		if(intensitySpeed>=2.0f)
			intensitySpeed = 2.0f;
		if(currentHealth/maxHealth > 0.2f)
			intensitySpeed = 10.0f;

		if(pulsing)
		{
			if(intensityForward)
			{
				intensityTime +=intensitySpeed* Time.deltaTime;
				intensity = Mathf.Lerp (intensityStart, intensityTarget, intensityTime);
				Camera.main.gameObject.GetComponent<ScreenOverlayC>().intensity = intensity;
				if(intensity>=intensityTarget)
				{
					intensityForward = false;
					intensityTime = 0;
				}
					
			}
			else
			{
				intensityTime +=intensitySpeed* Time.deltaTime;
				intensity = Mathf.Lerp (intensityTarget, intensityStart, intensityTime);
				Camera.main.gameObject.GetComponent<ScreenOverlayC>().intensity = intensity;
				if(intensity<=intensityStart)
				{
					intensityForward = true;
					intensityTime = 0;
				}
					
			}
		}			
		else
			Camera.main.gameObject.GetComponent<ScreenOverlayC>().intensity = 0;

	}

	void ResetOverlay()
	{
		Camera.main.gameObject.GetComponent<ScreenOverlayC>().intensity = 0;
		intensity = 0;
		intensityTime = 0;
	}
	void StopSpawnParticles()
	{

		spawnParticle.GetComponent<ParticleSystem> ().loop = false;
		spawnParticle.transform.Find ("Ray").gameObject.GetComponent<ParticleSystem> ().loop = false;
		spawnParticle.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem> ().loop = false;
		spawnParticle.transform.Find ("Ring").gameObject.GetComponent<ParticleSystem> ().loop = false;
		spawnParticle.transform.Find ("Ray").gameObject.GetComponent<ParticleSystem> ().playbackSpeed = 2.4f;
		spawnParticle.transform.Find ("Sparkles").gameObject.GetComponent<ParticleSystem> ().playbackSpeed = 2.3f;
		spawnParticle.transform.Find ("Ring").gameObject.GetComponent<ParticleSystem> ().playbackSpeed = 2.2f;
		//spawnParticle = null;
	}

	public void AddMana(float amount)
	{
		currentMana+=amount;
			if(currentMana>maxMana)
			currentMana = maxMana;
	}

	public void CheckMute()
	{
		if(mute)
			Mute ();
		else
			Unmute ();
	}

	public void Mute()
	{
		AudioListener.pause = true;
		MainMenuScript.volumeSliderValue = 0;
		AudioListener.volume = MainMenuScript.volumeSliderValue;
	}

	public void Unmute()
	{
		AudioListener.pause = false;
		AudioListener.volume = MainMenuScript.volumeSliderValue;
	}





	public void ReportInflictedDamage(float dmg)
	{
		damageDealt+=dmg;
		AddMana (dmg/7.0f);
		AddCombo ();

		latestDamage = dmg;
		if(dmg>biggestHit)
			biggestHit =dmg;

	}


	public void ReportInflictedDamage(float dmg, int type)
	{
		damageDealt+=dmg;
		if(type == NORMAL_ATTACK)
		{
			AddMana (dmg/3.0f);
			AddCombo ();
		}


		latestDamage = dmg;
		if(dmg>biggestHit)
			biggestHit =dmg;
		
	}

	void CheckKillEverything()
	{
		if(killEverything)
			KillEverything();
		killEverything = false;
	}
	public void KillEverything()
	{
		GameObject[] mons = GameObject.FindGameObjectsWithTag("Enemy");
		enemiesKilledByCommand = mons.Length;
		foreach (GameObject x in mons)
		{
			x.GetComponent<TrollController>().Die ();
		}

	}

	public void AddExp(float amount)
	{
		currentExp+=amount;
		ShowStats.addMessage ("You have gained " + amount + " exp");
		float expTop = 100.0f * Mathf.Pow (1.2f, level);
		while(currentExp>=expTop)
		{
			level++;
			currentExp-=expTop;
			ShowStats.addMessage ("LEVEL UP: Lv " + level);
			LevelUp ();
			//Debug.Log ("LEVELUP:" + level);
		}
	}

	public void LevelUp()
	{
		GameObject nop = (GameObject)Instantiate (Resources.Load ("Prefabs/LevelUp", typeof(GameObject)), new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
		nop.transform.parent = gameObject.transform;
		Destroy (nop, 3.0f);
	}

	public void ReportKill(GameObject other)
	{
		ShowStats.addMessage ("You have killed a " + other.name + ".");
		AddExp (other.GetComponent<TrollController>().expToGive);
	}


		public GameObject FindClosestEnemy()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closest = null;
		float minDistance = 1000.0f;
		foreach (GameObject x in objects) 
		{
			float dist = Vector3.Distance (transform.position,x.transform.position);
			if(dist<minDistance){
				minDistance = dist;
				closest = x;
			}
		}
		if (minDistance > checkDistance)
						return null;
				else
						return closest;

	}



}
