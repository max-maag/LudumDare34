﻿using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	private readonly IMapSectionGenerator[] sectionGenerators = {
		new JumpSectionGenerator(),
		new RandomBlockGenerator(
			new NormalDistribution(0,2),
			new NormalDistribution(2,0.5),
			new NormalDistribution(4,3)
		)
	};

	/// x coordinate at which the next element should be placed at (or: x coordinate up to which the level is defined)
	private float xNextElement;

	// Use this for initialization
	void Start () {
		GameObject initialGround = GameObject.FindWithTag(Tags.GROUND_TAG);
		xNextElement = initialGround.GetComponent<Collider2D> ().bounds.max.x;
	}
	
	// Update is called once per frame
	void Update () {
		float xRightOfCamera = Camera.main.ViewportToWorldPoint(Vector3.right).x;
		while(xNextElement <= xRightOfCamera) {
			xNextElement = sectionGenerators[Random.Range(0, sectionGenerators.Length)].GenerateSection(0, xNextElement);
		}
	}
}
