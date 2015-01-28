using UnityEngine;
using System.Collections;

public class ScoreControllerScript : MonoBehaviour {

	static ScoreControllerScript myinstance;
	
	public TextMesh scoreTextMesh ;
	public int Score = 0;
	public static ScoreControllerScript Instance
	{
		get
		{
			if (myinstance == null)
				myinstance = FindObjectOfType(typeof(ScoreControllerScript)) as ScoreControllerScript;
			
			return myinstance;
		}	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if( Score==0)
		{
			scoreTextMesh.text = "";
		}else
		{
			scoreTextMesh.text = ""+Score;

		}



	}
}
