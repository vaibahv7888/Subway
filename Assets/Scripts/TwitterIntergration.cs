using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Text;

public class TwitterIntergration : MonoBehaviour
{
	private static TwitterIntergration _instance = null;
	public string name;
	public string twitterMsg;
	public Texture2D profilePic;
	
	public static TwitterIntergration SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(TwitterIntergration)) as TwitterIntergration; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	
	void Start ()
	{
		
		
	
	}
	
	public void PostOnTwitter (string msg)
	{
		string newUsername = msg.Replace ("@", "");
		
		string url = "http://www.sourcekode.com/twitterapi/search.php?handle=" + newUsername;
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (www));
		
		
	}
	
	IEnumerator WaitForRequest (WWW www)
	{
		yield return www;
 
// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.data);
			  
			if(www.data.Contains("profile_pic_original")){
			StartCoroutine( ParseTheJson (www.data));
			}
			else{
				
				GameStates.SetCurrent_State_TO (GAME_STATE.FINALSCREEN);
			}
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
	
	IEnumerator ParseTheJson (string msg)
	{
		
		var dict = MiniJSON.Json.Deserialize (msg) as Dictionary<string,object>;
		
		name = (string)dict ["handle"];
		twitterMsg = (string)dict ["message"];
		//Debug.Log ("dict['profile_pic']: " + (string)dict ["profile_pic"]);
		
		WWW www = new WWW ((string)dict ["profile_pic_original"]);
		
		yield return www;
		
		profilePic = www.texture;
	
		GameStates.SetCurrent_State_TO (GAME_STATE.FINALSCREEN);
		
	}
	
	
	
}
