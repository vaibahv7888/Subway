    using UnityEngine;
    using System.Collections;
     
    public class WebcamTest : MonoBehaviour {
    public string deviceName;
    WebCamTexture wct;
     
     
	
	private static WebcamTest _instance = null;
	
	public static WebcamTest SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(WebcamTest)) as WebcamTest; 	
			}
			
			return _instance; 
		}
		set {
			
			_instance = value;
			
		}	
	}
	
	
	//Use this for initialization
    void Start () {
   		 WebCamDevice[] devices = WebCamTexture.devices;
   		 deviceName = devices[0].name;
   		 wct = new WebCamTexture(deviceName, 1280, 720, 12);
   		 renderer.material.mainTexture = wct;
   		 wct.Play();
    }
     
	
	
    // For photo varibles
     
    public Texture2D heightmap;
    public Vector3 size = new Vector3(100, 10, 100);
     
     
    void OnGUI() {
	gui.setGUIScale();	
   // if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
   // TakeSnapshot();
     
    }
     
    // For saving to the _savepath
    private string _SavePath = "C:/WebcamSnaps/"; //Change the path here!
    int _CaptureCounter = 0;
     
    public void TakeSnapshot()
    {
    
	try{	
		
	Texture2D snap = new Texture2D(wct.width, wct.height);
    snap.SetPixels(wct.GetPixels());
    snap.Apply();
     
    System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
    ++_CaptureCounter;
	
	
		}
		catch( System.Exception e){
			
			
		}
		GameStates.SetCurrent_State_TO(GAME_STATE.SHAREONFACEBOOK);	
		
}
   

}