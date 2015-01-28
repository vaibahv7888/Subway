using UnityEngine;
using System.Collections;

public  class ScaleRect : MonoBehaviour {

public static float customWidth  = 270f; //Set this value to the Width of your Game Tab in the Editor
public static float customHeight  = 480f; //Set this value to the Height of your Game Tab in the Editor
	public static  Rect returnRect;
	public static Rect scaledRect (float x , float y,  float width , float height ) {
  
    x = (x*Screen.width) / customWidth;
    y = (y*Screen.height) / customHeight;
    width = (width*Screen.width) / customWidth;
    height = (height*Screen.height) / customHeight;
   
    returnRect = new Rect(x,y,width,height);
    return returnRect;

	} 
	
	public static float scaleWidth(float width){
		 width = (width*Screen.width) / customWidth;
		return width;
	}
	
	
	public static float scaleHeight(float height){
		height = (height*Screen.height) / customHeight;
		return height;
	}
}
 