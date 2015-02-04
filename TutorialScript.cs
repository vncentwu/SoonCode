using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

	//public GameObject platform1;
	//public GameObject platform2;
	//public GameObject platform3;
	public GameObject[] platforms;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CheckBroadcasts", 1.0f, 0.25f);
		Invoke ("LateIntro",  0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateIntro()
	{
		ShowStats.addMessage("-----------------------");
		ShowStats.addMessage("Talk to the NPC by clicking on him.");
	}

	void CheckBroadcasts()
	{
		//Debug.Log (ConversationScript.broadcasts);

		if(ConversationScript.FindAndRemoveBroadcast("GoToBox1"))
		{
			CreateBoxLights(platforms[0]);
			ShowStats.addMessage("-----------------------");
			ShowStats.addMessage("Use A and D keys to move left and right.");
			ShowStats.addMessage("Press space key to jump.");

		}
		if(ConversationScript.FindAndRemoveBroadcast("Run1"))
		{
			ConversationScript con = GameObject.Find ("TutorialGuide").GetComponent<ConversationScript>();
			//con.CameraOn();
			ShowStats.StartConversation(con, 4);
			CreateBoxLights (platforms[1]);

			HorizontalFlashingRedArrow.start = true;
			ShowStats.addMessage("-----------------------");
			ShowStats.addMessage("Hold shift key to sprint.");
		}
		if(ConversationScript.FindAndRemoveBroadcast("ShiftJump"))
		{
			ShowStats.StartConversation(GameObject.Find ("TutorialGuide").GetComponent<ConversationScript>(), 5);
			CreateBoxLights(platforms[2]);
			ShowStats.addMessage("-----------------------");
			ShowStats.addMessage("Hold shift and space to long jump.");
			HorizontalFlashingRedArrow.start = false;
		}
		if(ConversationScript.FindAndRemoveBroadcast("Fight"))
		{
			ShowStats.StartConversation(GameObject.Find ("TutorialGuide").GetComponent<ConversationScript>(), 6);
			ShowStats.addMessage("-----------------------");
			ShowStats.addMessage("Press I to open inventory.");
		}
		if(ConversationScript.FindAndRemoveBroadcast("ReadyToFight"))
		{
			ShowStats.addMessage("-----------------------");
			ShowStats.addMessage("Left click to attack.");
		}
	}

	void CreateBoxLights(GameObject platform)
	{
		if(platform.GetComponent<TutorialPlatform1Script>()!=null)
			platform.GetComponent<TutorialPlatform1Script>().ShowAll();
	}

	/*void CreateBox1Lights()
	{
		if(platform1.GetComponent<TutorialPlatform1Script>()!=null)
			platform1.GetComponent<TutorialPlatform1Script>().ShowAll();
	}

	void CreateBox2Lights()
	{
		if(platform2.GetComponent<TutorialPlatform1Script>()!=null)
			platform2.GetComponent<TutorialPlatform1Script>().ShowAll();
	}

	void CreateBox3Lights()
	{
		if(platform3.GetComponent<TutorialPlatform1Script>()!=null)
			platform3.GetComponent<TutorialPlatform1Script>().ShowAll();
	}*/

}


