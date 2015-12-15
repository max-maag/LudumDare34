using UnityEngine;
using System;
using System.Collections;

public class BlockControl : MonoBehaviour {
	private const string UP_BUTTON_NAME = "up";
	private const string DOWN_BUTTON_NAME = "down";
	private const string BLOCK_TAG = "block";
	private const string BLOCK_LAYER = "level geometry";

	public float maxSpeed = 1f;
	public float blockSwitchCooldown = 0.25f;

	public Color lockedInColor;
	public Color selectedColor;

	private float lastSwitchTime;
	private Rigidbody2D currentBlock;

	const string ANIMATOR_STATE_PARAMETER = "animState";
	const int STATE_SELECTED = 1;
	const int STATE_LOCKED = 2;

	// Use this for initialization
	void Start () {
		switchBlock();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentBlock == null) {
			switchBlock ();
			return;
		}

		if(Time.time - lastSwitchTime < blockSwitchCooldown)
			return;

		Collider2D c = currentBlock.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();

		bool shouldSwitchBlocks =
			currentBlock.gameObject.transform.position.x + c.bounds.extents.x <=
				GameObject.FindWithTag(Tags.PLAYER_TAG).transform.position.x ||
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
			setBlockState(currentBlock.gameObject, STATE_LOCKED);
		}

		GameObject[] blocks = GameObject.FindGameObjectsWithTag(BLOCK_TAG);

		if(blocks.Length == 0) {
			currentBlock = null;
			return;
		}

		Array.Sort(blocks, (a,b) => (int) Math.Sign(a.transform.position.x - b.transform.position.x));
		
		for(int i=0; i<blocks.Length; i++) {
			currentBlock = blocks[i].GetComponent<Rigidbody2D>();
			if(!currentBlock.isKinematic) {
				// set selection animation
				setBlockState(currentBlock.gameObject, STATE_SELECTED);
				break;
			} else
				currentBlock = null;
		}

		lastSwitchTime = Time.time;
	}

	private void setBlockState(GameObject block, int state) {
		for(int i=0; i<block.transform.childCount; i++) {
			Transform singleBlock = block.transform.GetChild(i);
			if(LayerMask.LayerToName(singleBlock.gameObject.layer).Equals(BLOCK_LAYER)) {
				singleBlock.transform.GetChild(0).GetComponent<Animator>().SetInteger(ANIMATOR_STATE_PARAMETER, state);
			}
		}
	}

	void OnPlayerDeath() {
		if(currentBlock != null)
			currentBlock.velocity = Vector2.zero;
		
		enabled = false;
	}
}
