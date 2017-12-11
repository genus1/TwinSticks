using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	//Once public must be set in inspector or under script
	public bool recording;  //should be true
	public bool enableRecording;  //should start false
	private float initialFixedDelta;
	private bool isPaused = false;

	void Start(){
		PlayerPrefsManager.UnlockLevel (2);
		print(PlayerPrefsManager.IsLevelUnlocked(1));
		print(PlayerPrefsManager.IsLevelUnlocked(2));
		initialFixedDelta = Time.fixedDeltaTime;
		print (initialFixedDelta);
	}

	// Update is called once per frame
	//This will create a huge data buffer if left recording
	void Update () {
		if (enableRecording) {  //Create interface button to enable record/playback
			if (CrossPlatformInputManager.GetButton ("Fire1")){
				recording = false;
			}else{
				recording = true;
			}
		}

		//Below only work on PC programmed keys.  Need to update status of objects
		if (Input.GetKeyDown (KeyCode.P) && isPaused) {
			isPaused = false;
			ResumeGame ();
		} else if (Input.GetKeyDown (KeyCode.P) && !isPaused) {
			isPaused = true;
			PauseGame ();
		}

	}

	void PauseGame () {
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;
	}

	void ResumeGame () {
		Time.timeScale = 1f;
		Time.fixedDeltaTime = initialFixedDelta;
	}

	//if the system pauses my game it invokes my pause
	void OnApplicationPause (bool pauseStatus) {
		isPaused = pauseStatus;
	}
}
