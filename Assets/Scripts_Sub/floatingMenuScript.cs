using UnityEngine;
using System.Collections;

public class floatingMenuScript : MonoBehaviour {

	public GameObject floatingMenu; 


	public Ray ray;									//The hit ray
	public RaycastHit hit;								//The hit raycast

	public bool isMenuOut;
	public TextMesh NewTrack_count;
	public TextMesh DoubleDaba_count;
	public TextMesh Tunnel_count;
	public TextMesh DoubleTrain_count;

	// Use this for initialization
	void Start () {
		NewTrack_count.offsetZ =2 ;
		Tunnel_count.offsetZ =2 ;
		DoubleDaba_count.offsetZ =2 ;
		DoubleTrain_count.offsetZ =2 ;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			//Cast a ray
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//If the ray hit something in the set layer
			if (Physics.Raycast (ray, out hit,Mathf.Infinity )) {

				if( hit.collider.name == "Menu_collider")
				{
					StartCoroutine( floatingMenuMovement() );
				}

				if( hit.collider.tag == "floatMenuButton")
				{
					switch( hit.collider.name )
					{
					case "doubleTrain":
						print (" doubleTrain Tapped ");
						break;
					case "doubleTrack":
						print (" doubleTrack Tapped ");
						break;
					case "tunnelTrack":
						print (" tunnelTrack Tapped ");
						break;
					case "NewTrack":
						print (" NewTrack Tapped ");
						break;

					}
				}
//				print ("Menu touched on = " + hit.collider.name);
			}
		}
	
	}

	IEnumerator floatingMenuMovement()
	{

		if( isMenuOut )
		{
			if( floatingMenu.transform.position.x > -29.95f )
			{
				this.renderer.enabled = false;
				floatingMenu.transform.Translate( Vector3.right * Time.deltaTime*5 );
				yield return new WaitForSeconds(0.01f);
				StartCoroutine( floatingMenuMovement() );
			}
			else
			{
				this.renderer.enabled = true;
				isMenuOut =false;
			}

		}
		else
		{
			if( floatingMenu.transform.position.x < -24f )
			{
				this.renderer.enabled = false;
				floatingMenu.transform.Translate( Vector3.left * Time.deltaTime *5 );
				yield return new WaitForSeconds(0.01f);
				StartCoroutine( floatingMenuMovement() );
			}
			else
			{	
				this.renderer.enabled = false;
				isMenuOut =true;
			}
		}
		yield return new WaitForSeconds(0.2f);
	}
}
