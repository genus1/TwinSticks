using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	//Once public must be set in inspector or under script
	public bool recording;  //should be true
	public bool enableRecording;  //should start false

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
	}
}
