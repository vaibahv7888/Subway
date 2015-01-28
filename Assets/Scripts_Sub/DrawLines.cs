// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;


public class DrawLines : MonoBehaviour {

	
	public Material lineMaterial;
	
	public Material endlineMaterialRed, endlineMaterialBlue;
	public Texture2D capTexRed;
	public Texture2D capTexBlue;


	float rotateSpeed= 90.0f;
	private VectorLine[] line;
	
	private VectorLine endLine;
	
	public int index;
	private bool  endReached;
	private Vector2[] linePoints;
	private Color[] lineColors;
	private bool continuous= true;
	private bool fillJoins= false;
	private bool weldJoins= false;
	private bool oldFillJoins= false;
	private bool oldWeldJoins= false;
	private bool thickLine= false;
	private bool canClick= true;
	private int oldWidth;
	
	public bool  canSetTrack;

	public Vector3 curvePoint;
	public Camera screenCamera;

	public int NuOfLines = 2;

	Vector3 screenPosUp;

	//New Variables
	int[] m_lineIndex;
	int m_currentLineStart = 0;
	int m_lineID = 0;
	bool m_canDraLine = false;

	string[] m_startEnd = new string[2];
	string[,] m_newStartEnd;
	string[] m_capTexNames;
	Dictionary<int, Vector2[]> m_allLineDictionary = new Dictionary<int, Vector2[]>();
//	Dictionary<string, string> m_allStartEnd = new Dictionary<string, string>();
	List<string[]> m_allStartEnd = new List<string[]>();

	void  Start (){
		line = new VectorLine[NuOfLines];
		m_lineIndex = new int[NuOfLines];

		m_startEnd[0] = "NULL";
		m_startEnd[1] = "NULL";

		m_capTexNames = new string[NuOfLines];
		m_capTexNames[0] = "cap";
		m_capTexNames[1] = "capBlue";

		m_newStartEnd = new string[NuOfLines,2];

		for(int i=0; i<NuOfLines; i++) {
			Vector2[] _tempLineArray = new Vector2[2];
//			m_allLineDictionary.Add (i, _tempLineArray);
			m_allStartEnd.Add (m_startEnd);
			m_lineIndex[i] = 0;
			m_newStartEnd[i,0] = "NULL";
			m_newStartEnd[i,1] = "NULL";
		}

		oldWidth = Screen.width;

//		SetLine();

		//		VectorLine.SetCamera( CameraClearFlags.Nothing );		// To set camer of line[m_lineID] belaw main camera
		VectorLine.SetCamera( screenCamera , CameraClearFlags.Nothing , true );		// To set camer of line[m_lineID] belaw main camera
		//		VectorLine.canvas.sortingOrder = -1;
		VectorLine.SetEndCap (m_capTexNames[0], EndCap.Mirror, endlineMaterialRed, capTexRed);
		VectorLine.SetEndCap (m_capTexNames[1], EndCap.Mirror, endlineMaterialBlue, capTexBlue);
		SetLine();
	}
	
	public void  SetLine (){
		
		
		//////////////////////////////////////
		//	VectorLine.Destroy (line[m_lineID]);
		
		//	if (!continuous) {
		//		fillJoins = false;
		//	}
		LineType lineType= (continuous? LineType.Continuous : LineType.Discrete);
		
		//	FIXME_VAR_TYPE joins= (fillJoins? Joins.Fill : Joins.None);
		Joins joins= Joins.Fill ;
		float lineWidth= 8;//(thickLine? 24 : 2);
		
		linePoints = new Vector2[3];		//500
		if (lineType == LineType.Continuous) {
			lineColors = new Color[linePoints.Length-1];
		}
		else {
			lineColors = new Color[linePoints.Length/2];
		}


	
//		foreach(Color color1 in lineColors) 
//		{
//			color = Color.blue;
//
//		}

//		var line4 = new VectorLine("Vai", [Vector2(.1, .6), Vector2(.9, .6)], lineMaterial3, 20.0);
//		line4.useViewportCoords = true;
//		line4.endCap = "rounded";
//		line4.Draw ();

//		Vector2[] temp = {new Vector2(0.1f, 0.6f), new Vector2(0.5f, 0.5f), new Vector2(0.9f, 0.6f)};
//
//		VectorLine line1 = new VectorLine("Line", temp, Color.red, lineMaterial, 5.0f, lineType, joins);
//		line1.useViewportCoords = true;
//		line1.points2 = temp;
//		line1.endCap = m_capTexNames[m_lineID];
//		line1.Draw ();

		line[m_lineID] = new VectorLine("Line", linePoints, Color.red, lineMaterial, lineWidth, lineType, joins);
//		line[m_lineID].useViewportCoords = true;
		line[m_lineID].drawTransform = transform;
		line[m_lineID].collider = true;


		endReached = false;
		index = 0;
		
		//	endLine = new VectorLine("header", linePoints, lineColors, endlineMaterialRed, lineWidth, lineType, joins);
		//	endLine.drawTransform = transform;
		
		//	line[m_lineID].SetColor (Color(0.28ff,0.28ff, 0.28ff));
		oldFillJoins = true;
		oldWeldJoins = true;
		fillJoins = true;
		weldJoins = true;
		/////////////////////////////////////
//		SetTrackColor();
//		RandomizeColor ( this.trackColor );
		//capTex = 
		
//		line[m_lineID].endCap = m_capTexNames[m_lineID];
//		line[m_lineID].depth = -10;
//		line[m_lineID].layer = -1;
		
	}
	
	Ray ray;
	RaycastHit hit;
	RaycastHit hitRelease;
	
	Vector2 temp;
	int quadrant=-1; 
	
	public int count;

//	Vector2 startPoint;
	Vector2 endPoint;

	private bool m_onlyOnes = false;

	bool getLineId (string _stationName) {
		bool _isLine = false;
		Vector2[] _ln1, _ln2;

		for (int i=0; i<NuOfLines; i++) {
			m_startEnd = m_allStartEnd [i];
			if(i==0) {
				_ln1 = getPointsForLine (i);
				_ln2 = getPointsForLine (i);
			}else {
				_ln1 = getPointsForLine (i);
				_ln2 = getPointsForLine (i-1);
			}
			Debug.Log (m_allStartEnd[i][0]);
			Debug.Log ("m_startEnd["+0+"]= "+m_startEnd[0]);
			Debug.Log ("m_startEnd["+1+"]= "+m_startEnd[1]);
//			if(m_startEnd[1] == _stationName || m_startEnd[1] == "NULL") {
			if(m_newStartEnd[i,1] == _stationName || m_newStartEnd[m_lineID,1] == "NULL") {
				m_lineID = i;
				Debug.Log (">>>>>>>>>>>>getLineId_1= "+i);
				return true;
			}else if(i>0 && !m_onlyOnes) {
				m_onlyOnes=true;
				if(_ln1[1] == _ln2[1]) {
					m_lineID = i;
					Debug.Log (">>>>>>>>>>>>getLineId_1= "+i);
					return true;
				}
			}else {
				Debug.Log (">>>>>>>>>>>>getLineId_2= "+ i);
				_isLine = false;
			}
		}

		Debug.Log (">>>>>>>>>>>>getLineId_3");
		return _isLine;
	}


	void  Update (){
		// Since we can rotate the transform, get the local space for the current point, so the mouse position won't be rotated with the line[m_lineID]
		Vector2 mousePos = transform.InverseTransformPoint(Input.mousePosition);
		
		//get the vector representing the mouse's position relative to the point...
		Vector2 v = mousePos - linePoints[m_lineIndex[m_lineID]];
		//use atan2 to get the angle; Atan2 returns radians
		float angleRadians=Mathf.Atan2(v.y, v.x);
		//convert to degrees
		float angleDegrees= angleRadians*Mathf.Rad2Deg;
		//angleDegrees will be in the range (-180,180].
		//I like normalizing to [0,360) myself, but this is optional..
		if (angleDegrees<0)
			angleDegrees+=360;
		
		
		ray = screenCamera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity )) 
		{
//			Debug.Log("hit.collider.name= "+hit.collider.name);
		}
		

		
		// Set the current line[m_lineID] point and color when the mouse is clicked
		if ( Input.GetMouseButtonDown(0)
		    && hit.collider.tag=="station")
		{

//			m_startEnd = m_allStartEnd[m_lineID];
//			if(m_startEnd[0] == "NULL" || m_startEnd[1] == "NULL") {
//				m_startEnd[0] = hit.collider.gameObject.name;
//			}else if(m_startEnd[1] != hit.collider.gameObject.name) {
//				return;
//			}

//			if(m_startEnd[1] != hit.collider.gameObject.name && m_startEnd[1] != "NULL") {
//				m_lineID += 1;
//				if(m_lineID < NuOfLines) {
//					m_startEnd = m_allStartEnd[m_lineID];
//				}else {
//					m_lineID -= 1;
//					return;
//				}
//			}

			if(!getLineId (hit.collider.gameObject.name)) {
				return;
			}

			if(m_newStartEnd[m_lineID,0] == "NULL" && m_newStartEnd[m_lineID,1] == "NULL") {
				m_newStartEnd[m_lineID,0] = hit.collider.gameObject.name;
			}

			if(m_startEnd[0] == "NULL" || m_startEnd[1] == "NULL") {
				m_startEnd[0] = hit.collider.gameObject.name;
			}

			resetLine (mousePos);
			line[m_lineID].endCap = "";

			count++;

			linePoints[m_lineIndex[m_lineID]+2] = mousePos;
			// Don't overflow the points array
//			if ((m_lineIndex[m_lineID]+2) == line[m_lineID].points2.Length-1) {				//Vaibhav Removed
//				line[m_lineID].maxDrawIndex = m_lineIndex[m_lineID]+1;
//				endReached = true;
//				Debug.Log (">>>>>>>>>>>>>>Vai1::line[m_lineID].maxDrawIndex= "+line.maxDrawIndex);
//			}else {
//				line[m_lineID].maxDrawIndex = m_lineIndex[m_lineID]+2;
//				Debug.Log (">>>>>>>>>>>>>>Vai2::line[m_lineID].maxDrawIndex= "+line.maxDrawIndex);
//			}

			line[m_lineID].maxDrawIndex = m_lineIndex[m_lineID]+2;

//			linePoints[index++] = mousePos;
//			// Don't overflow the points array
//			if (index == line[m_lineID].points2.Length-1) {
//				index--;
//				endReached = true;
//			}
//			line[m_lineID].maxDrawIndex = index;
			
			if(index==1)
			{
				temp = mousePos;
				print(temp );
				
			}
			
			
			if(angleDegrees>=0 && angleDegrees<=90  )
			{
				quadrant = 1;
				
			}else if(angleDegrees>=90 && angleDegrees<=180  )
			{
				quadrant = 2;
				
			}else if(angleDegrees>=180 && angleDegrees<=270  )
			{
				quadrant = 3;
				
			}else if(angleDegrees>=270 && angleDegrees<=360  )
			{
				quadrant = 4;
				
			}

			NewController.Instance.TrackCheckForNew( hit.collider.transform, m_lineID);

			linePoints[m_lineIndex[m_lineID]]= screenCamera.WorldToScreenPoint ( hit.collider.transform.position );

//			startPoint = linePoints[m_lineIndex[m_lineID]];
			m_canDraLine = true;
			Debug.Log (">>>>>>>>>>>>>>>>>>>>>>mousePos= "+mousePos);
		}
		
		
//		if(index>=1)
		if(m_canDraLine)
		{
		
			
			switch(quadrant){
			case 1:
				
				linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x +(mousePos.y - linePoints[m_lineIndex[m_lineID]].y);
				if(linePoints[m_lineIndex[m_lineID]+1].x<mousePos.x){
					
					linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y +(mousePos.x - linePoints[m_lineIndex[m_lineID]].x);
					linePoints[m_lineIndex[m_lineID]+1].x = (mousePos.x);
				}
				
				
				
				if(angleDegrees>=90 && angleDegrees<=180)
				{//4th q
					
					linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x;
					linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
					quadrant = 2;
					//Debug.Log("from 1 to 2");
				}
				if(angleDegrees>=270 && angleDegrees<=360)
				{//4th q
					
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 4;
					//Debug.Log("from 1 to 4");
				}
				
				break;
				
				
			case 2:
				if(mousePos.x > linePoints[m_lineIndex[m_lineID]].x){
					linePoints[m_lineIndex[m_lineID]+1].y= mousePos.y-(mousePos.x-linePoints[m_lineIndex[m_lineID]].x) ;  
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y= mousePos.y+(mousePos.x-linePoints[m_lineIndex[m_lineID]].x) ;  
				}
				if(angleDegrees>=0 && angleDegrees<=90 && (linePoints[m_lineIndex[m_lineID]+1].y < linePoints[m_lineIndex[m_lineID]].y ))
				{
					linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x;
					
					quadrant = 1;
					//Debug.Log("from 2 to 1");
				}else if(angleDegrees>=90 && angleDegrees<=180 &&(linePoints[m_lineIndex[m_lineID]+1].y < linePoints[m_lineIndex[m_lineID]].y )){
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 21;
					//Debug.Log("from 2 to 21");
				}
				break;
				
			case 21:
				
				
				linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x -(mousePos.y - linePoints[m_lineIndex[m_lineID]].y);
				if(linePoints[m_lineIndex[m_lineID]+1].x>mousePos.x){
					
					linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y -(mousePos.x - linePoints[m_lineIndex[m_lineID]].x);
					linePoints[m_lineIndex[m_lineID]+1].x = (mousePos.x);
				}
				if(angleDegrees>=0 && angleDegrees<=90 && (linePoints[m_lineIndex[m_lineID]+1].y < linePoints[m_lineIndex[m_lineID]].y ))
				{
					linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x;
					//linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
					quadrant = 2;
					//Debug.Log("from 21 to 2");
				}
				
				if(angleDegrees>=180 && angleDegrees<=270)
				{//3rd
					
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y;
					linePoints[m_lineIndex[m_lineID]+1].x = mousePos.x;
					quadrant = 3;
					//Debug.Log("from 21 to 3");
				}
				
				break;
				
			case 3: 
				if( linePoints[m_lineIndex[m_lineID]+1].x<=linePoints[m_lineIndex[m_lineID]].x ){
					
					if(linePoints[m_lineIndex[m_lineID]+1].y > mousePos.y){
						linePoints[m_lineIndex[m_lineID]+1].x= mousePos.x-(mousePos.y-linePoints[m_lineIndex[m_lineID]].y) ;  
					}else{
						linePoints[m_lineIndex[m_lineID]+1].x= mousePos.x+(mousePos.y-linePoints[m_lineIndex[m_lineID]].y) ;  
					}
					
				} 
				
				if(angleDegrees>=90 && angleDegrees<=180 && linePoints[m_lineIndex[m_lineID]].x < linePoints[m_lineIndex[m_lineID]+1].x )
				{
					//temp = mousePos;
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 21;
					//Debug.Log("from 3 to 2");
				}
				
				if(angleDegrees>=180 && angleDegrees<=270 && linePoints[m_lineIndex[m_lineID]].x < linePoints[m_lineIndex[m_lineID]+1].x )
				{
					//temp = mousePos;
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 31;
					//Debug.Log("from 3 to 32");
				}
				
				break;
			case 31:
				
				linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x +(mousePos.y - linePoints[m_lineIndex[m_lineID]].y);
				if(linePoints[m_lineIndex[m_lineID]+1].x>mousePos.x){
					
					linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y +(mousePos.x - linePoints[m_lineIndex[m_lineID]].x);
					linePoints[m_lineIndex[m_lineID]+1].x = (mousePos.x);
				}
				if((angleDegrees>=90 && angleDegrees<=180) )
				{//4th q
					
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y;
					linePoints[m_lineIndex[m_lineID]+1].x = mousePos.x;
					quadrant = 3;
					//Debug.Log("from 31 to 3");
				}
				if((angleDegrees>=270 && angleDegrees<=360) && linePoints[m_lineIndex[m_lineID]+1].x > linePoints[m_lineIndex[m_lineID]].x)
				{//4th q
					
					linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x;
					linePoints[m_lineIndex[m_lineID]+1].y = mousePos.y;
					quadrant =32;
					//Debug.Log("from 31 to 32");
				}
				break;
				
			case 32:
				if(mousePos.x > linePoints[m_lineIndex[m_lineID]].x){
					linePoints[m_lineIndex[m_lineID]+1].y= mousePos.y+(mousePos.x-linePoints[m_lineIndex[m_lineID]].x) ;  
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y= mousePos.y-(mousePos.x-linePoints[m_lineIndex[m_lineID]].x) ;  
				}
				
				
				if(angleDegrees>=270 && angleDegrees<=360 && linePoints[m_lineIndex[m_lineID]].y < linePoints[m_lineIndex[m_lineID]+1].y)
				{
					
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 41;
					//Debug.Log("from 32 to 41");
				}else 
					if(angleDegrees>=180 && angleDegrees<=270 && linePoints[m_lineIndex[m_lineID]].y < linePoints[m_lineIndex[m_lineID]+1].y )
				{
					
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 31;
					//Debug.Log("from 32 to 31");
				}
				break;
				
			case 4:
				
				
				if(linePoints[m_lineIndex[m_lineID]+1].y > mousePos.y){
					linePoints[m_lineIndex[m_lineID]+1].x= mousePos.x+(mousePos.y-linePoints[m_lineIndex[m_lineID]].y) ;  
				}else{
					linePoints[m_lineIndex[m_lineID]+1].x= mousePos.x-(mousePos.y-linePoints[m_lineIndex[m_lineID]].y) ;  
				}
				
				linePoints[m_lineIndex[m_lineID]+1].y= linePoints[m_lineIndex[m_lineID]].y;
				
				
				if(angleDegrees>=0 && angleDegrees<=90 && (linePoints[m_lineIndex[m_lineID]+1].x< linePoints[m_lineIndex[m_lineID]].x ))
				{
					
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 1;
					//Debug.Log("from 4 to 1");
				}else
					if( angleDegrees>=270 && angleDegrees<=360 && ( linePoints[m_lineIndex[m_lineID]+1].x< linePoints[m_lineIndex[m_lineID]].x ) )
				{
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 41;
					//Debug.Log("from 4 to 41");
				}
				
				
				break;
				
			case 41:
				
				
				linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x -(mousePos.y - linePoints[m_lineIndex[m_lineID]].y);
				if(linePoints[m_lineIndex[m_lineID]+1].x<mousePos.x){
					
					linePoints[m_lineIndex[m_lineID]+1].y = (mousePos.y);
				}else{
					linePoints[m_lineIndex[m_lineID]+1].y = linePoints[m_lineIndex[m_lineID]].y -(mousePos.x - linePoints[m_lineIndex[m_lineID]].x);
					linePoints[m_lineIndex[m_lineID]+1].x = (mousePos.x);
				}
				
				if((angleDegrees>=0 && angleDegrees<=90)  )
				{//4th q
					
					linePoints[m_lineIndex[m_lineID]+1] = mousePos;
					quadrant = 4;
					//Debug.Log("from 41 to 4");
				}
				
				if(angleDegrees>=180 && angleDegrees<=270 )
				{
					
					linePoints[m_lineIndex[m_lineID]+1].x = linePoints[m_lineIndex[m_lineID]].x;
					linePoints[m_lineIndex[m_lineID]+1].y = mousePos.y;
					quadrant = 32;
					//Debug.Log("from 41 to 32");
				}
				
				break;
				
			}
	
		}
		
		if( Input.GetMouseButtonUp(0)  )
		{
			if(!m_canDraLine)
				return;

			if (Physics.Raycast (ray,out hitRelease ,Mathf.Infinity )) 
			{
				if( hitRelease.collider.tag=="station" )
				{
					m_startEnd[1] = hitRelease.collider.gameObject.name;
					m_newStartEnd[m_lineID,1] = hitRelease.collider.gameObject.name;
//					m_allStartEnd[m_lineID] = m_startEnd;
					m_allStartEnd.Insert (m_lineID, m_startEnd);

					//						print("INDEX at mouse up before= "+index);
					screenPosUp  = screenCamera.WorldToScreenPoint (hitRelease.collider.transform.position );
					
					linePoints[m_lineIndex[m_lineID]+2] = screenPosUp;//  new Vector2 ( hitRelease.collider.transform.position.x ,hitRelease.collider.transform.position.z ); 

					Ray rayCurvepoint = screenCamera.ScreenPointToRay (new Vector3(linePoints[m_lineIndex[m_lineID]+1].x,linePoints[m_lineIndex[m_lineID]+1].y ,0));	
					RaycastHit hitCurvePoint;
					
					if( Physics.Raycast(rayCurvepoint ,out hitCurvePoint ,500 ))
					{
//							print("HIt base = "+hitCurvePoint.point );
						curvePoint =  hitCurvePoint.point;
					}

					increaseLinePntIndex (2);
					m_currentLineStart = m_lineIndex[m_lineID];

					// Don't overflow the points array
					if (m_lineIndex[m_lineID] == line[m_lineID].points2.Length-1) {
						//			index--;
						endReached = true;
					}

					Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>m_lineID= "+m_lineID);

					m_allLineDictionary.Remove (m_lineID);
					m_allLineDictionary.Add (m_lineID, linePoints);
					line[m_lineID].endCap = m_capTexNames[m_lineID];
					line[m_lineID].Draw();

//					drawAllLines ();

//					Vector2[] tempVect = getPointsForLine (m_lineID);

					// here to set the train track
					canSetTrack = true;
					for(int i=0; i<linePoints.Length; i++) {
						Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>VABlinePoints["+i+"]"+linePoints[i]);
//						Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>m_lineID= "+m_lineID+" tempVect["+i+"]"+tempVect[i]);
					}
					if(m_lineID == 0) {			//Vaibhav Need to apply for all lines 
						NewController.Instance.setTheTrack(Input.mousePosition);
						
						NewController.Instance.ForTrain.SetActive(true);
						NewController.Instance.setTrainAcive (m_lineID);
						
						NewController.Instance.BeforeSetTrack (m_lineID);
					}
					
				}
				else
				{
					if(m_lineID == 0) {
						NewController.Instance.RemoveAddedObject(m_lineID) ;
					}

					if(m_newStartEnd[m_lineID,1] == "NULL") {
						m_newStartEnd[m_lineID,0] = "NULL";
					}

//					VectorLine.Destroy (ref line[m_lineID]);

					removeExtraLine ();
//					SetLine();
				}
			}
			m_canDraLine = false;
	}
		
		if(m_canDraLine)
		{
//			for(int i=0; i<linePoints.Length; i++) {
//				Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>linePoints["+i+"]= "+linePoints[i]);
//			}
//			Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>mousePos= "+mousePos);

			linePoints[m_lineIndex[m_lineID]+2] = mousePos;
			line[m_lineID].points2 = linePoints;
//			line[m_lineID].endCap = m_capTexNames[m_lineID];
			line[m_lineID].Draw ();
		
		}


		// Rotate around midpoint of screen.  This could also be accomplished by rotating the line[m_lineID].vectorObject.transform instead,
		// in which case we'd just need to use Vector.DrawLine(line[m_lineID]) without the transform. However, we can use the transform to rotate about
		// any axis, not just Z, and the line[m_lineID] will still be drawn correctly. Try changing "forward" to "right", for example.
		//transform.RotateAround(Vector2(Screen.width/2, Screen.height/2), Vector3.forward, Time.deltaTime * rotateSpeed * Input.GetAxis("Horizontal"));
		
		if (oldWidth != Screen.width) {
			oldWidth = Screen.width;
			VectorLine.SetCamera(CameraClearFlags.Nothing);
//			print("For camera clear flags");
		}
	}

	void drawAllLines () {
		Vector2[] temp;
		for (int i=0; i<NuOfLines; i++) {
			temp = getPointsForLine (i);
			for(int j=0; j<temp.Length; j++) {
				Debug.Log ("Line["+i+"] and temp["+j+"]= "+temp[j]);
			}
			if(temp != null) {		// && i != m_lineID
				Debug.Log (">>>>>>>>>>>>>>>i= "+i);
				VectorLine.Destroy (ref line[i]);
				line[i] = null;
				
				line[i] = new VectorLine("Line", temp, lineColors, lineMaterial, 8.0f, LineType.Continuous, Joins.Fill);
				//		line[m_lineID].useViewportCoords = true;
				//		line[m_lineID].drawTransform = transform;
				line[i].collider = true;
				line[i].trigger = true;
				line[i].endCap = m_capTexNames[i];
				line[i].Draw ();
				RandomizeColor ( i );
			}
		}
	}

	void removeExtraLine () {
		Debug.Log ("m_lineIndex[m_lineID]= "+m_lineIndex[m_lineID]);
//		if(m_lineIndex[m_lineID]>=2)
//			m_lineIndex[m_lineID] -= 2;
//		int tempCount;
//		if(m_lineIndex[m_lineID] == 0) {
//			tempCount = m_lineIndex[m_lineID];
//		}else {
//			tempCount = m_lineIndex[m_lineID];
//		}

		Vector2[] tempArray;
//		Vector2[] tempArray = new Vector2[m_lineIndex[m_lineID]+1];
//		for(int i=0; i<tempArray.Length; i++) {
//			tempArray[i] = new Vector2(linePoints[i].x, linePoints[i].y);
//		}

		if(m_lineIndex[m_lineID] != 0) {
			tempArray = getPointsForLine (m_lineID);
		}else {
			tempArray = new Vector2[m_lineIndex[m_lineID]];
			for(int i=0; i<tempArray.Length; i++) {
				tempArray[i] = new Vector2(linePoints[i].x, linePoints[i].y);
			}
		}

		linePoints = null;
		linePoints = new Vector2[m_lineIndex[m_lineID]+1];
		linePoints[m_lineIndex[m_lineID]] = Vector2.zero;
		for(int i=0; i<tempArray.Length; i++) {
			linePoints[i] = new Vector2(tempArray[i].x, tempArray[i].y);
		}

		Debug.Log ("linePoints.Length= "+linePoints.Length);
		for(int i=0; i<linePoints.Length; i++) {
			Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>VAI::linePoints["+i+"]"+linePoints[i]);
		}

		VectorLine.Destroy (ref line[m_lineID]);
		line[m_lineID] = null;

		if(m_lineIndex[m_lineID] == 0) 
			return;

		line[m_lineID] = new VectorLine("Line", linePoints, lineColors, lineMaterial, 8.0f, LineType.Continuous, Joins.Fill);
		//		line[m_lineID].useViewportCoords = true;
		//		line[m_lineID].drawTransform = transform;
		line[m_lineID].collider = true;
		line[m_lineID].trigger = true;
		line[m_lineID].endCap = m_capTexNames[m_lineID];
		line[m_lineID].Draw();

		RandomizeColor ( m_lineID );
	}

	void resetLine (Vector2 _mousePos) {
		Vector2[] tempArray;

		if(m_lineIndex[m_lineID] != 0) {
			tempArray = getPointsForLine (m_lineID);
		}else {
			tempArray = new Vector2[m_lineIndex[m_lineID]];
			for(int i=0; i<tempArray.Length; i++) {
				tempArray[i] = new Vector2(linePoints[i].x, linePoints[i].y);
			}
		}

		linePoints = null;
		linePoints = new Vector2[m_lineIndex[m_lineID] + 3];
		for(int i=0; i<tempArray.Length; i++) {
			linePoints[i] = new Vector2(tempArray[i].x, tempArray[i].y);
		}

		linePoints[m_lineIndex[m_lineID]+1] = new Vector2(_mousePos.x, _mousePos.y);
		linePoints[m_lineIndex[m_lineID]+2] = new Vector2(_mousePos.x, _mousePos.y);

//		m_allLineDictionary.Add (m_lineID, linePoints);

//		Debug.Log ("linePoints.Length= "+linePoints.Length);
//		for(int i=0; i<linePoints.Length; i++) {
//			Debug.Log (">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>linePoints["+i+"]"+linePoints[i]);
//		}

		VectorLine.Destroy (ref line[m_lineID]);
		line[m_lineID] = null;
		
		line[m_lineID] = new VectorLine("Line", linePoints, lineColors, lineMaterial, 8.0f, LineType.Continuous, Joins.Fill);
		//		line[m_lineID].useViewportCoords = true;
//		line[m_lineID].drawTransform = transform;
		line[m_lineID].collider = true;
		line[m_lineID].trigger = true;

		RandomizeColor ( m_lineID );
	}

	public Vector2[] getPointsForLine (int _lineID) {
		Vector2[] temp;
		if(m_allLineDictionary.TryGetValue (m_lineID, out temp)) {
//			Debug.Log ("m_allLineDictionary.TryGetValue = SUCCESS");
		}else {
			temp = new Vector2[m_lineIndex[m_lineID]];
			for(int i=0; i<temp.Length; i++) {
				temp[i] = new Vector2(linePoints[i].x, linePoints[i].y);
			}
		}
		return temp;
	}

	public Vector2[] getScreenPntsForLine (int _lineID) {
		Vector2[] _tempVectr = getPointsForLine (_lineID);

//		Vector3[] _screenPnts = new Vector3[_tempVectr.Length];

		for (int i=0; i<_tempVectr.Length; i++) {
//			Debug.Log ("_tempVectr["+i+"]= "+_tempVectr[i]);
			_tempVectr[i] = screenCamera.WorldToScreenPoint ( new Vector3(_tempVectr[i].x, -43f, _tempVectr[i].y) );
		}


		return _tempVectr;
////		screenCamera.WorldToScreenPoint ( 
//		return screenCamera.WorldToScreenPoint ( new Vector3(_tempVectr.x, -43f, _tempVectr.y) );
	}

	int increaseLinePntIndex (int i=1) {
		return m_lineIndex[m_lineID]+=i;
	}

	void  addEndLine (){
		
		//print(" Three index "+linePoints[3]);
		
		// line[m_lineID].drawEnd(5);


		linePoints[3].x = linePoints[2].x;
		linePoints[3].y = linePoints[2].y+10;
		index++;
		linePoints[4].x = linePoints[2].x;
		linePoints[4].y = linePoints[2].y-10;
		// print(" Three index after add "+linePoints[3]);
		if (index == line[m_lineID].points2.Length-1) 
		{
			endReached = true;
		}
		
		line[m_lineID].maxDrawIndex = index;
		
		index++;
		//print("added........."+index);
	}
	
	
	public int trackColor;
	
	void  SetTrackColor ()
	{
		switch ( this.transform.tag )
		{
		case"line1": 
				trackColor=1;
			break;
			
		case"line2":
				trackColor=2; 
			break;
			
		case"line3":
				trackColor=3;
			break;
			
		case"line4":
				trackColor=4;
			break;
			
		case"line5":
				trackColor=5;
			break;
			
		case"line6":
				trackColor=6;
			break;
			
		case"line7":
				trackColor=7;
			break;
			
		case"line8":
				trackColor=8;
			break;
			
			
		}
	}
	
	void  RandomizeColor ( int trackColor ){

		Debug.Log (">>>>>>>>>>>>>>>>>trackColor= "+trackColor);
//		trackColor += 1;

		switch ( m_lineID )
		{
		case 0:
			line[m_lineID].SetColor (Color.red);
			
			break;
			
		case 1:
			line[m_lineID].SetColor (Color.blue);
			
			break;
			
		case 2:
			line[m_lineID].SetColor (new Color32( 254 ,209 ,94,255 ) );//yellow
			
			break;
			
		case 3:
			
			line[m_lineID].SetColor (new Color32( 0 ,152, 80,255 ) );//green
			
			break;
			
		case 4:
			line[m_lineID].SetColor (new Color32( 254,106,57,255) );//orange
			break;
			
		case 5:
			line[m_lineID].SetColor (new Color32( 0,161 ,210, 255 ) );//Sky blue
			break;
			
		case 6:
			line[m_lineID].SetColor (new Color32( 193 ,59, 165,255 ) );//LAveder
			break;
			
		case 7:
			line[m_lineID].SetColor (new Color32( 92 ,44, 125,255 ) );//Dark blue
			break;
			
			
			
			
		}
		
	}
	
	void  RandomizeAllColors (){
		for (int i= 0; i < lineColors.Length; i++) {
			lineColors[i] =new Color(Random.value, Random.value, Random.value);
		}
		line[m_lineID].SetColors (lineColors);
	}
}