using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class LineManager : MonoBehaviour {

//	public Camera m_mainCam;
//	public Material m_lineMat;
//	public Material m_endCapMat;
//	public Texture2D m_endCapTexture;
//
//	List<Vector2> m_linePnts;
//
//	private VectorLine m_line;
//
//	int m_currntLinePnt = 0;
//	bool m_canDrawLine = false;
//
//	// Use this for initialization
//	void Start () {
//		init ();
//	}
//
//	Ray ray;
//	RaycastHit hit;
//	RaycastHit hitRelease;
//	
//	Vector2 temp;
//	int quadrant=-1; 
//	
//	public int count;
//	
//	//	Vector2 startPoint;
//
//	void init () {
//		m_linePnts = new List<Vector2>();
//		
//		VectorLine.SetCamera( m_mainCam , CameraClearFlags.Nothing , true );
//		VectorLine.SetEndCap ("endCap", EndCap.Mirror, m_endCapMat, m_endCapTexture);
//		Vector2[] temp = {new Vector2(0.1f, 0.6f), new Vector2(0.9f, 0.6f)};
//		m_line = new VectorLine("Line", temp, m_lineMat, 10.0f, LineType.Continuous, Joins.Fill);
////		m_line.useViewportCoords = true;
//		m_line.endCap = "endCap";
//		m_line.Draw ();
//	}
//
//	// Update is called once per frame
//	void Update () {
//	
//		// Since we can rotate the transform, get the local space for the current point, so the mouse position won't be rotated with the line
//		Vector2 mousePos = transform.InverseTransformPoint(Input.mousePosition);
//		
//		//get the vector representing the mouse's position relative to the point...
//		Vector2 v = mousePos - linePoints[m_lineIndex];
//		//use atan2 to get the angle; Atan2 returns radians
//		float angleRadians=Mathf.Atan2(v.y, v.x);
//		//convert to degrees
//		float angleDegrees= angleRadians*Mathf.Rad2Deg;
//		//angleDegrees will be in the range (-180,180].
//		//I like normalizing to [0,360) myself, but this is optional..
//		if (angleDegrees<0)
//			angleDegrees+=360;
//		
//		
//		ray = screenCamera.ScreenPointToRay (Input.mousePosition);
//		if (Physics.Raycast (ray, out hit, Mathf.Infinity )) 
//		{
//			
//		}
//
//		if ( Input.GetMouseButtonDown(0)
//		    && hit.collider.tag=="station")
//		{
//			m_canDrawLine = true;
//			Vector2 temp = new Vector2(mousePos.x, mousePos.y);
//
//			m_linePnts.Add (temp);
//			m_linePnts.Add (temp);
//			m_linePnts.Add (temp);
//
//			if(angleDegrees>=0 && angleDegrees<=90  )
//			{
//				quadrant = 1;
//				
//			}else if(angleDegrees>=90 && angleDegrees<=180  )
//			{
//				quadrant = 2;
//				
//			}else if(angleDegrees>=180 && angleDegrees<=270  )
//			{
//				quadrant = 3;
//				
//			}else if(angleDegrees>=270 && angleDegrees<=360  )
//			{
//				quadrant = 4;
//				
//			}
//
//			NewController.Instance.TrackCheckForNew( hit.collider.transform);
//
//			m_linePnts[m_currntLinePnt] = screenCamera.WorldToScreenPoint ( hit.collider.transform.position );
//		}
//
//		if(Input.GetMouseButton (0)) {
//
//		}
//
//	}
//
//
}
