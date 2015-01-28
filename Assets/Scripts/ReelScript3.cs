using UnityEngine;
using System.Collections;

public class ReelScript3 : MonoBehaviour {
	
	
	float ReelSpeed=10;
	// Use this for initialization
	
	private static ReelScript3 _instance = null;
	
	public bool isButtonClicked;
	public bool startSpinning;
	float timeafterstop;
	public bool afterspin;
		Quaternion q1;
	public bool slowDownValue;
	
	public static ReelScript3 SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(ReelScript3)) as ReelScript3; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	void Start () {
	ReelSpeed = Random.Range (400, 410);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (startSpinning)
			transform.Rotate (Vector3.right * ReelSpeed * Time.deltaTime);
		
		
		
		if(!slowDownValue && afterspin){
			
			transform.Rotate (Vector3.right * ReelSpeed * Time.deltaTime);
			
			if(ReelSpeed<=25){
				
				setQ1value();	
			slowDownValue=true;
			
			}
			else{
			ReelSpeed=ReelSpeed-3;
				
			}
			
		}
		
		
		if (afterspin && slowDownValue) {
			
			transform.rotation = Quaternion.Lerp (transform.rotation,
                                         q1,
                                         timeafterstop * 3.0f * Time.deltaTime);		
		
		}	
		
		
		
	}
	
	
	
	public void setQ1value(){
		
			timeafterstop=Random.Range(0.1f,0.9f);
		    print (transform.eulerAngles.x+"---"+(transform.eulerAngles.x%60));
		
			int valueIs=60;
			if(Random.Range(0,3)==1){
			
			valueIs=120;
			}
		
		    float rotationvalue=(60-(transform.eulerAngles.x%60))+transform.eulerAngles.x;
		
		    print (rotationvalue);
		
			q1 = Quaternion.Euler (rotationvalue, transform.rotation.y, transform.rotation.z);
		
		
	}
	
}
