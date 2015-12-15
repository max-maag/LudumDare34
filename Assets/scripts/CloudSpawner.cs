using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {
	public GameObject cloud;
	public Sprite[] clouds;

	private readonly NormalDistribution dist = new NormalDistribution(1, 0.5);

	private float nextGen;
	// Use this for initialization
	void Start () {
		nextGen = Time.time + (float) dist.NextNormal();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextGen) {
			Debug.Log("Spawning");
			GameObject go = Instantiate(cloud);
			SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
			renderer.sprite = clouds[Random.Range(0, clouds.Length)];

			Vector3 rightCenter = Camera.main.ViewportToWorldPoint(Vector3.right);
			Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

			Debug.Log(rightCenter);
			Debug.Log(rightTop);

			go.transform.position += rightCenter + new Vector3(
				renderer.sprite.bounds.extents.x,
				Random.Range(0f, rightTop.y - rightCenter.y) + renderer.sprite.bounds.max.y,
				10f);

			go.GetComponent<CloudBehaviour>().speed = Random.Range(0f, 0.3f);

			nextGen += (float) dist.NextNormal();
		}
	}

	void OnPlayerDeath() {
		enabled = false;
	}
}
