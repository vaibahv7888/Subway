using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassangerController_Script : MonoBehaviour {

	public List <GameObject> AllActiveStationsList = new List<GameObject> ();

	public GameObject[] passangers;

	static PassangerController_Script myinstance;

	public static PassangerController_Script Instance {
		get {
			if (myinstance == null)
				myinstance = FindObjectOfType (typeof(PassangerController_Script)) as PassangerController_Script;
			
			return myinstance;
		}	
	}
	// Use this for initialization
	void Start () {

//		StartCoroutine( generatePassanger() );
	
	}
	int PassangerCount;

	public IEnumerator generatePassanger()
	{

		yield return new WaitForSeconds(2);
		PassangerCount++;

		int stationForPassangerNum = Random.Range( 0,AllActiveStationsList.Count );
		
		AllActiveStationsList[stationForPassangerNum].GetComponent<PassangerGenerationForEachStation>().generatePassanger( );// call to passangerGenerationForEachStation script

		yield return new WaitForSeconds(0.5f);

		StartCoroutine( generatePassanger() );

	}

	// Update is called once per frame
	void Update () {
	
	}
}
