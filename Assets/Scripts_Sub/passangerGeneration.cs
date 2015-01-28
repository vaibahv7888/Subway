using UnityEngine;
using System.Collections;

public class passangerGeneration : MonoBehaviour {


	static passangerGeneration myinstance;
//	public GameObject Passanger_Triangle;
//	public GameObject Passanger_Circle;
//	public GameObject Passanger_Square;
	public GameObject[] passangers;

	public static passangerGeneration Instance {
		get {
			if (myinstance == null)
				myinstance = FindObjectOfType (typeof(passangerGeneration)) as passangerGeneration;
			
			return myinstance;
		}	
	}
	// Use this for initialization
	void Start () {
	
		StartCoroutine( generatePassanger() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int count;
	public IEnumerator generatePassanger()
	{
//		print ("Hereeee...");
		int passangerNum = Random.Range( 0,passangers.Length ); // random passanger selection

		int stationForPassangerNem = Random.Range( 0,passangers.Length );

//		Transform passangerAtStation = ActivatedStations.Instance.ActivatedStationsList[ActivatedStations.Instance.ActivatedStationsList.Count-1].transform;
//		Instantiate(passangers [ passangerNum ], new Vector3(i * 2.0F, 0, 0), Quaternion.identity) as Transform;

		passangers [ passangerNum ].SetActive(true);

		passangers [ passangerNum ].transform.position = new Vector3 ( this.transform.position.x+(count+1.5f) ,this.transform.position.y ,this.transform.position.z) ;

		count++;

		yield return new WaitForSeconds(1);

//		StartCoroutine( generatePassanger() );

	}

}
