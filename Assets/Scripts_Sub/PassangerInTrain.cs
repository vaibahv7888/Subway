using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassangerInTrain : MonoBehaviour {

	static PassangerInTrain myinstance;
	
	public List <string>stationTypeInTrain = new List<string> ();// tria  , circ , star ,octa , hexa ,rect ,squa , diam

	public List <GameObject>passangersInTrain = new List<GameObject> ();// tria  , circ , star ,octa , hexa ,rect ,squa , diam

	public List <int>passangerIndexToRemove = new List<int> ();

	public string currentStationType;
	
	public static PassangerInTrain Instance
	{
		get
		{
			if (myinstance == null)
				myinstance = FindObjectOfType(typeof(PassangerInTrain)) as PassangerInTrain;
			
			return myinstance;
		}	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//function is called from Newcontroller
	public void GetAddPassangerInTrain( GameObject curentStation ) 
	{
		GameObject passangerToRemove;

		if( stationTypeInTrain.Count < 6 )
		{
			for( int i =0 ; i < curentStation.GetComponent<PassangerGenerationForEachStation>().passangersList.Count ;i++ )
			{


				for( int j=0;j< PassangerPickScript.Instance.stationType.Count ;j++)
				{

					if(  PassangerPickScript.Instance.stationType[j].ToString() == curentStation.GetComponent<PassangerGenerationForEachStation>().passangersList[i].tag )
					{

						/*Here should add passanger to trainPasssanger list 
						 *and remove that passanger from that station - curentStation.GetComponent<PassangerGenerationForEachStation>().removePassnger()
						 */

						passangerIndexToRemove.Add(i);
//						print("Train staions = "+ PassangerPickScript.Instance.stationType[j].ToString() );
//						print("Train staions = "+ curentStation.GetComponent<PassangerGenerationForEachStation>().passangersList[i].tag );
//						print ("Object shud Removed  index = "+ i +"   "+j);

					}

				}

			}

			for(int k= 0; k< passangerIndexToRemove.Count;k++ )
			{
				curentStation.GetComponent<PassangerGenerationForEachStation>().removePassanger( passangerIndexToRemove[k] );

				print ("Removed object index = "+ passangerIndexToRemove[k] );

			}


			passangerIndexToRemove.Clear();

			NewController.Instance.allPassangerPicked = true;

		}

		for ( int i = 0;i< stationTypeInTrain.Count ;i++ )
		{
			if( stationTypeInTrain.Count < 6 )
			{
//				stationTypeInTrain.Add(
			}

			if( currentStationType == stationTypeInTrain[i].ToString() )
			{

			}
		}


		switch(""+currentStationType)
		{

		case "tria":

			break;
		case "circ":
			break;
		case "star":
			break;
		case "octa":
			break;
		case "hexa":
			break;
		case "rect":
			break;
		case "squa":
			break;
		case "diam":
			break;

		}
	}
}
