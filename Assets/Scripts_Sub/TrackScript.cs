using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackScript : MonoBehaviour {


	static TrackScript myinstance;
	
	public List <GameObject>StationsOnTrack = new List<GameObject>();
	
	public int count;
	public static TrackScript Instance
	{
		get
		{
			if (myinstance == null)
				myinstance = FindObjectOfType(typeof(TrackScript)) as TrackScript;
			
			return myinstance;
		}	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetTrack ( )
	{
		
//		if (hitRelease.transform != null) {
//			if (hit.transform.name != hitRelease.transform.name) {
//				print ("can set the track");
//				for (int i = 0; i < trackStations.Count; i++) {
//					if (trackStations [i].name == hitRelease.transform.name) {
//						cantAdd = true;
//					}
//				}
//				
//				if (!cantAdd && hitRelease.transform.tag == "station") {
//					trackStations.Add (hitRelease.transform.gameObject);
//					print ("Added = " + hitRelease.transform.gameObject);
//					
//					AddTargetToTrack (trackStations, trainObject);
//					
//					
//				}
//				
//				
//			} else {
//				print ("Cant set the track");
//			}
//			
//			cantAdd = false;
//		}
	}
}
