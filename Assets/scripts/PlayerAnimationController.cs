using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	const string ANIMATOR_STATE_PARAMETER = "animState";

	const int STATE_RUN = 1;
	const int STATE_DIE = 2;
	const int STATE_FALL = 3;
	const int STATE_LAND = 4;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		changeState (STATE_RUN);
	}

	private void changeState(int state){
		if (state < STATE_RUN || state > STATE_LAND) {
			Debug.LogError ("Animation state out of range: was " + state + " valid is [" + STATE_RUN + ", " + STATE_LAND + "]");
		}
		animator.SetInteger (ANIMATOR_STATE_PARAMETER, state);
	}

	public void onTouchingFloorChanged(bool isTouchingFloorNow) {
		changeState (isTouchingFloorNow ? STATE_LAND : STATE_FALL);
	}

	public void onSpawn() {
		changeState (STATE_RUN);
	}

	public void onDie() {
		changeState (STATE_DIE);
	}
}
