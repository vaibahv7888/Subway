using UnityEngine;
using System.Collections;

public class InGameController : MonoBehaviour
{

	
	private static InGameController _instance = null;
	public GameObject footBall;
	public GameObject spawnPos;
	public Vector3 force;
	public int spawnCount;
	public bool isHit;
	public int[]levelTimer;
	public int[]goalsTarget;
	public int currentGoals;

	public int greencount,bluecount,pinkcount;
	
	public int tapcount;
	
	public bool isButtonClicked;
	public int noOfGifts;
	
	public bool isgameOver;
	
	public GameObject background;
	public GameObject slotMachine;
	public GameObject CameraOpen;
	public bool showThreetoOneImage;
	
	public string first,second,third;
	
	public static InGameController SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(InGameController)) as InGameController; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}

	void Start ()
	{
		
		
		
		//BGSound.SharedInstance.stopBgSound();
		
		
		

	}
	
	
	public void Update ()
	{
	
		if(Input.GetMouseButtonUp(1)){
			
			tapcount++;
			if(tapcount==1){
				
				ReelScript1.SharedInstance.startSpinning=true;
				ReelScript2.SharedInstance.startSpinning=true;
				ReelScript3.SharedInstance.startSpinning=true;
				
			}
			else if(tapcount==2){
				
				ReelScript1.SharedInstance.startSpinning=false;
				//ReelScript1.SharedInstance.setQ1value();
				ReelScript1.SharedInstance.afterspin=true;
				
				//ReelScript1.SharedInstance.animation.Play();
			}
			else if(tapcount==3){
				
				ReelScript2.SharedInstance.startSpinning=false;
				//ReelScript2.SharedInstance.setQ1value();
				ReelScript2.SharedInstance.afterspin=true;
				//ReelScript2.SharedInstance.animation.Play();
			}
			else if(tapcount==4){
				
				ReelScript3.SharedInstance.startSpinning=false;
				//ReelScript3.SharedInstance.setQ1value();
				ReelScript3.SharedInstance.afterspin=true;
				
				StartCoroutine(waitForyouwin());
				//ReelScript3.SharedInstance.animation.Play();
			}
			
			
			//print (tapcount);
			
		}
		
		
	}
	
	IEnumerator waitForyouwin ()
	{
		
		yield return new WaitForSeconds(10f);
		
//		GameManager.SharedInstance.OpenTheThirdExternalApp();
		if(Cube1Script.SharedInstance.nameofObj=="pink"){
			
			pinkcount++;
			first="pink";
			
		}
		if(Cube2Script.SharedInstance.nameofObj=="pink"){
			
			pinkcount++;
			second="pink";
			
		}
		if(Cube3Script.SharedInstance.nameofObj=="pink"){
			
			pinkcount++;
			third="pink";
			
		}
		if(Cube1Script.SharedInstance.nameofObj=="green"){
			
			greencount++;
			first="green";
		}
		if(Cube2Script.SharedInstance.nameofObj=="green"){
			
			greencount++;
			second="green";
			
		}
		if(Cube3Script.SharedInstance.nameofObj=="green"){
			
			greencount++;
			third="green";
			
		}
		if(Cube1Script.SharedInstance.nameofObj=="blue"){
			
			bluecount++;
			first="blue";
			
		}
		if(Cube2Script.SharedInstance.nameofObj=="blue"){
			
			bluecount++;
			second="blue";
			
		}
		if(Cube3Script.SharedInstance.nameofObj=="blue"){
			
			bluecount++;
			third="blue";
			
		}
		
		if(pinkcount==3 || greencount==3 || bluecount==3){
			
			noOfGifts=3;
		}
		else if(pinkcount==2 || greencount==2 || bluecount==2){
			
			noOfGifts=2;
		}
		else{
			noOfGifts=1;
			
		}
		
		
		print (noOfGifts);
		
		
		
		GameStates.SetCurrent_State_TO (GAME_STATE.YOUWIN);
	}
	
	
	void OnDestroy(){
		if(GameManager.SharedInstance){
		GameManager.SharedInstance.username="";
		GameManager.SharedInstance.emailId="";
		GameManager.SharedInstance.twitterID="";
		}
		
	}
	
	
	
}
