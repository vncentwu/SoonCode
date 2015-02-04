using UnityEngine;
using System.Collections;


public class MainMenuScript : MonoBehaviour {

	float ringWidth = 500.0f;
	float ringHeight = 300.0f;
	public Texture2D ring;
	public Texture2D dot;
	public Texture2D newgame;
	public Texture2D loadgame;
	public Texture2D options;
	public Texture2D sound;
	public Texture2D soundOff;
	public Texture2D optionsBackground;
	public Texture2D quit;

	Rect optionsRect;
	float optionsWidth;
	float optionsHeight;
	public static float volumeSliderValue = 1;
	public static float pastVolume = 1;
	public bool showOptions;


	// Use this for initialization
	void Start () {
	
		optionsWidth = Screen.width/2;
		optionsHeight = Screen.height - Screen.height/10;

		optionsRect = new Rect(Screen.width/2 - optionsWidth/2, Screen.height/2 - optionsHeight/2, optionsWidth, optionsHeight);

	}
	
	// Update is called once per frame
	void Update () {
		LowHealthEffect ();

		if(volumeSliderValue != 0)
			PlatformerController.mute = false;
		else
			PlatformerController.mute = true;
		if(PlatformerController.mute)
		{
			if(volumeSliderValue!=0)
				pastVolume = volumeSliderValue;
			volumeSliderValue = 0;
			AudioListener.pause = true;
			AudioListener.volume = volumeSliderValue;
		}
		else
		{
			AudioListener.pause = false;
			volumeSliderValue = pastVolume;
			AudioListener.volume = volumeSliderValue;
		}

	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height - 120, 140, 110), ring);
		GUI.BeginGroup (new Rect(Screen.width/2 - ringWidth/2, Screen.height/2 - ringHeight/2, ringWidth, ringHeight));
		float buttonWidth = 130.0f;
		float buttonHeight = 40.0f;
		if(GUI.Button (new Rect(ringWidth/2-buttonWidth/2, 0.5f *ringHeight/8, buttonWidth, buttonHeight), newgame)&& !showOptions)
			Application.LoadLevel ("Tutorial");
		if(GUI.Button (new Rect(ringWidth/2-buttonWidth/2, 2.5f*ringHeight/8, buttonWidth, buttonHeight), loadgame)&& !showOptions)
			Application.LoadLevel ("platformer");
		if(GUI.Button (new Rect(ringWidth/2-buttonWidth/2, 4.5f*ringHeight/8, buttonWidth, buttonHeight), options)&& !showOptions)
			showOptions = true;
		if(GUI.Button (new Rect(ringWidth/2-buttonWidth/2, 6.5f*ringHeight/8, buttonWidth, buttonHeight), quit)&& !showOptions)
			Application.OpenURL ("http://goo.gl/forms/ACuSTwMHDb");
		GUI.EndGroup();


		if(PlatformerController.mute)
		{
			if(GUI.Button (new Rect(40, Screen.height - 80, 40, 40), soundOff))
			{
				PlatformerController.mute = false;
				volumeSliderValue = pastVolume;
				if(pastVolume == 0)
				{
					volumeSliderValue = 0.5f;
					pastVolume = 0.5f;
				}

			}

		}
		else
		{
			if(GUI.Button (new Rect(40, Screen.height - 80, 40, 40), sound))
			{
				PlatformerController.mute = true;
				pastVolume = volumeSliderValue;
				volumeSliderValue = 0;
			}

				
		}

		if(showOptions)
		{
			//GUI.SetNextControlName("Options");
			optionsRect = GUI.Window(0, optionsRect, DoOptionsWindow, "Options");
			//GUI.FocusControl ("Options");
		}



	}

	void DoOptionsWindow(int windowID)
	{
		//GUI.DrawTexture (new Rect(0, 0, optionsWidth, optionsHeight), optionsBackground);
		if(GUI.Button (new Rect(optionsWidth - 20, 0, 20, 20), "x"))
		   showOptions = false;

		string volumemsg = "Volume: [" + (int)(volumeSliderValue*100) + "]";
		if(PlatformerController.mute || volumeSliderValue == 0)
			volumemsg = "Volume: [MUTE]";
		
		GUI.Label (new Rect(optionsWidth/10, optionsHeight/20, optionsWidth/6, optionsHeight/20), volumemsg);
		volumeSliderValue = GUI.HorizontalSlider(new Rect(optionsWidth/2 + optionsWidth/10, optionsHeight/20, optionsWidth/3, optionsHeight/20), volumeSliderValue, 0.0f, 1.0f);
		pastVolume = volumeSliderValue;
	}



	float intensityTarget =  2.5f;
	float intensityStart = 0.5f;
	float intensity = 0;
	float intensitySpeed = 0.3f;
	float intensityTime = 0;
	bool intensityForward = true;
	bool pulsing = true;
	void LowHealthEffect()
	{
	

		intensitySpeed = 0.5f;
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


}
