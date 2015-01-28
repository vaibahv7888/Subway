using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PassangerPickScript : MonoBehaviour {



	static PassangerPickScript myinstance;

	public List <string>stationType = new List<string> ();// tria  , circ , star ,octa , hexa ,rect ,squa , diam
	 
	public static PassangerPickScript Instance
	{
		get
		{
			if (myinstance == null)
				myinstance = FindObjectOfType(typeof(PassangerPickScript)) as PassangerPickScript;
			
			return myinstance;
		}	
	}


	// Use this for initialization
	void Start () {


	}


	// Update is called once per frame
	void Update () {
	

	}

	/*This function is called when new track is set*/
	public void addStationType() 
	{
		stationType.Clear();
		for ( int i = 0;i< NewController.Instance.trackStationsInList.Count ;i++ )
		{

			if( i%2==0 )
			{

//				print ("count = "+NewController.Instance.trackStationsInList[i].renderer.material.name.Substring(0,4));
				stationType.Add( NewController.Instance.trackStationsInList[i].renderer.material.name.Substring(0,4) );

				
	       	}
			
		}
	

	}


}
