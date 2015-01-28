using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatedStations : MonoBehaviour {


	static ActivatedStations myinstance;

	public List <GameObject>ActivatedStationsList = new List<GameObject> ();
	
	
	public static ActivatedStations Instance {
		get {
			if (myinstance == null)
				myinstance = FindObjectOfType (typeof(ActivatedStations)) as ActivatedStations;
			
			return myinstance;
		}	
	}
 public void AddStationInList( GameObject stationToAdd )
	{
		ActivatedStationsList.Add( stationToAdd );
	}
}
