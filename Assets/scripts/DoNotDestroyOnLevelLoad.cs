using UnityEngine;
using System.Collections;

public class DoNotDestroyOnLevelLoad : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
