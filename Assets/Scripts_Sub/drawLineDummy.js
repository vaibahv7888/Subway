import Vectrosity;
import UnityEngine.GUILayout;

var lineMaterial : Material;
var rotateSpeed = 90.0;
public var line : VectorLine;
public var index : int;
private var endReached : boolean;
private var linePoints : Vector2[];
private var lineColors : Color[];
private var continuous = true;
private var fillJoins = false;
private var weldJoins = false;
private var oldFillJoins = false;
private var oldWeldJoins = false;
private var thickLine = false;
private var canClick = true;
private var oldWidth : int;

public var canStartDraw:boolean;
public var screenCamera:Camera;
public var isLineCurved:boolean;


public var useTouch:boolean;
//public var newController:GameObject;
//var newController : NewController;
var screenPosUp : Vector3;
var screenPosDown : Vector3;
	
function Start () {
	oldWidth = Screen.width;
	
	
//				#if UNITY_EDITOR
//				useTouch= false;
//				#else
//				useTouch = true;
//				#endif
//	newController = GameObject.Find(NewController+"").GetComponent(NewController);
	
	VectorLine.Destroy (line);

	SetLine();
}

public function SetLine () {

	var lineType = (continuous? LineType.Continuous : LineType.Discrete);
	
//	var joins = (fillJoins? Joins.Fill : Joins.None);
	var joins = Joins.Fill ;
	var lineWidth = 8;//(thickLine? 24 : 2);

	linePoints = new Vector2[500];
	if (lineType == LineType.Continuous) {
		lineColors = new Color[linePoints.Length-1];
	}
	else {
		lineColors = new Color[linePoints.Length/2];
	}
	for (color in lineColors) color = Color.blue;

	line = new VectorLine("Line", linePoints, lineColors, lineMaterial, lineWidth, lineType, joins);
	line.drawTransform = transform;
	endReached = false;
	
	
	#if UNITY_EDITOR
				index = 0;
				#else
				index = -1;
				#endif
	
	
	
//	line.SetColor (Color(0.28f,0.28f, 0.28f));
	oldFillJoins = true;
	oldWeldJoins = true;
	fillJoins = true;
	weldJoins = true;
	
//	trackColor = Random.Range(1,8);
SetTrackColor();
RandomizeColor ( trackColor );

isMouseDown=false;
 	isMouseUp=false;
// RandomizeColor ( 3  );

}




var temp:Vector2;

var quadrant :int=-1; 

var ray:Ray;
var hit : RaycastHit;
var hitRelease : RaycastHit;

function Update () {



if(useTouch)
	getTouch();
else
	getClick();
//	
}

function getClick()
{
		
// Since we can rotate the transform, get the local space for the current point, so the mouse position won't be rotated with the line
var mousePos = transform.InverseTransformPoint( Input.mousePosition );

 

var mousePosX = mousePos.x;
var mousePosY = mousePos.y;
///////////////new 1
	 //get the vector representing the mouse's position relative to the point...
var v:Vector2 = mousePos - linePoints[0];
//use atan2 to get the angle; Atan2 returns radians
var angleRadians=Mathf.Atan2(v.y, v.x);
//convert to degrees
var angleDegrees = angleRadians*Mathf.Rad2Deg;
//angleDegrees will be in the range (-180,180].
//I like normalizing to [0,360) myself, but this is optional..
if (angleDegrees<0)
angleDegrees+=360;


ray = screenCamera.main.ScreenPointToRay (Input.mousePosition);


	// Set the current line point and color when the mouse is clicked
	
			if (Physics.Raycast (ray, hit ,Mathf.Infinity )) 
	{
	if ( Input.GetMouseButtonDown(0)  && hit.collider.tag=="station" ) {
	
	isMouseDown=true;
 	isMouseUp=false;
	
	 screenPosDown = screenCamera.WorldToScreenPoint (hit.collider.transform.position );
	
		print ("target is " + screenPosDown.x + " pixels from the left");
	
		index++;
		
//		linePoints[index] = mousePos ;
		linePoints[index] = screenPosDown ;
		print("screenPos = "+screenPosDown );
//		hit.collider.transform.position

		// Don't overflow the points array
		if (index == line.points2.Length-1) {
			index--;
			endReached = true;
		}
		
		index++;
		
		line.maxDrawIndex = index;
		
		if(index==1)
		{
//			temp = mousePos;
			
			temp = screenPosDown;
			print(temp );
		
//			temp =  linePoints[1];

		}
		
		
		}
	}
	

	
	if(index<=2 && isLineCurved){
	

if( linePoints[0].x > mousePos.x)
{


	if( linePoints[0].y > mousePos.y)
	{
//	print ("lower part");//

		linePoints[1] = new Vector2( temp.x+(mousePos.y +linePoints[0].y) ,linePoints[0].y+(mousePos.y-linePoints[0].y)  );
	
//			linePoints[1] = new Vector2( temp.x+(4*( mousePos.y) ) ,linePoints[0].y+(mousePos.y-linePoints[0].y)  ); 
	}
	else
	{
	
//	print ("Angle = "+ Mathf.DeltaAngle(linePoints[1].x,mousePos.x ));//Upper part
			linePoints[1] = new Vector2( temp.x-(mousePos.y -linePoints[0].y) ,linePoints[0].y+(mousePos.y-linePoints[0].y)  ); 
	
	}


}
else{

	if( linePoints[0].y > mousePos.y)
	{
//	print ("Angle = "+ Mathf.DeltaAngle(linePoints[1].x,mousePos.x ));
	//		if( linePoints[0].x-(mousePos.y+linePoints[0].y) > linePoints[0].x )//upper part
	//	{
			linePoints[1] = new Vector2( temp.x+(mousePos.y -linePoints[0].y) ,linePoints[0].y+(mousePos.y-linePoints[0].y)  ); 
	//	}
		
	}
	else
	{
//	print ("Angle = "+ Mathf.DeltaAngle(linePoints[1].x,mousePos.x ));
			linePoints[1] = new Vector2( temp.x-(mousePos.y -linePoints[0].y) ,linePoints[0].y+(mousePos.y-linePoints[0].y)  ); 
	
	}
}

	}
	// The current
//	 line point should always be where the mouse is
//if( isMouseDown)
//{
//linePoints[index] = screenPosDown;
//}
//else if ( isMouseUp )
//{
//linePoints[index] = screenPosUp;
//}
//else
//{
//	linePoints[index] = mousePos;
//	}
	
	linePoints[index] = mousePos;
	
	
	
	
	if( Input.GetMouseButtonUp(0) )
	{
	isMouseDown=false;
 	isMouseUp=true;
	
	if (Physics.Raycast (ray, hitRelease ,Mathf.Infinity )) 
	{
	
//	print("collider tag at mouse up = "+hit.collider.tag);
		if( hitRelease.collider.tag=="station" )
		{
//		print("INDEX at mouse up before= "+index);
 screenPosUp  = screenCamera.WorldToScreenPoint (hitRelease.collider.transform.position );
			index++;
			
			
//		linePoints[index] = mousePos;
		linePoints[index] = screenPosUp;
//		print("INDEX at mouse up = "+index);
		// Don't overflow the points array
		if (index == line.points2.Length-1) {
			index--;
			endReached = true;
		}

		line.maxDrawIndex = index;
		}
	}

	
	}
	
	
	#if UNITY_EDITOR
				
				if (index > 0 && index <=3 )
	{
		line.Draw ();
	}
	
	 if ( index >=3 ){
	
	SetLine();
	
	}
	#else
				if (index > 0 && index <=2 )
	{
		line.Draw ();

	}
	
	 if ( index >=2 ){
	
	SetLine();
	
	}
				#endif
	
	
	
	
	// Rotate around midpoint of screen.  This could also be accomplished by rotating the line.vectorObject.transform instead,
	// in which case we'd just need to use Vector.DrawLine(line) without the transform. However, we can use the transform to rotate about
	// any axis, not just Z, and the line will still be drawn correctly. Try changing "forward" to "right", for example.
	transform.RotateAround(Vector2(Screen.width/2, Screen.height/2), Vector3.forward, Time.deltaTime * rotateSpeed * Input.GetAxis("Horizontal"));
	
	if (oldWidth != Screen.width) {
		oldWidth = Screen.width;
		VectorLine.SetCamera();
	}
		
		
}


var isMouseDown:boolean;
var isMouseUp:boolean;



function getTouch()
{
var touch = Input.GetTouch(0);
var touchPos =  touch.position ;
//var touchPos = transform.InverseTransformPoint(Input.GetTouch(0).position );


//	if (Input.touchCount ==1 ) 
//		  
//		  {
		  
		  print("Train... single touch ");
		switch ( touch.phase )
		{
		
			case TouchPhase.Began :
			
				if ( canStartDraw ) 
		{
		
		 drawConditions();
		
		if(index==1)
		{
		
//			temp =  linePoints[1];

		}
}
			
			break;
			case TouchPhase.Moved :
			 print("Train... touch moved ");
			linePoints[index] = touchPos;
	
//				drawConditions();		
			
			break;
			case TouchPhase.Ended :
			 print("Train... sTouch ended ");
			
			
			if (Physics.Raycast (ray, hit ,Mathf.Infinity )) 
	{
	
		print("Train...collider tag = "+hit.collider.tag);
		if( hit.collider.tag=="station" )
		{
			index++;
		linePoints[index] = touchPos;
		print("INDEX at touch end = "+index);
		// Don't overflow the points array
		if (index == line.points2.Length-1) {
			index--;
			endReached = true;
		}
		
//		index++;
		line.maxDrawIndex = index;
		}
	}
	else
	{
	
	
	VectorLine.Destroy (line);
	
	print("Should reset line");
//	SetLine();
	
	}
	

			
			break;
			
		}
		
		
		if (index > 0 && index <2 )
	{
		line.Draw ();
	}
	else{
	
	SetLine();
	print("INDEX at Set line = "+index );
	}
	
	
//		}
}


function drawConditions()
{var touch = Input.GetTouch(0);
var touchPos =  touch.position ;

	index++;
		linePoints[index] = touchPos ;
//		print("INDEX1111 = "+index);
		// Don't overflow the points array
		if (index == line.points2.Length-1) {
			index--;
			endReached = true;
		}
		
//		index++;
//		print("INDEX22222 = "+index);
		line.maxDrawIndex = index;
	
	
	}
public var trackColor:int;

function SetTrackColor()
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

function RandomizeColor ( trackColor ) {

switch ( trackColor )
{
	case 1:
	line.SetColor (Color.red);
	break;
	
	case 2:
	line.SetColor (Color.blue);
	break;
	
	case 3:
	line.SetColor (Color32( 254 ,209 ,94,255 ) );//yellow
	break;
	
	case 4:
	
	line.SetColor (Color32( 0 ,152, 80,255 ) );//green
	
	break;
	
	case 5:
	line.SetColor ( Color32( 254,106,57,255) );//orange
	break;
	
	case 6:
	line.SetColor (Color32( 0,161 ,210, 255 ) );//Sky blue
	break;
	
	case 7:
	line.SetColor (Color32( 193 ,59, 165,255 ) );//LAveder
	break;
	
	case 8:
	line.SetColor (Color32( 92 ,44, 125,255 ) );//Dark blue
	break;
	
	


}

}

function RandomizeAllColors () {
	for (var i = 0; i < lineColors.Length; i++) {
		lineColors[i] = Color(Random.value, Random.value, Random.value);
	}
	line.SetColors (lineColors);
}