
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MySound : MonoBehaviour
{
	public bool soundON, musicON;
	private bool commentaryON;
	public Hashtable SoundClip  = new Hashtable();
	
	private static MySound _instance = null;
	
	public static MySound SharedInstance {
		get {
			// if the instance hasn't been assigned then search for it
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(MySound)) as MySound; 	
			}			
			return _instance; 
		}
	}
	
	void Awake() {		
		
		soundON = (PlayerPrefs.GetInt ("soundON") == 0 ? true : false);
		musicON = (PlayerPrefs.GetInt ("musicON") == 0? true : false);	
		commentaryON = (PlayerPrefs.GetInt ("musicON") == 0? true : false);	
	}
	
	public void setSoundMode(bool soundMode){
		
		soundON = soundMode;
		PlayerPrefs.SetInt ("soundON" ,(soundON== true? 0 : 1));		
		setVolume(soundON?1:0);
	}
	
	public void setCommentaryMode(bool commentaryMode){
		
		commentaryON = commentaryMode;
		PlayerPrefs.SetInt ("commentaryON" ,(commentaryON== true? 0 : 1));	
		
			for(int i=0;i<CommentryTransformList.Count;i++){
				if(CommentryTransformList[i] != null){
				((Transform)CommentryTransformList[i]).audio.volume = (commentaryON ? 1:0);
				}
			}
		
	}
	
	public void setMusicMode (bool musicMode){
		
		musicON = musicMode;
		PlayerPrefs.SetInt ("musicON" ,(musicON == true ? 0 : 1));
	}
	
	public static AudioClip getClip (SOUND_CLIP clipIndex) {
		
		if (SharedInstance.SoundClip [(int)clipIndex] == null) {
			SharedInstance.SoundClip [(int)clipIndex] = Resources.Load ("SOUNDS/"+clipIndex)  as  AudioClip;	
		}       
		return SharedInstance.SoundClip [(int)clipIndex]as  AudioClip;	
	}
	

	public static	void playSound (SOUND_CLIP clipIndex, Transform soundObject,bool loop = false) {
		
		if(soundObject == null || !SharedInstance.soundON){
			// setVolume(0);
			return;
		}
		
		AudioSource audioSrc;
		if(soundObject.audio == null){
			soundObject.gameObject.AddComponent<AudioSource>();
			
			audioSrc = soundObject.audio;
			audioSrc.loop = loop;
		}else{
			audioSrc = soundObject.audio;
			audioSrc.loop = loop;
		}
		audioSrc.enabled = true;
	
		if (SharedInstance.SoundClip [(int)clipIndex] == null) {
		
			SharedInstance.SoundClip [(int)clipIndex] = Resources.Load ("SOUNDS/"+clipIndex)  as  AudioClip;	
#if DEBUG
			////Debug.Log("Sound File Loaded  =======================>"+clipIndex);
#endif
		}
		if (audioSrc == null || !audioSrc.enabled ||!SharedInstance.soundON || SharedInstance.SoundClip [(int)clipIndex] == null) {
			return;
		}
		
		if (!audioSrc.loop) {
			audioSrc.audio.Stop ();
			
		}
		            
		
		if (!audioSrc.audio.isPlaying ) {
			audioSrc.clip = SharedInstance.SoundClip [(int)clipIndex]as  AudioClip;	
			
			if(clipIndex == SOUND_CLIP.BUTTON_CLICK){
				audioSrc.audio.ignoreListenerPause = true;
				audioSrc.audio.ignoreListenerVolume = true;
			}
			
//			if(clipIndex.ToString().Contains("COMMENTARY") ){
//				
//				if(SharedInstance.commentaryON ){
//				  audioSrc.audio.Play ();
//				}
//			}
//			else{
			  audioSrc.audio.Play ();
//			}
#if DEBUG
		//	//Debug.Log("playSound  = ======================>"+clipIndex);
#endif
			//if (!audioSrc.loop)
			//SharedInstance.Remove (clipIndex, audioSrc.clip.length + 0.2f,audioSrc);
//			
				
		
//			if(clipIndex != SOUND_CLIP.COMMENTARY_INTRO &&  clipIndex.ToString().Contains("COMMENTARY") && InGamecontroller.SharedInstance!=null){
//				SharedInstance.setAudioSourceVolume (InGamecontroller.SharedInstance.transform,1,audioSrc.clip.length + 0.2f);
//				
//				audioSrc.audio.ignoreListenerPause = true;
//				audioSrc.audio.ignoreListenerVolume = true;
//				if(!CommentryTransformList.Contains(audioSrc.transform)){
//					CommentryTransformList.Add(audioSrc);
//				}
//			 }
			
		}
	}
	
	
	public static	void playCommentary (SOUND_CLIP clipIndex, Transform soundObject,bool loop = false) {
		
		if(soundObject == null || !SharedInstance.commentaryON){
			// setVolume(0);
			return;
		}
		
		AudioSource audioSrc;
		if(soundObject.audio == null){
			soundObject.gameObject.AddComponent<AudioSource>();
			
			audioSrc = soundObject.audio;
			audioSrc.loop = loop;
		}else{
			audioSrc = soundObject.audio;
			audioSrc.loop = loop;
		}
		audioSrc.enabled = true;
	
		if (SharedInstance.SoundClip [(int)clipIndex] == null) {
		
			SharedInstance.SoundClip [(int)clipIndex] = Resources.Load ("SOUNDS/"+clipIndex)  as  AudioClip;	
#if DEBUG
			////Debug.Log("Sound File Loaded  =======================>"+clipIndex);
#endif
		}
		if (audioSrc == null || !audioSrc.enabled ||!SharedInstance.commentaryON || SharedInstance.SoundClip [(int)clipIndex] == null) {
			return;
		}
		
		if (!audioSrc.loop) {
			audioSrc.audio.Stop ();
			
		}
		            
		
		if (!audioSrc.audio.isPlaying ) {
			audioSrc.clip = SharedInstance.SoundClip [(int)clipIndex]as  AudioClip;	
			
			if(InGameController.SharedInstance!=null){
				//SharedInstance.setAudioSourceVolume (InGamecontroller.SharedInstance.transform,1,audioSrc.clip.length + 0.2f);
				audioSrc.audio.ignoreListenerPause = true;
				audioSrc.audio.ignoreListenerVolume = true;
				if(!CommentryTransformList.ContainsKey(audioSrc)){
					CommentryTransformList.Add(audioSrc,audioSrc);
				}
			 }
			
			  audioSrc.audio.Play ();
			
#if DEBUG
		//	//Debug.Log("play Commentary  = ======================>"+clipIndex);
#endif
			
				
		
			
			
		}
	}
	
	
	
	public static Hashtable CommentryTransformList = new Hashtable();
	
	public static	void playSoundOneShot (SOUND_CLIP clipIndex) {
		
		if( !SharedInstance.soundON){
			// setVolume(0);
			return;
		}
		
		
	
		if (SharedInstance.SoundClip [(int)clipIndex] == null) {
		
			SharedInstance.SoundClip [(int)clipIndex] = Resources.Load ("SOUNDS/"+clipIndex)  as  AudioClip;	
#if DEBUG
		//	//Debug.Log("Sound File Loaded  =======================>"+clipIndex);
#endif
		}
		if (!SharedInstance.soundON || SharedInstance.SoundClip [(int)clipIndex] == null) {
			return;
		}
		
		
		
		InGameController.SharedInstance.audio.PlayOneShot(SharedInstance.SoundClip [(int)clipIndex] as AudioClip);          
	
	
	}
	
	public  void setAudioSourceVolume(Transform audioSrc,float Volume ,float sec){
		
		StartCoroutine ( setAudioSourceVolume_(  audioSrc, Volume , sec));
	}
	
	IEnumerator setAudioSourceVolume_( Transform audioSrc,float Volume ,float sec) {
		if(audioSrc!=null){
			audioSrc.audio.volume = 0;
		}
		yield return new WaitForSeconds(sec);
		if(audioSrc!=null){
			audioSrc.audio.volume = Volume;
		}
		
	}
	
	
	public static void  PlaySoundAfter(SOUND_CLIP clipIndex ,Transform soundObject , float sec,bool loop = false) {
		
		if(soundObject.audio == null){
			return;
		}		
		SharedInstance.startSound (clipIndex, sec,soundObject,loop);
	}
	
    public  void startSound (SOUND_CLIP clipIndex ,Transform soundObject , float sec,bool loop){
		
		StartCoroutine (startSound (clipIndex, sec,soundObject,loop));
	}	
	
	IEnumerator startSound(SOUND_CLIP clipIndex, float sec,Transform audioSrc,bool loop = false) {
		
		yield return new WaitForSeconds(sec);
		playSound ( clipIndex, audioSrc,loop);
		
	}
	
	public static void  StopSound(SOUND_CLIP clipIndex ,Transform soundObject){
		
		if(soundObject.audio == null){
			return;
		}
		soundObject.audio.Stop();
		SharedInstance.Remove (clipIndex, 0.0f,soundObject.audio);
	}
	
	
	
	
	public static void  StopSoundAfter(SOUND_CLIP clipIndex ,Transform soundObject , float sec) {
		
		if(soundObject.audio == null){
			return;
		}		
		SharedInstance.Remove (clipIndex, sec,soundObject.audio);
	}
	
	void Remove (SOUND_CLIP clipIndex, float sec,AudioSource audioSrc) {
		
		StartCoroutine (RemoveSoundFile (clipIndex, sec,audioSrc));
	}
	
	IEnumerator RemoveSoundFile (SOUND_CLIP clipIndex, float sec,AudioSource audioSrc) {
				
		yield return new WaitForSeconds(sec);
		
		if(audioSrc!=null && !audioSrc.isPlaying){
			audioSrc.Stop();
			Resources.UnloadAsset(audioSrc.clip);
			audioSrc.clip = null;
		}
		SoundClip [(int)clipIndex] = null;	
	}		
	
	public static void setVolume(float Volume){
		
        AudioListener.volume = Volume;
	
	}
	
   public  void PauseSound(){
		
		 AudioListener.pause = true;	
		 setVolume(0);
		 
	} 	
	
	public  void ResumeSound(){
		
		 AudioListener.pause = false;
		 setVolume(1);
		
	}
}
