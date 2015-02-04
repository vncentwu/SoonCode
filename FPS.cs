using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

	public GameObject bulletPrefab;
	public GameObject dummy;
	public GameObject flasher;
	public GameObject gun;
	public GameObject gun2;
	public float recoil = 20.0F;
	public bool shooting = false;
	public AudioClip gunSound;
	public float gunSpeed = 6.0F;

	//Vector3 diff;

	float speed = 1000f;
	float time = 0;

	void FlashOff(){
		flasher.renderer.enabled = false;

	}

	void FlashOn(){
		audio.PlayOneShot (gunSound);
		flasher.transform.Rotate(0, Random.Range(0, 90), 0);
		flasher.renderer.enabled = true; // turn on muzzle flash
		Invoke ("FlashOff", 0.05f);
		Recoil ();

	}

	IEnumerator Shift(Vector3 trans)
	{
		yield return new WaitForSeconds (0.1f);
		gun2.transform.Translate (-1 * trans);
	}

	void Recoil(){
		Vector3 diff = (transform.position - gun.transform.position)/recoil;
		Debug.Log (diff);
		gun2.transform.Translate (diff);
		StartCoroutine (Shift (diff));

	}


	// Use this for initialization
	void Start () {
		flasher = (GameObject)GameObject.FindWithTag ("Flash");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
						
						if(!shooting){
							//gun2.audio.Play ();
						}
							shooting = true;
							
						if (time <= 0) {
								FlashOn ();
								time = gunSpeed;
								Camera cam = Camera.main;
								GameObject bullet = (GameObject)Instantiate (bulletPrefab, dummy.transform.position, dummy.transform.rotation);
								//GameObject bullet2 = (GameObject)Instantiate (bulletPrefab, dummy.transform.position, dummy.transform.rotation);
								bullet.rigidbody.AddForce (transform.forward * speed, ForceMode.Impulse);
								//bullet2.rigidbody.AddForce (transform.forward * (speed+20), ForceMode.Impulse);
								//flasher.renderer.enabled = true;
								
						}
		

				} else {
			if(shooting){
				//gun2.audio.Stop ();
			}


			shooting = false;
				
				}

			time = time - 1;
				

	}
}