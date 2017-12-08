using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording = true;
	
	// Update is called once per frame
	//This will create a huge data buffer if left recording
	void Update () {
		if (CrossPlatformInputManager.GetButton ("Fire1")){
			recording = false;
		}else{
			recording = true;
		}
	}
}
