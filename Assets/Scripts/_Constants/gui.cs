#define  UNITY_IPHONE

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class gui 
	
{
	public static bool isBannerAdsShow=true;
	static	GUIStyle style_Gui = new GUIStyle ();
	public static GUISkin skin_Gui;
	static	Rect rect = new Rect (0, 0, Constants.customWidth, Constants.customHeight) ;
	static 	Vector3 vect3 = new Vector3 (Screen.width / Constants.customWidth, Screen.height / Constants.customHeight, 0.1f);
	public static bool isBackKeyPressed;
	public static int backKeycount;
	public static void setGUIScale ()
	{
		GUI.matrix = Matrix4x4.Scale (vect3);
	}
	
	public static void setRect (float xPos, float yPos, float width, float height)
	{
		
		rect.x = xPos;
		rect.y = yPos;
		rect.width = width;
		rect.height = height;
		
	}
	
	static	Rect scalerect = new Rect (0, 0, Constants.customWidth, Constants.customHeight) ;
	static Vector2 vect2 = new Vector2 (0.0f, 0.0f);
	
	
	static bool getTouch (Rect tempRect , int fingerId = 0)
	{
		
		if ((Input.touches.Length>0 && fingerId >= 0 && Input.touches [fingerId].phase == TouchPhase.Ended && Input.touches [0].tapCount == 1) || (Application.isEditor && Input.GetMouseButton (0))) {
		
			vect2.x = Application.isEditor ? Input.mousePosition.x : Input.touches [fingerId].position.x;
			vect2.y = (float)(Screen.height - (Application.isEditor ? Input.mousePosition.y : Input.touches [fingerId].position.y));
		
			if (ScaleRect (tempRect).Contains (vect2)) {
                        return true;
			}
		}	
		
		return false;
	}
	
	public static bool Contains(float xPos, float yPos, float width, float height, int fingerId = 0){
		
		
		if ((Input.touches.Length>0 && fingerId >= 0 && Input.touches [fingerId].phase == TouchPhase.Ended && Input.touches [fingerId].tapCount == 1 ) || (Application.isEditor && Input.GetMouseButton (0))) {
	    	setRect (xPos, yPos, width, height);
			vect2.x = Application.isEditor ? Input.mousePosition.x : Input.touches [fingerId].position.x;
			vect2.y = (float)(Screen.height - (Application.isEditor ? Input.mousePosition.y : Input.touches [fingerId].position.y));
		
			if (ScaleRect (rect).Contains (vect2)) {
			
                        return true;
				
			}
		}	
		return false;
	}
	
	public static bool Contains(Rect rectangle , int fingerId = 0){
		
		
		if ((Input.touches.Length>0 && fingerId >=0 && Input.touches [fingerId].phase == TouchPhase.Ended && Input.touches [fingerId].tapCount == 1 ) || (Application.isEditor && Input.GetMouseButton (0))) {
	    	
			vect2.x = Application.isEditor ? Input.mousePosition.x : Input.touches [fingerId].position.x;
			vect2.y = (float)(Screen.height - (Application.isEditor ? Input.mousePosition.y : Input.touches [fingerId].position.y));
		
			if (ScaleRect (rectangle).Contains (vect2)) {
					
                        return true;
			}
		}	
		return false;
	}
	
	public static bool Contains_POS(Rect rectangle , int fingerId = 0){
		
		
		if ((Input.touches.Length>0 && fingerId >= 0 ) || (Application.isEditor && Input.GetMouseButton (0))) {
	    	
			vect2.x = Application.isEditor ? Input.mousePosition.x : Input.touches [fingerId].position.x;
			vect2.y = (float)(Screen.height - (Application.isEditor ? Input.mousePosition.y : Input.touches [fingerId].position.y));
		
			if (ScaleRect (rectangle).Contains (vect2)) {
					
                        return true;
			}
		}	
		return false;
	}
	
	public static Rect ScaleRect (Rect tempRect)
	{
		
		scalerect.x = (tempRect.x * Screen.width) / Constants.customWidth;
		scalerect. y = (tempRect.y * Screen.height) / Constants.customHeight;
		scalerect.width = (tempRect.width * Screen.width) / Constants.customWidth;
		scalerect.height = (tempRect.height * Screen.height) / Constants.customHeight;
   
	
		return scalerect;
	}
	
	public static bool Button (float xPos, float yPos, float width, float height, IMAGE_TYPE normalImage_Id, IMAGE_TYPE activeImage_Id,bool isTouchEnable = false, Transform audioSource = null, string str_text = "")
	{
		
		setRect (xPos, yPos, width, height);
		
		style_Gui.normal.background = Images.getTexture (normalImage_Id);
		style_Gui.active.background = Images.getTexture (activeImage_Id);
		
		if (((GUI.Button (rect, str_text, style_Gui)  && (!isBackKeyPressed ||normalImage_Id == IMAGE_TYPE.BTN_NO_NORMAL ||normalImage_Id == IMAGE_TYPE.BTN_YES_NORMAL)  ) || (isBackKeyPressed && (normalImage_Id == IMAGE_TYPE.BTN_BACK_NORMAL || normalImage_Id == IMAGE_TYPE.BTN_BACK_RIGHT_NORMAL  ))|| (getTouch (rect) && isTouchEnable) ) ) {
			
			if (normalImage_Id == IMAGE_TYPE.BTN_BACK_NORMAL || normalImage_Id == IMAGE_TYPE.BTN_BACK_RIGHT_NORMAL ){
			isBackKeyPressed = false;
			}

			
			if (audioSource == null && GameManager.SharedInstance)
				audioSource = GameManager.SharedInstance.transform;
			MySound.playSound (SOUND_CLIP.BUTTON_CLICK, audioSource);
			return true;
		}
		
		return false;
		
	}
	
	public static bool Button (float xPos, float yPos, float width, float height, string str_text = "")
	{
		
		setRect (xPos, yPos, width, height);
			
		if (!isBackKeyPressed  && GUI.Button (rect, str_text)   ) {
//			if(GameManager.SharedInstance){
//			MySound.playSound (SOUND_CLIP.BUTTON_CLICK, GameManager.SharedInstance.transform);
//			}
			return true;
		}
		
		return false;
		
	}

	public static bool Button (float xPos, float yPos, float width, float height, string str_text, GUIStyle style)
	{
		
		setRect (xPos, yPos, width, height);
		
		
//		if (!isBackKeyPressed  && GUI.Button (rect, str_text, style)  ) {
//			MySound.playSound (SOUND_CLIP.BUTTON_CLICK, GameManager.SharedInstance.transform);
//			return true;
//		}
		
		return false;
		
	}
	
	public static bool RepeatButton (float xPos, float yPos, float width, float height, IMAGE_TYPE normalImage_Id, IMAGE_TYPE activeImage_Id, string str_text = "")
	{
		
		setRect (xPos, yPos, width, height);
		
		style_Gui.normal.background = Images.getTexture (normalImage_Id);
		style_Gui.active.background = Images.getTexture (activeImage_Id);
		
		
		if (GUI.RepeatButton (rect, str_text, style_Gui)  ) {
			return true;
		}
		
		return false;
		
	}
	
	public static bool ToggleButton (float xPos, float yPos, float width, float height, IMAGE_TYPE offImage_Id, IMAGE_TYPE onImage_Id, bool toggleFlag,Transform audioSource = null)
	{
		
		setRect (xPos, yPos, width, height);	
		if (gui.Contains(rect)) {
			
			if (audioSource == null)
				audioSource = GameManager.SharedInstance.transform;
			MySound.playSound (SOUND_CLIP.BUTTON_CLICK, audioSource);
			
		}
		return   GUI.Toggle (rect, toggleFlag, Images.getTexture (toggleFlag == true ? onImage_Id : offImage_Id), GUIStyle.none);
	}
	public static bool ToggleButton (float xPos, float yPos, float width,float height, string On_text,string OFF_text, bool toggleFlag,Transform audioSource = null)
	{
		
		setRect (xPos, yPos, width, height);	
			if (gui.Contains(rect)) {
			
			if (audioSource == null)
				audioSource = GameManager.SharedInstance.transform;
			MySound.playSound (SOUND_CLIP.BUTTON_CLICK, audioSource);
			
		}
		return   GUI.Toggle (rect, toggleFlag, (toggleFlag == true ? On_text : OFF_text));
	}

	
	public static void Window (int id, float xPos, float yPos, float width, float height, GUI.WindowFunction windFun, string str_text , GUIStyle style)
	{
		setRect (xPos, yPos, width, height);
		
		GUI.Window (id, rect, windFun, str_text, style);
	}
	
	public static void DrawTexture (float xPos, float yPos, float width, float height, IMAGE_TYPE Image_Id)
	{
		

////Debug.Log(">>>>>>>> image = "+Image_Id);
		setRect (0, 0, 0, 0);
		GUI.Button (rect, "", GUIStyle.none);
		setRect (xPos, yPos, width, height);
		
		
		GUI.DrawTexture (rect, Images.getTexture (Image_Id));
		
	
	}	
	public static void DrawTexture (float xPos, float yPos, float width, float height, Texture2D tex)
	{
		

////Debug.Log(">>>>>>>> image = "+Image_Id);
		setRect (0, 0, 0, 0);
		
		setRect (xPos, yPos, width, height);
		
		
		GUI.DrawTexture (rect, tex);
		
	
	}	
public static string TextField( float xPos, float yPos, float width, float height,string str_text,GUIStyle style)
	{
		
		setRect (xPos, yPos, width, height);
		
	return	GUI.TextField(rect,str_text,style);
		
	}
	
public static string PasswordField( float xPos, float yPos, float width, float height,string str_text, char mask,GUIStyle style)
	{
		
		setRect (xPos, yPos, width, height);
		
	return	GUI.PasswordField(rect,str_text,mask,style);
		
	}	
		
public static string TextField( float xPos, float yPos, float width, float height,string str_text)
	{
		
		setRect (xPos, yPos, width, height);
		
	return	GUI.TextField(rect,str_text);
		
	}
	
public static string PasswordField( float xPos, float yPos, float width, float height,string str_text, char mask)
	{
		
		setRect (xPos, yPos, width, height);
		
	return	GUI.PasswordField(rect,str_text,mask);
		
	}	
//		public static void Box (float xPos, float yPos, float width, float height, IMAGE_TYPE Image_Id)
//	{
//		setRect (0, 0, 0, 0);
//		
//		setRect (xPos, yPos, width, height);			
//		GUI.Box (rect, Images.getTexture (Image_Id));
//		
//	
//	}	
	public static void  Label (float xPos, float yPos, float width, float height, string str_text, int FontSize, Color fontColor, TextAnchor alignment )
	{
		if (skin_Gui == null) {
			skin_Gui = Resources.Load ("FontSkin/FontSkin") as GUISkin;
		}
		
	
		skin_Gui.label.normal.textColor = fontColor;
		skin_Gui.label.fontSize = FontSize;
		skin_Gui.label.alignment = alignment;
		setRect (xPos, yPos, width, height);
		
		GUI.Label (rect, str_text, skin_Gui.label);
	}
	
	public static void  Label (float xPos, float yPos, float width, float height, string str_text, int FontSize, Color fontColor)
	{
		if (skin_Gui == null) {
			skin_Gui = Resources.Load ("FontSkin/FontSkin") as GUISkin;
		}
		
	
		skin_Gui.label.normal.textColor = fontColor;
		skin_Gui.label.fontSize = FontSize;
		skin_Gui.label.alignment = TextAnchor.UpperLeft;
		setRect (xPos, yPos, width, height);
		
		GUI.Label (rect, str_text, skin_Gui.label);
	}
	
	public static void  Label (float xPos, float yPos, float width, float height, string str_text, int FontSize)
	{
		
		if (skin_Gui == null) {
			skin_Gui = Resources.Load ("FontSkin/FontSkin") as GUISkin;
		}
		
		skin_Gui.label.normal.textColor = Color.white;
		skin_Gui.label.fontSize = FontSize;
		skin_Gui.label.alignment = TextAnchor.UpperLeft;
		setRect (xPos, yPos, width, height);
		
		GUI.Label (rect, str_text, skin_Gui.label);
	}
	
	public static void  Label (float xPos, float yPos, float width, float height, string str_text)
	{
		
		if (skin_Gui == null) {
			skin_Gui = Resources.Load ("FontSkin/FontSkin") as GUISkin;
		}
		skin_Gui.label.normal.textColor = Color.white;
		skin_Gui.label.alignment = TextAnchor.UpperLeft;
		setRect (xPos, yPos, width, height);
		
		GUI.Label (rect, str_text, skin_Gui.label);
	}
	
	public static void BeginGroup (float xPos, float yPos, float width, float height, string str_text = "")
	{
		
		setRect (xPos, yPos, width, height);
		
		GUI.BeginGroup (rect, str_text);
	}
	
	public static void EndGroup ()
	{
		GUI.EndGroup ();
	}

	public static void	SetQualityLevel ()
	{
	
		#if UNITY_IPHONE	
//		if (iPhoneSettings.generation == iPhoneGeneration.iPhone) {
//			QualitySettings.currentLevel = QualityLevel.Simple;	
//			
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPhone3G) {
//			QualitySettings.currentLevel = QualityLevel.Fastest;			
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPhone3GS) {
//			QualitySettings.currentLevel = QualityLevel.Fastest;			
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch1Gen) {			
//			QualitySettings.currentLevel = QualityLevel.Fastest;			
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch2Gen) {			
//			QualitySettings.currentLevel = QualityLevel.Fastest;			
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch3Gen) {				
//			QualitySettings.currentLevel = QualityLevel.Fastest;
//		} else if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen) {
//			QualitySettings.currentLevel = QualityLevel.Fastest;
//		} else {
//			QualitySettings.currentLevel = QualityLevel.Simple;
//		}
		#endif	
	}
	#if DEBUG_
	public static void DrawArrow(Vector3 pos, Vector3 direction,float arrowLength  = 50.0f,float arrowHeadLength = 0.25f, float arrowHeadAngle = 30.0f)
    {
        Debug.DrawRay(pos, direction,Color.red,arrowLength);
       
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength,Color.green,arrowLength);
        Debug.DrawRay(pos + direction, left * arrowHeadLength,Color.green,arrowLength);
    }
#endif


}
