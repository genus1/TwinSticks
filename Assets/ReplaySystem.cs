using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 200;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private Rigidbody rigidBody;
	private GameManager manager;
	private int startFrame, endFrame;
	private bool firstLoop;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		manager = GameObject.FindObjectOfType<GameManager>();
		firstLoop = true;
	}

	void Update () {
		if (manager.enableRecording){  //Fix to update firstLoop when changing enable
			if (manager.recording) {
				Record ();
			} else {
				PlayBack ();
			}
		}

	}

	void PlayBack () {
		rigidBody.isKinematic = true;
		int frameNow = Time.frameCount;
		int frame;
		int modLength = endFrame - startFrame;

		firstLoop = true;
		if (modLength < bufferFrames){  //buffer not full  Some glitch on short frame but recovers
			frame = (frameNow + (startFrame % modLength)) % modLength;
		}else{
			frame = (frameNow - startFrame) % bufferFrames;   //Starts at the start frame modulo
		}

		transform.position = keyFrames [frame].position;
		transform.rotation = keyFrames [frame].rotation;
	}

	void Record () {
		if (firstLoop) {
			startFrame = Time.frameCount;
			endFrame = startFrame;
			firstLoop = false;
			StoreFrame (startFrame);
		} else {
			endFrame = Time.frameCount;
			StoreFrame (endFrame);
		}
	}

	void StoreFrame (int thisFrame)
	{
		rigidBody.isKinematic = false;
		int frame = thisFrame % bufferFrames;
		float time = Time.time;
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
	}

	/// <summary>
	///  A structure for storing time, rotation and position;
	/// </summary>
	public struct MyKeyFrame{
		public float frameTime;
		public Vector3 position;
		public Quaternion rotation;

		public MyKeyFrame (float aTime, Vector3 aPosition, Quaternion aRotation){
			frameTime = aTime;
			position = aPosition;
			rotation = aRotation;
		}
	}
}
