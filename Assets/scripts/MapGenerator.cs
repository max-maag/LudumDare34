using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag(PLAYER_TAG);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
