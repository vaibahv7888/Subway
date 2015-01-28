using UnityEngine;
using System.Collections;

public class Cube1Script : MonoBehaviour
{
	
	
	float ReelSpeed = 10;
	// Use this for initialization
	
	public string nameofObj;
	private static Cube1Script _instance = null;
	public bool isButtonClicked;
	public bool afterspin;
	public bool startSpinning;
	
	public static Cube1Script SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(Cube1Script)) as Cube1Script; 	
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
		print (nameofObj+"Cube1Script");
		
		
	}
	
	
	
	
	
}
