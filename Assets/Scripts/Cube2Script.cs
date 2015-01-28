using UnityEngine;
using System.Collections;

public class Cube2Script : MonoBehaviour
{
	
	
	float ReelSpeed = 10;
	// Use this for initialization
	
	public string nameofObj;
	private static Cube2Script _instance = null;
	public bool isButtonClicked;
	public bool afterspin;
	public bool startSpinning;
	
	public static Cube2Script SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(Cube2Script)) as Cube2Script; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	 
	Quaternion q1;
	
	void Start ()
	{
		
		
	
	}
	
	void OnTriggerEnter(Collider gameObj){

		nameofObj=gameObj.name;
		print (nameofObj+"cube2swcript");
		
	}
	
	
	
	
	
}
