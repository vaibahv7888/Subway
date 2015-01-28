#define UNITY_IPHONE
//#define SYSTEM_GC

using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	
	void Start ()
	{
		#if UNITY_IPHONE
		gui.SetQualityLevel ();
		
#endif	
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			
			
		}
		
	}	
	
	void OnGUI ()
	{
		gui.setGUIScale ();
		
		if (GameManager.SharedInstance.exitpopupflag) {
			GUI.enabled = false;	
		}	
		
		if (GameStates._currentState != null) {
			GameStates._currentState ();
		}
		if (GameManager.SharedInstance.exitpopupflag) {
			GUI.enabled = true;
			
		}
	}
		
}
