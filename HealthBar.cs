using UnityEngine;
using System.Collections;
public class HealthBar : MonoBehaviour {
	//public float barDisplay; //current progress
	Vector2 pos = new Vector2(20,40);
	Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public Texture2D outline;
	public float currentHealth = 1;
	public float maxHealth = 1;
	public float borderWidth = 5;

	public float percOfScreenX = 2;
	public float percOfScreenY = 8;
	public float percOfScreenWidth = 4;
	public float percOfScreenHeight = 50;
	public bool show = false;

	public string name = "Unknown Enemy";
	void Start(){

	}

	void OnGUI() {

		if (show) {

						pos = new Vector2 (Screen.width / percOfScreenX - Screen.width / percOfScreenWidth / 2, Screen.height / percOfScreenY - Screen.height / percOfScreenHeight / 2);
						size = new Vector2 (Screen.width / percOfScreenWidth, Screen.height / percOfScreenHeight);

						int current =  (int)currentHealth;
						if(current<=0)
							current = 0;
						int max = (int)maxHealth;
						GUI.DrawTexture (new Rect (pos.x - borderWidth, pos.y - borderWidth, size.x + borderWidth * 2, size.y + borderWidth * 2), outline);
						GUI.DrawTexture (new Rect (pos.x, pos.y, size.x, size.y), emptyTex);
						GUI.DrawTexture (new Rect (pos.x, pos.y, size.x * (current / maxHealth), size.y), fullTex);
						GUI.Label (new Rect (pos.x + size.x / 2.4f, pos.y - size.y / 4.0f, size.x / 2, size.y * 2.3f), "(" + current + "/" + max + ")");
						GUI.Label (new Rect (pos.x, pos.y - size.y * 2.3f, size.x / 2, size.y * 2.3f), name);
				}
	}
	void Update() {

	}
}