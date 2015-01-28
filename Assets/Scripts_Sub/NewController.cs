using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewController : MonoBehaviour
{
	
		static NewController myinstance;
//		static int instances = 0;
		private int previousFirstStationNumber = 0;
		public GameObject[] Lines;
		public GameObject ForTrain;
		public GameObject train;
	public GameObject[] m_trains;
//		public GameObject train2;
//		public GameObject train3;
//		public GameObject train4;
//		public GameObject train5;
//		public GameObject train6;
//		public GameObject train7;
//		public GameObject train8;
		public GameObject target;
//		public GameObject target2;
//		public GameObject target3;
//		public GameObject target4;
//		public GameObject target5;
//		public GameObject target6;
//		public GameObject target7;
//		public GameObject target8;
		public List <GameObject>allStations = new List<GameObject> (); // now 4 items


		public List <GameObject>trackStationsInList = new List<GameObject> ();
		public List <GameObject>trackStationsOne = new List<GameObject> ();
		public List <GameObject>trackStationsTwo = new List<GameObject> ();
		public List <GameObject>trackStationsThree = new List<GameObject> ();
		public List <GameObject>trackStationsFour = new List<GameObject> ();
		public List <GameObject>trackStationsFive = new List<GameObject> ();
		public GameObject[] firstStaitonGroups;
		public Camera mainCamera;
		public float cameraZoomFactor;
		public float speed;
		public bool isMoveTrain = false;
		public float rotatespeed = 10;
		public bool goOnce = false;
		public bool moveForward = true;
		public Ray ray;										//The hit ray
		public RaycastHit hit;								//The hit raycast
		public RaycastHit hitRelease;
		public RaycastHit hitToDraw;
		Quaternion newRotation;
		Vector3 eulers;
		public bool useTouch;
		public bool isAllowedToDraw;
		public GameObject menuCollider;
//		public GameObject drawLineObject_manager_1;
		public GameObject drawLineObject_manager_2;
//		public GameObject drawLineObject_manager_3;
//		public GameObject drawLineObject_manager_4;
//		public GameObject drawLineObject_manager_5;
//		public GameObject drawLineObject_manager_6;
//		public GameObject drawLineObject_manager_7;
//		public GameObject drawLineObject_manager_8;

//		private DrawLines drawLinesScript;

		public DrawLines drawLinesScript_1;
		public int mouseTouchCounter;
		public Texture2D[] TrainSolidColors;
		public GameObject tempGameObject_Original;

		public static NewController Instance {
				get {
						if (myinstance == null)
								myinstance = FindObjectOfType (typeof(NewController)) as NewController;

						return myinstance;
				}	
		}

		// Use this for initialization
		void Start ()
		{
//		drawLinesScript = drawLineObject_manager_1.GetComponent<DrawLines> ();
				drawLinesScript_1 = drawLineObject_manager_2.GetComponent<DrawLines> ();


				StartCoroutine (WaitToStart ());

				
				
				if (PlayerPrefs.HasKey ("previousFirstStationNumber") == false) {
						PlayerPrefs.SetInt ("previousFirstStationNumber", 0);
				}

				previousFirstStationNumber = PlayerPrefs.GetInt ("previousFirstStationNumber");



//				#if UNITY_EDITOR
//				useTouch= false;
//				#else
//				useTouch = true;
//				#endif

		}

		IEnumerator WaitToStart ()
		{
				yield return new WaitForSeconds (2f);
				StartCoroutine (ActiveFirstThreeStations ());
//		StartCoroutine (cameraPositioning ());
		}

//		IEnumerator cameraPositioning ()
//		{
//
//				if (mainCamera.orthographicSize > cameraZoomFactor) {
//						mainCamera.orthographicSize = mainCamera.orthographicSize - (Time.deltaTime * 5);
//						yield return new WaitForSeconds (0.020f);
//						StartCoroutine (cameraPositioning ());
//				} else {
//						StartCoroutine (ActiveFirstThreeStations ());
//				}
//
//
//		}

		IEnumerator ActiveFirstThreeStations ()
		{
				yield return new WaitForSeconds (1f);


				if (previousFirstStationNumber <= (firstStaitonGroups.Length - 2)) {
						previousFirstStationNumber++;
						PlayerPrefs.SetInt ("previousFirstStationNumber", previousFirstStationNumber);
				} else {
			 
						PlayerPrefs.SetInt ("previousFirstStationNumber", 0);
						previousFirstStationNumber = PlayerPrefs.GetInt ("previousFirstStationNumber");
				}


				foreach (Transform child in firstStaitonGroups[previousFirstStationNumber].transform) {

						ActivatedStations.Instance.AddStationInList( child.gameObject );	// to add activated stations to global list

						child.gameObject.SetActive (true);

						PassangerController_Script.Instance.AllActiveStationsList.Add(child.gameObject); // to add station in active station list
		
//						ActiveStationScript.Instance.AllActiveStationsList.Add(child.gameObject); // to add station in active station list
		
				}

				
				
				PlayerPrefs.Save ();



				StartCoroutine (StationGenerationScript.Instance.TimerToActiveStations ());


				StartCoroutine( PassangerController_Script.Instance.generatePassanger() );
				

				
		}

//		IEnumerator ActiveStaion ()
//		{
//
//		Debug.Log (">>>>>>>>>>>>>ActiveStaion");
//				yield return new WaitForSeconds (1f);
//		
//				randomGenerateStation (allStations [Random.Range (0, 4)]);
//
//				train.transform.position = new Vector3 (stationClone.transform.position.x, -40, stationClone.transform.position.z);
//		
//
//				yield return new WaitForSeconds (1f);
//				randomGenerateStation (allStations [Random.Range (0, 4)]);
//
//				target = stationClone;
//	
//				yield return new WaitForSeconds (2f);
//
//				
//				StartCoroutine (MoveTrainObject (train, trackStationsOne));
//
//		}
	
		GameObject stationClone;
//		private GameObject lastGeneratedStation;
	
		void randomGenerateStation (GameObject stationToGenerate)
		{
		
				stationClone = (GameObject)Instantiate (stationToGenerate, stationToGenerate.transform.position, stationToGenerate.transform.rotation);
//				lastGeneratedStation = stationClone;

		
				stationClone.transform.position = new Vector3 (Random.Range (-13, 13),
															stationToGenerate.transform.position.y,
															Random.Range (-7, 7));


		}

		int count;

		// Update is called once per frame
		void Update ()
		{
//		if( drawLinesScript_1.canSetTrack )
//		{

				GetClicks ();
//		}

		}
	Transform tempFirstStation;
		void GetClicks ()
		{
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);


				//If we pressed the mouse
//				if (Input.GetMouseButtonDown (0)) {
//
//						count++;
////			if(mouseTouchCounter>1)
//
////						TrackCheckForNew ();	
////						Debug.Log ("Check times");
//
//				}

				if (Input.GetMouseButtonUp (0)) {
	
						//Cast a ray
//						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			
//						//If the ray hit something in the set layer
//						if (Physics.Raycast (ray, out hitRelease, Mathf.Infinity)) {
//								if (hitRelease.collider.tag == "station" && drawLinesScript_1.index % 2 == 0) {
//
////										print ("Mouse released on = " + hitRelease.collider.name);
//
//								}
//
//						}

//			StartCoroutine (checkForSetTrackFlag());

//						if (drawLinesScript_1.canSetTrack) {
//								SetTrack (trackStationsInList, ForTrain);
//								drawLinesScript_1.canSetTrack = false;
//						}

//						if (ForTrain.active == false && trackStationsInList.Count > 1)
//								ForTrain.SetActive (true);

				}



		}

	public void setTheTrack( Vector2 MousePos)
	{

		ray = Camera.main.ScreenPointToRay (MousePos);
		
		//If the ray hit something in the set layer
		if (Physics.Raycast (ray, out hitRelease, Mathf.Infinity)) {

				
				//										print ("Mouse released on = " + hitRelease.collider.name);
//				trackStationsInList.Add (hitRelease.transform.gameObject);
				

			
		}
		
		//			StartCoroutine (checkForSetTrackFlag());

		//						if (drawLinesScript_1.canSetTrack) {


		//								drawLinesScript_1.canSetTrack = false;
		//						}
		
		//						if (ForTrain.active == false )
//		ForTrain.SetActive (true);
	}

//	IEnumerator checkForSetTrackFlag()
//	{
//
////		if( !drawLinesScript.canSetTrack )
////		{
////			print( " in coroutine...");
//			yield return new WaitForSeconds(0.5f);
////			StartCoroutine (checkForSetTrackFlag());
////		}
////		else{
//			SetTrack ( trackStationsInList, ForTrain );
////			drawLinesScript.canSetTrack = false;
////		}
//
//	}
	public void BeforeSetTrack(int _lineID)
	{
		Debug.Log ("_lineID= "+_lineID);
		if (_lineID == 0) {
			SetTrack (trackStationsInList, m_trains [_lineID], _lineID);
		}
		else if (_lineID == 1) {
			SetTrack (trackStationsOne , m_trains [_lineID], _lineID);
		}
	}

//		bool cantAdd;
		Ray rayFromCurve;
		RaycastHit hitFromCurve;

		void SetTrack (List<GameObject>trackStations, GameObject trainObject, int _lineID)
		{
//		print ("List count = "+trackStations.Count);
				if (hitRelease.transform != null) {

//			print ("First = "+tempFirstStation.name);
//			print ("Next = "+hitRelease.transform.name);
			Debug.Log ("First = "+tempFirstStation.name);

			if ( tempFirstStation.name != hitRelease.transform.name) {

//								print ("can set the track");
								for (int i = 0; i < trackStations.Count; i++) {
										if (trackStations [i].name == hitRelease.transform.name) {
//												cantAdd = true;
										}
								}
//				print ("Cant add = "+cantAdd +"  hitRelease.transform.tag = "+ hitRelease.transform.tag);
								if ( hitRelease.transform.tag == "station") {

										GameObject tempGameObject = (GameObject)Instantiate (tempGameObject_Original);// 1.to create object


										tempGameObject.transform.position = new Vector3 (drawLinesScript_1.curvePoint.x,		// 2.to set positions
					                                                 -43.78725f,
					                                                 drawLinesScript_1.curvePoint.z);

//					tempGameObject.transform.parent = GameObject.Find("Group_Station_CurvePoints").transform;	// 3.to assign parent
										tempGameObject.renderer.enabled = false;

										//here to add curve point 
				
										trackStations.Add (tempGameObject.transform.gameObject);
										AddTargetToTrack (trackStations, tempGameObject, _lineID);  //to add curve point
										
//										print ("curve point Added = " + hitRelease.transform.gameObject);

										//then end point

										trackStations.Add (hitRelease.transform.gameObject); 	//first add target object to list
										AddTargetToTrack (trackStations, trainObject, _lineID);		//then call AddTargetToTrack();
//										print ("Added = " + hitRelease.transform.gameObject);
										PassangerController_Script.Instance.AllActiveStationsList.Add(hitRelease.transform.gameObject); // to add station in active station list
//										ActiveStationScript.Instance.AllActiveStationsList.Add(hitRelease.transform.gameObject); // to add station in active station list
										PassangerPickScript.Instance.addStationType();

				}

//								cantAdd = false;

						} else {
								print ("Cant set the track");
						}
				}

		}

		public int targetNumber = 1;
	bool callOnceForMoveTrain;

		void AddTargetToTrack (List<GameObject>trackStations, GameObject trainObject, int _lineID)
		{


				if (trackStations.Count >= 2 ) {
	
		
						target.transform.position = new Vector3 (trackStations [targetNumber].transform.position.x, 
			                                         trackStations [targetNumber].transform.position.y, 
			                                         trackStations [targetNumber].transform.position.z);
						//isMoveTrain = true;
						trainObject.SetActive (true);
						StartCoroutine (setRotationTrain ());

			if(!callOnceForMoveTrain)
			{
				Debug.Log (">>>>>>>>>>>>>>>>>>_lineID= "+_lineID);
				StartCoroutine (MoveTrainObject (m_trains[_lineID], trackStations));		//train
				callOnceForMoveTrain = true;
			}

//						speed = 2;


				}
		
		}
		//values for internal use
		private Quaternion _lookRotation;
		private Vector3 _direction;

		IEnumerator setRotationTrain ()
		{
//		print ("In setRotationTrain");

				//find the vector pointing from our position to the target
				_direction = (target.transform.position - train.transform.position).normalized;
				//create the rotation we need to be in to look at the target
				_lookRotation = Quaternion.LookRotation (_direction);
				//rotate us over time according to speed until we are in the required rotation
				train.transform.transform.rotation = Quaternion.Slerp (train.transform.rotation, _lookRotation, Time.deltaTime * rotatespeed);

				yield return new WaitForSeconds (0.004f);

				StartCoroutine (setRotationTrain ());
//		print (" look at ");

		}


		public bool isTrackOneReturn;
	bool goOnceForFirstStation;
		public void TrackCheckForNew ( Transform firstStation , int _lineID)
		{

//				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
		

//						if (hit.collider.tag == "station") {//to add first station in list

//								if (trackStationsOne.Count == 0) {

//										ForTrain = train;	
//		ForTrain = m_trains [_lineID];

//										trackStationsInList = trackStationsOne;

//										drawLinesScript_1.SetLine ();
										
										
		if (_lineID == 0)
			tempFirstStation = firstStation;
		else
			tempFirstStation = firstStation;

		if(!goOnceForFirstStation)
		{
			if(_lineID == 0)
				trackStationsInList.Add ( firstStation.gameObject );
			else
				trackStationsOne.Add ( firstStation.gameObject );

			m_trains [_lineID].transform.position = new Vector3 (firstStation.position.x, firstStation.position.y, firstStation.position.z);
//			ForTrain.transform.position = new Vector3 (firstStation.position.x, firstStation.position.y, firstStation.position.z);
			goOnceForFirstStation = true;
		}
									

//								} 


								for (var col=0; col<8; col++) {
										if ((col + 1) == drawLinesScript_1.trackColor) {
				m_trains [_lineID].renderer.material.mainTexture = TrainSolidColors [col];
//												ForTrain.renderer.material.mainTexture = TrainSolidColors [col];
										}
								}
	

//						}
		

				

		}

	public void setTrainAcive (int _lineID) {
		m_trains [_lineID].SetActive (true);
	}

	public void RemoveAddedObject(int _lineID)
	{
		if (_lineID == 0) {
			print ("Item removed" + trackStationsInList.Count);
			if (trackStationsInList.Count == 1) {
				goOnceForFirstStation = false;
				trackStationsInList.RemoveAt (trackStationsInList.Count - 1);
			}
		} else if (_lineID == 1) {
			print ("Item removed" + trackStationsOne.Count);
			if (trackStationsOne.Count == 1) {
				goOnceForFirstStation = false;
				trackStationsOne.RemoveAt (trackStationsOne.Count - 1);
			}
		}
	}

	public bool allPassangerPicked;
	IEnumerator MoveTrainObject (GameObject trainObject, List<GameObject> trackStations)	{
		
		float step = speed * Time.deltaTime;
		trainObject.transform.position = Vector3.MoveTowards (trainObject.transform.position, target.transform.position, step);

//		Debug.Log ("target.transform.position= " + target.transform.position);
		
		if ( (trainObject.transform.position == target.transform.position) && allPassangerPicked )
		{
//			Debug.Log (">>>>>>>>>>>>>>>>>>>VAIBHAV");
			//						print("check repeat time......");
			/* Here i can check the station at which train is stayed */
			goOnce = false;
			//			allPassangerPicked = false;
			if ( isTrackOneReturn && targetNumber == 0) {
				isTrackOneReturn = false;
				
			}
			
			if( targetNumber%2==0 || targetNumber==0 ) //here we can get the target station type
			{
				//				print ("Station wait...."+ trackStations [targetNumber].renderer.material.name.Substring(0,4));
				allPassangerPicked = false;
				
				
				PassangerInTrain.Instance.currentStationType = trackStations [targetNumber].renderer.material.name.Substring(0,4);
				
				PassangerInTrain.Instance.GetAddPassangerInTrain( trackStations [targetNumber] ); // function to add passangers on train
				
			}
			
			
			if (!goOnce) {
				
				goOnce = true;
				
				if (targetNumber < (trackStations.Count - 1) && !isTrackOneReturn) {//forward journey
					
					targetNumber++;
					target.transform.position = new Vector3 (trackStations [targetNumber].transform.position.x, 
					                                         trackStations [targetNumber].transform.position.y, 
					                                         trackStations [targetNumber].transform.position.z);
					
				} else { //return journey
					
					isTrackOneReturn = true;
					targetNumber--;
					
					target.transform.position = new Vector3 ( trackStations [targetNumber].transform.position.x, 
					                                         trackStations [targetNumber].transform.position.y, 
					                                         trackStations [targetNumber].transform.position.z);
					
				}
				
			}
			
			if( targetNumber%2!=0 ) //train stop for some time at station
			{
				yield return new WaitForSeconds (1f);
				
			}
			
		}
		
		yield return new WaitForSeconds (0.02f);
		
		StartCoroutine (MoveTrainObject (trainObject, trackStations));
		
		
	}


	IEnumerator MoveTrainOnPath (GameObject trainObject, Vector3[] _path) {
		float step = speed * Time.deltaTime;
		trainObject.transform.position = Vector3.MoveTowards (trainObject.transform.position, target.transform.position, step);
		
		
//		if ( (trainObject.transform.position == target.transform.position) && allPassangerPicked )
//		{
//			Debug.Log (">>>>>>>>>>>>>>>>>>>VAIBHAV");
//			//						print("check repeat time......");
//			/* Here i can check the station at which train is stayed */
//			goOnce = false;
//			//			allPassangerPicked = false;
//			if ( isTrackOneReturn && targetNumber == 0) {
//				isTrackOneReturn = false;
//				
//			}
//			
//			if( targetNumber%2==0 || targetNumber==0 ) //here we can get the target station type
//			{
//				//				print ("Station wait...."+ trackStations [targetNumber].renderer.material.name.Substring(0,4));
//				allPassangerPicked = false;
//				
//				
//				PassangerInTrain.Instance.currentStationType = trackStations [targetNumber].renderer.material.name.Substring(0,4);
//				
//				PassangerInTrain.Instance.GetAddPassangerInTrain( trackStations [targetNumber] ); // function to add passangers on train
//				
//			}
//			
//			
//			if (!goOnce) {
//				
//				goOnce = true;
//				
//				if (targetNumber < (trackStations.Count - 1) && !isTrackOneReturn) {//forward journey
//					
//					targetNumber++;
//					target.transform.position = new Vector3 (trackStations [targetNumber].transform.position.x, 
//					                                         trackStations [targetNumber].transform.position.y, 
//					                                         trackStations [targetNumber].transform.position.z);
//					
//				} else { //return journey
//					
//					isTrackOneReturn = true;
//					targetNumber--;
//					
//					target.transform.position = new Vector3 ( trackStations [targetNumber].transform.position.x, 
//					                                         trackStations [targetNumber].transform.position.y, 
//					                                         trackStations [targetNumber].transform.position.z);
//					
//				}
//				
//			}
//			
//			if( targetNumber%2!=0 ) //train stop for some time at station
//			{
//				yield return new WaitForSeconds (1f);
//				
//			}
//			
//		}
//		
//		yield return new WaitForSeconds (0.02f);
//		
//		StartCoroutine (MoveTrainObject (trainObject, trackStations));
		yield return new WaitForSeconds (0.02f);
	}






}
