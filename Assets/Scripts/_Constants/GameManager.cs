using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
public class GameManager : MonoBehaviour
{

	public delegate void CurrentState ();

	public  GAME_STATE currentState;
	public CurrentState _currentState;
	private static GameManager _instance = null;
	public static GameManager current;
	public  Transform myTransform;
	public bool soundStatus = true;
	public bool isGamePause;
	public bool isChapterStart;
	bool isTextBlink;
	private int Tap_To_Play_YPOS;
	public int selectedLevelNo;
	public bool exitpopupflag;
	public float xPos, yPos, width, height;
	public int unlockedLevelNo;
	public bool playSoundOnce;
//	public bool showDiwaliCrushAd;
	public string username="", emailId="",twitterID="";
	
	

	public static GameManager SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(GameManager)) as GameManager; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}

	public void  Awake ()
	{
		
		//PlayerPrefs.DeleteAll();
		if (current != null && current != this) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			current = this;
			myTransform = transform;
		}
		if (myTransform.gameObject.GetComponent<GUIManager> () == null) {
			myTransform.gameObject.AddComponent<GUIManager> ();
		}
		
		Input.multiTouchEnabled = true;

		
	}

	void Start ()
	{
		print ("In start");
		RegisterGameStates ();
		GameStates.SetCurrent_State_TO (GAME_STATE.MAINMENU);
		soundStatus = MySound.SharedInstance.soundON;
		
		
	}
	

	
	public void RegisterGameStates ()
	{
		
		GameStates.RegisterStates (GAME_STATE.SPLASHSCREEN, SplashScreen);
		
		GameStates.RegisterStates (GAME_STATE.MAINMENU, MainMenu);

		GameStates.RegisterStates (GAME_STATE.INGAME, InGame);
		
		GameStates.RegisterStates (GAME_STATE.USERINPUTSCREEN, UserInputScreen);
		
		GameStates.RegisterStates (GAME_STATE.GETREADY, GetReady);
		
		GameStates.RegisterStates (GAME_STATE.AFTERGETREADY, AfterGetReady);
		
//		GameStates.RegisterStates (GAME_STATE.INGAME, InGame);
		
		GameStates.RegisterStates (GAME_STATE.YOUWIN, Youwin);
		
		GameStates.RegisterStates (GAME_STATE.SHAREONFACEBOOK, ShareonFaceboook);
		
		GameStates.RegisterStates (GAME_STATE.FACEBOOKSCREEN, FacebookScreen);
		
		GameStates.RegisterStates (GAME_STATE.TWITTERSCREEN, TweeterScreen);
		
		GameStates.RegisterStates (GAME_STATE.FINALSCREEN, FinalScreen);
		
		GameStates.RegisterStates (GAME_STATE.CAMERACAPTURE, CameraCapture);

		
	
	}

	public void SplashScreen()
	{
		gui.setGUIScale ();

		GameStates.SetCurrent_State_TO (GAME_STATE.MAINMENU);
	}


	public void MainMenu ()
	{
		gui.setGUIScale ();
		
		
		if (!playSoundOnce) {
			
			playSoundOnce = true;
			BGSound.SharedInstance.PlayBgSound ();
			
		}
			
		if (gui.Button (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN1, IMAGE_TYPE.TEXT_SCREEN1)) {
			
			GameStates.SetCurrent_State_TO (GAME_STATE.USERINPUTSCREEN);
			
		}
		
//		print ("In main menu");
	}
	
	public void UserInputScreen ()
	{
		gui.setGUIScale ();
		
		
		gui.DrawTexture (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN2);
		
		
		
	}
	
	public void GetReady ()
	{
		
		gui.setGUIScale ();
		gui.DrawTexture (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN3);
		
	}
	
	public void AfterGetReady ()
	{
		
		gui.setGUIScale ();
		gui.DrawTexture (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN4);
		
	}
	
	IEnumerator waitForGetReadyScreen ()
	{

		yield return new WaitForSeconds(3f);
		GameStates.SetCurrent_State_TO (GAME_STATE.AFTERGETREADY);
		
		StartCoroutine (waitForAfterGetReadyScreen ());
	}

	IEnumerator waitForAfterGetReadyScreen ()
	{
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel(1);
		yield return new WaitForSeconds(2f);
//		OpenTheSecondExternalApp();
		GameStates.SetCurrent_State_TO (GAME_STATE.INGAME);
	}

		public void InGame ()
	{
		gui.setGUIScale ();

		print ( "In gameObject");
	}
	
	public void Youwin ()
	{
		gui.setGUIScale ();
		
		
		
	}
	
	public void ShareonFaceboook()
	{
		
		gui.setGUIScale ();
		gui.DrawTexture (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN8);
	
		
	}
	
	public void FacebookScreen()
	{
			gui.setGUIScale ();
		
//		if (gui.Button (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN9, IMAGE_TYPE.TEXT_SCREEN9)) {
//			
//		string msg="I just played the #HPSurprise game and won!";	
//			
//		  Application.OpenURL("http://sourcekode.com/facebookapi/index.php?msg="+msg);	
//			
//			GameStates.SetCurrent_State_TO (GAME_STATE.MAINMENU);
//			Application.LoadLevel(0);
//			
//		}
		
	}
	
	public void TweeterScreen(){
		
		
		gui.setGUIScale ();
		gui.DrawTexture (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN11);
//			twitterID = gui.TextField (42.1f, 233.9f-12.03f, 185f, 25.01f, twitterID,GUIStyle.none);
//		  if (gui.Button (80.9f, 255.7f, 98.7f, 21.1f, IMAGE_TYPE.BTN_BLANK, IMAGE_TYPE.BTN_BLANK)) {
//			
//			if(twitterID!="")
//			TwitterIntergration.SharedInstance.PostOnTwitter(twitterID);
//			
//		}
		
	}
	
	public void FinalScreen()
	{
			gui.setGUIScale ();
		
		
//		if (gui.Button (0, 0, 270, 480, IMAGE_TYPE.TEXT_SCREEN10, IMAGE_TYPE.TEXT_SCREEN10)) {
//			if(GameManager.SharedInstance){
//		GameManager.SharedInstance.username="";
//		GameManager.SharedInstance.emailId="";
//		GameManager.SharedInstance.twitterID="";
//		}
//			Application.LoadLevel(0);
//			GameStates.SetCurrent_State_TO(GAME_STATE.MAINMENU);
//			
//		}
		
//		GUI.DrawTexture(new Rect(146.02f,335.49f,100,120),TwitterIntergration.SharedInstance.profilePic);
//		gui.Label(29.4f,345.2f,100,100,TwitterIntergration.SharedInstance.name,15 );
//		gui.Label(28.93f,370.3f,80,200,TwitterIntergration.SharedInstance.twitterMsg,15 );
//		
		
	}
		
	public void CameraCapture(){
		gui.setGUIScale ();
		
//		if(!InGameController.SharedInstance.showThreetoOneImage){
//			
//			gui.DrawTexture (0f,0f, 270, 480, IMAGE_TYPE.TEX_SMILE);
//		}
		
		
	}
	
//	IEnumerator waitForSomeTime(){
//	yield return new WaitForSeconds(3f);
//		if(InGameController.SharedInstance)
//		InGameController.SharedInstance.showThreetoOneImage=true;
//		
//		yield return new WaitForSeconds(1f);	
//		
//		if(WebcamTest.SharedInstance)
//		WebcamTest.SharedInstance.TakeSnapshot();
//		
//	}

}
