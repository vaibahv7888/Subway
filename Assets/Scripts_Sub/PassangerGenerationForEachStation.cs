using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassangerGenerationForEachStation : MonoBehaviour {

	public GameObject[] passangers;

	public List <GameObject>passangersList = new List<GameObject> ();

	public string stationType;
	// Use this for initialization
	void Start () {

//		offset = 0;
		stationType = this.transform.renderer.material.name.Substring(0,4);

		firstPlacePassanger = this.transform.position.x+1.15f;

		nextLanePlacePassangerZ = this.transform.position.z - 1.15f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	float offset;
	public int localPassangerCount;

	private float firstPlacePassanger;
	private float nextLanePlacePassangerZ;

//	private float xValue;

	public void generatePassanger(  )
	{
//		print ("Count = "+passangersList.Count );
		float HeightValue =  this.transform.position.z;


		GameObject passangerClone;


		int passangerNum = Random.Range( 0,this.passangers.Length ); // random passanger selection

		if( passangersList.Count < 5 )							// variable to maintain distance
		   {
				
			localPassangerCount++;

		

//					offset = offset+1.35f;	

			if( passangersList.Count == 0 )
			{
			passangerClone =  ( Instantiate( this.passangers [ passangerNum ].transform, 
			                                new Vector3 ( firstPlacePassanger,
				             this.transform.position.y,
				             HeightValue ), 
			                                this.passangers [ passangerNum ].transform.rotation )as Transform ).gameObject ;
			}
			else{

				passangerClone =  ( Instantiate( this.passangers [ passangerNum ].transform, 
				                                this.passangers [ passangerNum ].transform.position ,  
				                                this.passangers [ passangerNum ].transform.rotation )as Transform ).gameObject ;
			}

		
//			passangerClone.transform.name = "passangerClone"+localPassangerCount;

//			print(" Offset.... = "+passangerClone.name);
			this.passangersList.Add(passangerClone);

			setPassangetPosition(); // to set the positions of passangers

		}
		else
		{

//			if( passangersList.Count > 1)
//			{
//				removePassanger( Random.Range(0,4));
//			}

		}

	}

	
	public int numberOfPassangerRemoved;

	/*Function is called from method - GetAddPassangerInTrain
from script- PassangerInTrain
	  */

	public void removePassanger( int indexNo )
	{
		print("print....= "+indexNo +"passanger count "+ (passangersList.Count-1) );

		if( indexNo < this.passangersList.Count )
		{
		
		string objectName ="";//+this.passangersList[indexNo].name;

		this.passangersList[indexNo].name = "ObjectTodestroy";

		this.passangersList.RemoveAt(indexNo);

			
		
		Destroy ( GameObject.Find("ObjectTodestroy" ) );

		print ("objectto  destroyed "+ objectName );
		 
		numberOfPassangerRemoved++;
		localPassangerCount--;

		ScoreControllerScript.Instance.Score++;

		setPassangetPosition();

		}

	}


	public void setPassangetPosition()
	{
		for ( int i = 0;i< passangersList.Count;i++)
		{
			switch(i)
			{
			case 0:
				passangersList[0].transform.position = new Vector3(  firstPlacePassanger ,  this.transform.position.y ,this.transform.position.z);
				//xValue = passangersList[0].transform.position.x;
				break;

			case 1:
				passangersList[1].transform.position = new Vector3(  passangersList[0].transform.position.x+1.15f ,  this.transform.position.y ,this.transform.position.z);
				//xValue = passangersList[1].transform.position.x;
//				print ("Hereee......");
				break;

			case 2:
				passangersList[i].transform.position = new Vector3(  passangersList[i-1].transform.position.x+1.15f ,  this.transform.position.y ,this.transform.position.z);
				//xValue = passangersList[i].transform.position.x;
				break;

			case 3:
				passangersList[i].transform.position = new Vector3(  firstPlacePassanger ,  this.transform.position.y ,nextLanePlacePassangerZ);
				//xValue = passangersList[i].transform.position.x;
				break;

			case 4:
				passangersList[i].transform.position = new Vector3(  passangersList[i-1].transform.position.x+1.15f , this.transform.position.y ,nextLanePlacePassangerZ);
				//xValue = passangersList[i].transform.position.x;
				break;

			case 5:
				passangersList[i].transform.position = new Vector3(  passangersList[i-1].transform.position.x+1.15f , this.transform.position.y ,nextLanePlacePassangerZ);
				//xValue = passangersList[i].transform.position.x;
				break;

			case 6:
				passangersList[i].transform.position = new Vector3(  passangersList[i-1].transform.position.x+1.15f , this.transform.position.y ,nextLanePlacePassangerZ);
				//xValue = passangersList[i].transform.position.x;
				break;

			case 7:
				passangersList[i].transform.position = new Vector3(  passangersList[i-1].transform.position.x+1.15f , this.transform.position.y ,nextLanePlacePassangerZ);
				//xValue = passangersList[i].transform.position.x;
				break;

		}
	}
	}
}
