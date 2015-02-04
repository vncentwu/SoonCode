using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShowStats : MonoBehaviour {

	public bool show = true;
	bool showPause = false;
	public static bool showConsole = false;
	public bool showControls = true;
	// Use this for initialization
	public static List<string> messages = new List<string>();
	public static List<string> commands = new List<string>();
	public int messagesDisplayed =5;
	float test = 0;
	public static Vector2 scrollPosition  = Vector2.zero;
	public static Vector2 scrollPosition2  = Vector2.zero;
	public static Vector2 scrollPosition3  = Vector2.zero;
	public static Vector2 scrollConsole  = Vector2.zero;
	public static bool news = false;
	public static bool blanked = false;
	public float width = 250.0f;
	public float height = 150.0f;
	Rect windowRect;
	Rect windowRect2;
	Rect windowRect3;
	Rect windowRect4;
	Rect windowRect5;
	Rect chatRect;
	Rect chatTextRect;
	public int chatTextRectPadding = 4;
	public string chatText = "";
	public static bool mouseConsumed;

	public static string currentCommand = "";

	public float chatBorderWidth = 3;
	Rect chatBorderRect;

	public Texture2D chatBorderText;

	float pauseWidth = 300;
	float pauseHeight = 200;

	float consoleWidth = 600;
	float consoleHeight = 300;

	public static ConversationScript convo;

	public GUIStyle style;

	void Start () {
		windowRect = new Rect(Screen.width - 300 - 5, Screen.height - height - 5, 300, height);
		windowRect2 = new Rect(Screen.width/2 - width/2, Screen.height - 125 - 5.0f, width, 100);
		windowRect3 = new Rect(0, 0, width, 0.15f*Screen.height);
		windowRect4 = new Rect(Screen.width/2 - pauseWidth/2, Screen.height/2 - pauseHeight/2, pauseWidth, pauseHeight);
		windowRect5 = new Rect(0 +5, Screen.height - consoleHeight - 5, consoleWidth, consoleHeight);

		chatRect = new Rect(0.8f * Screen.width, 0.05f * Screen.height, 0.2f * Screen.width, 0.3f* Screen.height);
		chatTextRect = new Rect(chatRect.x + chatTextRectPadding, chatRect.y + chatTextRectPadding, chatRect.width - chatTextRectPadding*2, chatRect.height - chatTextRectPadding*2); 
		chatBorderRect = new Rect(0.65f * Screen.width - chatBorderWidth, 0.05f * Screen.height - chatBorderWidth, 0.35f * Screen.width + chatBorderWidth*2, 0.3f* Screen.height + chatBorderWidth*2);

		if(!blanked)
		{
			AddBlanks ();
			addMessage ("Welcome to Soon.");
			addMessage ("Check the bottom of vncentwu.itch.io/");
			addMessage ("For more detailed gameplay description.");
			addMessage ("");
			addMessage ("Press the tilde key (~) to open the console");
			addMessage ("and type help for a list of commands!");

			addCommand ("This is the console.");
			addCommand ("You can enter commands here while in development mode.");
			addCommand ("To see the full list of commands, type 'help' or go to the game description.");
			addCommand ("");
		}

		chatText = "This is a testing message. I hope things don't get fakt up gg lol";
	}

	void AddBlanks()
	{
		blanked = true;
		int count = 0;
		while(count<5)
		{
			count++;
			addMessage ("");
		}
	}

	// Update is called once per frame
	void Update () {




		//Debug.Log (chatBorderRect);
		//Debug.Log (Input.mousePosition);


		if(Input.GetKeyDown(KeyCode.F7))
			showControls = !showControls;
		if(Input.GetKeyDown (KeyCode.Escape))
			showPause=!showPause;
		if(Input.GetKeyDown (KeyCode.BackQuote))
			showConsole=!showConsole;

		if(convo!=null)
			if(convo.currentFrame == -1)
				EndConversation();
		//test++;
		//if(news)
		//	//scrollPosition = new Vector2(0, 20.0f + messages.Count*20.0f);
		//if(test%50 == 0)
			//addMessage ("Hello, this is a test: " + test);
	}

	public static void StartConversation(ConversationScript conv)
	{
		convo = conv;
		convo.currentFrame = 0;
	}

	public static void StartConversation(ConversationScript conv, int frame)
	{
		convo = conv;
		convo.currentFrame = frame;
		convo.CameraOn();
	}

	public static void EndConversation()
	{
		convo.CameraOff();
		convo=null;

	}
	public static void addMessage(string message)
	{
		messages.Add(message);
		scrollPosition2 = new Vector2(0, 20.0f + messages.Count*20.0f);
	}

	public static void addCommand(string message)
	{
		commands.Add("CLIENT [" + System.DateTime.Now.ToString ("hh:mm:ss") + "]: " + message);
		scrollConsole = new Vector2(0, 20.0f + commands.Count*20.0f);
	}
	public static void addPlayerCommand(string message)
	{
		commands.Add("PLAYER [" + System.DateTime.Now.ToString ("hh:mm:ss") + "]: " + message);
		scrollConsole = new Vector2(0, 20.0f + commands.Count*20.0f);
	}


	public static void ParseAndAddCommand(string command)
	{
		addPlayerCommand (command);
		if(command.ToUpper ().Equals ("GODMODE"))
		{
			PlatformerController.godmode = !PlatformerController.godmode;
			if(PlatformerController.godmode)
				addCommand ("Godmode is now on.");
			else
				addCommand ("Godmode is now off.");
		}
		else if(command.ToUpper ().Equals ("3D"))
		{
			PlatformerController.mode_3d = !PlatformerController.mode_3d;
			if(PlatformerController.mode_3d)
				addCommand ("3D mode is now on.");
			else
				addCommand ("3D mode is now off.");
		}
		else if(command.ToUpper ().Equals ("MANA"))
		{
			PlatformerController.mode_mana = !PlatformerController.mode_mana;
			if(PlatformerController.mode_mana)
				addCommand ("Unlimited mana is now on.");
			else
				addCommand ("Unlimited mana is now off.");
		}
		else if(command.ToUpper ().Equals ("ONEHIT"))
		{
			PlatformerController.oneHit = !PlatformerController.oneHit;
			if(PlatformerController.oneHit)
				addCommand ("One hitting is now on.");
			else
				addCommand ("One hitting is now off.");
		}
		else if(command.ToUpper ().Equals ("HELP"))
		{
			addCommand ("Current commands: 'godmode' (invincible), 'onehit' (1hitkill), and '3D mode' (can move in 3D");
		}
		else if(command.ToUpper ().Equals ("KILL"))
		{
			PlatformerController.killEverything = true;
			addCommand ("All enemies on this map have been killed.");
		}
		currentCommand = "";  

	}

	void OnGUI()
	{
		if(chatBorderRect.Contains (Event.current.mousePosition) && convo!=null)
			mouseConsumed = true;
		else
			mouseConsumed = false;
		
		//Debug.Log (mouseConsumed);
		if(show)
		{
			windowRect2 = GUI.Window(1, windowRect2, DoDebugWindow, "Debugging");
		}
		if(showPause)
			windowRect4 = GUI.Window (3, windowRect4, DoPauseWindow, "Menu");
		if(showControls)
			windowRect3 = GUI.Window (2, windowRect3, DoControlsWindow, "Controls");
		if(showConsole)
			windowRect5 = GUI.Window (4, windowRect5, DoConsoleWindow, "Console");


		windowRect = GUI.Window(0, windowRect, DoMessageWindow, "Messages");
		//GUI.depth = 2;



		//chatRect = GUI.Window (5, chatRect, DoChatWindow, "");
		if(convo!=null)
			ShowConvo();

		//GUI.depth = 0;



	}

	void ShowConvo()
	{
		GUI.BeginGroup (chatBorderRect);
		GUI.DrawTexture (new Rect(0,0, chatBorderRect.width, chatBorderRect.height), chatBorderText);
		GUI.BeginGroup (new Rect(chatBorderWidth + Screen.width *0.15f, chatBorderWidth, chatBorderRect.width - chatBorderWidth*2.0f, chatBorderRect.height - chatBorderWidth*2.0f));

		float w = chatTextRect.width;
		float h = chatTextRect.height * 0.1f;

		chatText = convo.GetMessage ();
		GUI.Label (new Rect(chatTextRectPadding, chatTextRectPadding, chatTextRect.width, chatTextRect.height), chatText, style);

		ConversationScript.Responses[] resps = convo.GetResponses();
		String targName = "";
		for(int i = 0; i<resps.Length; i++)
		{
			if(GUI.Button (new Rect(chatTextRectPadding, chatTextRect.height - (h+chatTextRectPadding) * ((resps.Length-i)), w, h), resps[i].response))
			{
				convo.ApplyTarget (resps[i].target);
				string broad = resps[i].broadcast;
				if(!broad.Equals (""))
					convo.ApplyBroadcast (broad);
			}

					
		}

		//if(GUI.Button (new Rect(chatTextRectPadding, chatTextRect.height - chatTextRectPadding - h, w, h), "test"))
			//Debug.Log ("wow");
		GUI.EndGroup();
		GUI.EndGroup();
	}
	void DoPauseWindow(int windowID)
	{
		float buttonWidth = 150;
		float buttonHeight = 20;
		if(GUI.Button (new Rect(pauseWidth/2-buttonWidth/2, pauseHeight/8, buttonWidth, buttonHeight), "Resume"))
			showPause = false;
		if(GUI.Button (new Rect(pauseWidth/2-buttonWidth/2, 3*pauseHeight/8, buttonWidth, buttonHeight), "Options"))
			addMessage ("Options not yet supported");
		if(GUI.Button (new Rect(pauseWidth/2-buttonWidth/2, 5*pauseHeight/8, buttonWidth, buttonHeight), "Main Menu"))
			Application.LoadLevel ("MainMenu");
		if(GUI.Button (new Rect(pauseWidth/2-buttonWidth/2, 7*pauseHeight/8, buttonWidth, buttonHeight), "Quit"))
			Application.OpenURL ("http://goo.gl/forms/ACuSTwMHDb");

	}

	void DoChatWindow(int windowID)
	{

	}

	void DoConsoleWindow(int windowID)
	{
		scrollConsole = GUI.BeginScrollView(new Rect(0, 20, consoleWidth, consoleHeight-40), scrollConsole, new Rect(0, 0, consoleWidth, 20 + commands.Count * 20));
		
		int count = 0;
		int pos = 0;
		while(pos<commands.Count)
		{
			GUI.Label (new Rect(5, 20 * count, consoleWidth, 100.0f), commands[pos]);
			pos++;
			count++;
		}

		//GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
		//GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
		//GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
		//GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
		GUI.EndScrollView();
		Event e = Event.current;
		if(e.type == EventType.keyDown && e.keyCode == KeyCode.Return)
			ParseAndAddCommand(currentCommand);
		if(e.type == EventType.keyDown && e.keyCode == KeyCode.BackQuote)
			showConsole = !showConsole;

		if(currentCommand.Equals ("`"))
			currentCommand = "";
		GUI.SetNextControlName ("Input");
		currentCommand = GUI.TextField(new Rect(5, consoleHeight - 20, consoleWidth, 20), currentCommand);
		GUI.FocusControl ("Input");
		GUI.DragWindow ();

	}

	void DoControlsWindow(int windowID)
	{
		scrollPosition3 = GUI.BeginScrollView(new Rect(5, 0, width, 200), scrollPosition3, new Rect(0, 0, 220, 20 + 120));
		


		GUI.Label (new Rect (00, 20.0f, 250.0f, 100.0f), "Check bottom of page for controls");
		GUI.Label (new Rect (00, 40.0f, 250.0f, 100.0f), "Way too many to print here now.");
		GUI.Label (new Rect (00, 60.0f, 250.0f, 100.0f), "Press F7 to close this window.");
		/*GUI.Label (new Rect (00, 40.0f, 250.0f, 100.0f), "Sprint - Shift          Attack - Left Click");
		GUI.Label (new Rect (00, 60.0f, 250.0f, 100.0f), "Dash/teleport - Q     Portal tele - X");
		GUI.Label (new Rect (00, 80.0f, 250.0f, 100.0f), "Pick-up Item - Z      ");
		GUI.Label (new Rect (00, 100.0f, 250.0f, 100.0f), "Cast Fireball - 1      Cast Lightning - 2");
		GUI.Label (new Rect (00, 120.0f, 250.0f, 100.0f), "Cast Fire Blast - 3   Cast Ice Blast - 4");
		GUI.Label (new Rect (00, 140.0f, 250.0f, 100.0f), "Cast Heal - 5          Restart - F5");
		GUI.Label (new Rect (00, 160.0f, 250.0f, 100.0f), "Suicide - F6           Toggle Controls - F7");
		GUI.Label (new Rect (00, 180.0f, 250.0f, 100.0f), "Open Console -  `    Open Menu - ESC");*/
		GUI.EndScrollView();
		if(GUI.Button (new Rect(windowRect3.width-20, 0, 20, 20), "x"))
			showControls = false;
		GUI.DragWindow ();

	}
	void DoDebugWindow(int windowID)
	{
		scrollPosition = GUI.BeginScrollView(new Rect(5, 20, 400, 150), scrollPosition, new Rect(0, 0, 220, 20 + 120));
		
		int count = 0;
		int pos = 0;
		float expTop = 100.0f * Mathf.Pow (1.2f, PlatformerController.level);
		GUI.Label (new Rect (00, 0, 100.0f, 100.0f), "Level: " + PlatformerController.level);
		GUI.Label (new Rect (0, 20.0f, 200.0f, 100.0f), "Exp: " + PlatformerController.currentExp + "   " + "NextLV: " + expTop);
		//GUI.Label (new Rect (0, 40.0f, 200.0f, 100.0f), "ExpNext: " + expTop);
		GUI.Label (new Rect (0, 40.0f, 200.0f, 100.0f), "Combo: " + PlatformerController.combo + "   BiggestHit: " + (int)PlatformerController.biggestHit);
		//GUI.Label (new Rect (0, 80.0f, 200.0f, 100.0f), "LastHit: " + PlatformerController.latestDamage);
		//GUI.Label (new Rect (0, 100.0f, 200.0f, 100.0f), "BiggestHit: " + PlatformerController.biggestHit);
		GUI.Label (new Rect (0, 60.0f, 200.0f, 100.0f), "Money: " + PlatformerController.money);
		GUI.EndScrollView();
		GUI.DragWindow ();
	}

	void DoMessageWindow(int windowID)
	{
		scrollPosition2 = GUI.BeginScrollView(new Rect(5, 20, 400, 150), scrollPosition2, new Rect(0, 0, 400, 20 + messages.Count * 20));
		
		int count = 0;
		int pos = 0;
		while(pos<messages.Count)
		{
			GUI.Label (new Rect(0, 20 * count, 400.0f, 100.0f), messages[pos]);
			pos++;
			count++;
		}
		//GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
		//GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
		//GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
		//GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
		GUI.EndScrollView();
		GUI.DragWindow ();
	}

}
