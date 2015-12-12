using UnityEngine;
using System;
using System.Collections;

public class BlockControl : MonoBehaviour {
	private const string UP_BUTTON_NAME = "up";
	private const string DOWN_BUTTON_NAME = "down";
	private const string BLOCK_TAG = "block";

	public float maxSpeed = 1f;
	public float blockSwitchCooldown = 0.25f;

	private float lastSwitchTime;
	private Rigidbody2D currentBlock;

	// Use this for initialization
	void Start () {
		switchBlock();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentBlock == null)
			return;
		
		if(Time.time - lastSwitchTime < blockSwitchCooldown)
			return;

		bool shouldSwitchBlocks =
			Input.GetButton(UP_BUTTON_NAME) &&
			Input.GetButton(DOWN_BUTTON_NAME);

		float vel = 0f;

		if(shouldSwitchBlocks) {
			switchBlock();
			if(currentBlock == null)
				return;
		} else if(Input.GetButton(UP_BUTTON_NAME)) {
			vel = maxSpeed;
		} else if(Input.GetButton(DOWN_BUTTON_NAME)) {
			vel = -maxSpeed;
		}

		currentBlock.velocity = new Vector2(currentBlock.velocity.x, vel);
	}

	void switchBlock() {
		if(currentBlock != null) {
			currentBlock.velocity = Vector2.zero;
			currentBlock.isKinematic = true;
		}
		
		Debug.Log("Switching Blocks");
		GameObject[] blocks = GameObject.FindGameObjectsWithTag(BLOCK_TAG);

		if(blocks.Length == 0) {
			Debug.Log("No next blog found.");
			currentBlock = null;
			return;
		}

		Array.Sort(blocks, (a,b) => (int) Math.Sign(a.transform.position.y - b.transform.position.y));

		for(int i=0; i<blocks.Length; i++) {
			currentBlock = blocks[0].GetComponent<Rigidbody2D>();
			if(!currentBlock.isKinematic)
				break;
			else
				currentBlock = null;
		}

		lastSwitchTime = Time.time;

		if(currentBlock == null)
			Debug.Log("No next blog found.");
	}
}
