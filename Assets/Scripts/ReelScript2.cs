using UnityEngine;
using System.Collections;

public class ReelScript2 : MonoBehaviour {
	
	
	float ReelSpeed=10;
	private static ReelScript2 _instance = null;
		public bool startSpinning;
	public bool isButtonClicked;
	public bool afterspin;
		Quaternion q1;
	float timeafterstop;
	public bool slowDownValue;
	public static ReelScript2 SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(ReelScript2)) as ReelScript2; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	
	// Use this for initialization
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
