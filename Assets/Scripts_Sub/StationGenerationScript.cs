using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationGenerationScript : MonoBehaviour {

	static StationGenerationScript myinstance;

	public List <GameObject>FirstCicleRemainingStations = new List<GameObject>();

	public int count;
	public static StationGenerationScript Instance
	{
		get
		{
			if (myinstance == null)
				myinstance = FindObjectOfType(typeof(StationGenerationScript)) as StationGenerationScript;
			
			return myinstance;
		}	
	}

	public IEnumerator TimerToActiveStations()
	{
		yield return new WaitForSeconds(27f);

		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(47f);

		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(67f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(98f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(115f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(150f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(180f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(210f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(217f);
		
		GetRemovedActiveStations();
		GetActivateSingleStation();

		yield return new WaitForSeconds(235f);
		
		GetRemovedActiveStations();



	}


	void GetRemovedActiveStations()
	{

		
		for(int i=FirstCicleRemainingStations.Count - 1; i > -1; i--)
		{
			
			if( FirstCicleRemainingStations[i].active==true )
			{
				count++;
//				PassangerController_Script.Instance.AllActiveStationsList.Add( FirstCicleRemainingStations[i] );
				FirstCicleRemainingStations.RemoveAt(i); 		// will remove active objects from list
				
			}
			
		}

	}

	GameObject activatedStation;

	void GetActivateSingleStation()
	{
		GameObject tempActiveStation =	FirstCicleRemainingStations[Random.Range(0,FirstCicleRemainingStations.Count)];

		tempActiveStation.SetActive(true);

		PassangerController_Script.Instance.AllActiveStationsList.Add(tempActiveStation ); // to add station in active station list

	}
}
