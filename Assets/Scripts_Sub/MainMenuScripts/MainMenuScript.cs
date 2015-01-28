using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public GameObject bgPlane;
	public Texture2D mainMenuBg;
	public Texture2D selectCityBg;
	public GameObject btnColliders;
	public GameObject citySelectColliedrs;
	Ray ray;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		StartCoroutine( WaitTimerForSplahScreen());

	}

	IEnumerator WaitTimerForSplahScreen()
	{
		yield return new WaitForSeconds(2f);
		StartCoroutine( OnMainMenuScreen());
	}
	IEnumerator OnMainMenuScreen()
	{
		bgPlane.renderer.material.mainTexture = mainMenuBg;
		btnColliders.SetActive(true);
		yield return new WaitForSeconds(1f);
	}


	void OnGUI()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if( Physics.Raycast( ray ,out hit ,100) )
		   {

			if( hit.collider.tag =="button" && Input.GetMouseButtonDown(0) )
			{
				print("clicked");
				bgPlane.renderer.material.mainTexture = selectCityBg;
				citySelectColliedrs.SetActive(true);
				btnColliders.SetActive(false);
			}

			if( hit.collider.tag =="citycollider" && Input.GetMouseButtonDown(0) )
			{
//				print("clicked");
				bgPlane.renderer.material.mainTexture = selectCityBg;
				Application.LoadLevel("NewYorkScene");
				citySelectColliedrs.SetActive(false);
			}
			
		}
//		if( bgPlane.renderer.material.mainTexture == mainMenuBg )
//		{
//			if ( gui.Button(0,0,480,270,IMAGE_TYPE.BTN_MAINMENU,IMAGE_TYPE.BTN_MAINMENU ) )
//			{
//				print("clicked");
//				bgPlane.renderer.material.mainTexture = selectCityBg;
//			}
//
//		}

//		if( bgPlane.renderer.material.mainTexture == selectCityBg )
//		{
//			if ( gui.Button(0,0,1024,600,IMAGE_TYPE.BTN_CITYSELECTION,IMAGE_TYPE.BTN_CITYSELECTION ) )
//			{
//				print("clicked");
//				Application.LoadLevel("NewYorkScene");
////				bgPlane.renderer.material.mainTexture = selectCityBg;
//			}
//			
//		}
	}

}
