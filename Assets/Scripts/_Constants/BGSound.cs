using UnityEngine;
using System.Collections;

public class BGSound : MonoBehaviour
{

	private static BGSound _instance = null;

	public static BGSound SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(BGSound)) as BGSound; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	
	// Use this for initialization
	void Start ()
	{
	
		//PlayBgSound ();
	}
	
	public void PlayBgSound ()
	{
		MySound.StopSound (SOUND_CLIP.LOOPCROUD, transform);
		MySound.playSound (SOUND_CLIP.BG, transform, true);
		
		if (audio)
			audio.volume = 1f;
		
		
	}
	
	
	
	public void stopBgSound (){
		MySound.StopSound (SOUND_CLIP.BG, transform);
		MySound.playSound (SOUND_CLIP.LOOPCROUD, transform, true);
	
		if (audio)
			audio.volume = 0.3f;
		
	}
	
	
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
