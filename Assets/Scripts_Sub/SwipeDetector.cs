using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;

	public float minSwipeDistX;
		
	private Vector2 startPos;
	
//	public TextMesh swipeMotion_textMesh;

	void Update()
	{
//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
			
		{
			
			Touch touch = Input.touches[0];
			
			
			
			switch (touch.phase) 
				
			{
				
			case TouchPhase.Began:

				startPos = touch.position;
				
				break;
				
				
				
			case TouchPhase.Ended:

					float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

					if (swipeDistVertical > minSwipeDistY) 
						
					{
						
						float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
						
						if (swipeValue > 0)
					{
						//Jump ();//up swipe
//						if(!GUIManager.Instance.showPause)
//						PlayerManager.Instance.SausageJump();
						
					}

							
						
						else if (swipeValue < 0)
					{
						//down swipe
//						if(!GUIManager.Instance.showPause)
//						PlayerManager.Instance.SausageSlip();
					}
							//Shrink ();
						
					}
					
//					float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
//					
//					if (swipeDistHorizontal > minSwipeDistX) 
//						
//					{
//						
//						float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
//						
//						if (swipeValue > 0)//right swipe
//					{  
////						swipeMotion_textMesh.text = "for right 333 ";
//					}
//							//MoveRight ();
//						
//						else if (swipeValue < 0)//left swipe
//					{   
////						swipeMotion_textMesh.text = "for Lefttt 444 ";
//					}	
//							//MoveLeft ();
//						
//					}
				
				
				
				break;
			}
		}
	}
}