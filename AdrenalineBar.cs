using UnityEngine;
using System.Collections;
public class AdrenalineBar : MonoBehaviour {
	//public float barDisplay; //current progress
	Vector2 pos = new Vector2(20,40);
	Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public Texture2D comboTex;
	public Texture2D outline;
	public float currentHealth = 1;
	public float maxHealth = 1;
	public float borderWidth = 3;
	
	public float percOfScreenX = 2;
	public float percOfScreenY = 8;
	public float percOfScreenWidth = 4;
	public float percOfScreenHeight = 50;
	public bool show = false;

	float height = 100.0f;
	float width = 20.0f;

	public string name = "Unknown Enemy";
	void Start(){
		
	}
	
	void OnGUI() {
		
		if (show) {

			width = Screen.width/128;
			height = Screen.height/2;

			pos = new Vector2(Screen.width/20, Screen.height/2 - height/2);
			size = new Vector2 (width, height);
			
			int current =  (int)PlatformerController.combo;
			if(current<=0)
				current = 0;
			int max = (int)PlatformerController.maxCombo;

			GUI.DrawTexture (new Rect (pos.x - borderWidth, pos.y - borderWidth, size.x + borderWidth * 2, size.y + borderWidth * 2), outline);
			GUI.DrawTexture (new Rect (pos.x, pos.y, size.x, size.y), emptyTex);
			if(PlatformerController.combo /PlatformerController.maxCombo>=1)
				GUI.DrawTexture (new Rect (pos.x, pos.y, size.x, size.y * PlatformerController.combo /PlatformerController.maxCombo), comboTex);
			else
				GUI.DrawTexture (new Rect (pos.x, pos.y + (height - size.y * PlatformerController.combo /PlatformerController.maxCombo), size.x, size.y * PlatformerController.combo /PlatformerController.maxCombo), fullTex);
			//GUI.Label (new Rect (pos.x + size.x / 2.4f, pos.y - size.y / 4.0f, size.x / 2, size.y * 2.3f), "(" + current + "/" + max + ")");
			//GUI.Label (new Rect (pos.x, pos.y - size.y * 2.3f, size.x / 2, size.y * 2.3f), name);
		}
	}
	void Update() {
		
	}
}