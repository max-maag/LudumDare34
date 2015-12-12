using UnityEngine;
using System.Collections;

public class GroundFactory : MonoBehaviour
{

	public static GroundFactory instance;
	private const string EARTH = "GroundEarth";

	private GroundFactory () {}

	void Start () {
		instance = this;
	}

	/// <summary>
	/// Returns an earth ground (bottom half of the screen is ground).
	/// </summary>
	/// <returns>The earth.</returns>
	/// <param name="width">Width.</param>
	/// <param name="xLeft">The x coordinate destination (left).</param>
	/// <param name="yTop">The y coordinate destination (top).</param>
	public GameObject getEarth (float width, float xLeft, float yTop) {
		GameObject earth = (GameObject) Instantiate (Resources.Load (EARTH), Vector3.zero, Quaternion.identity);

		Vector3 size = earth.GetComponent<Collider2D> ().bounds.size;
		float y0 = Camera.main.ViewportToWorldPoint (Vector2.zero).y;
		float desiredWorldHeight = yTop - y0;
		float yScaleToFitScreen = desiredWorldHeight / size.y;
		earth.transform.localScale = new Vector3 (width, yScaleToFitScreen);
		Vector3 sizeAfterScaling = earth.GetComponent<Collider2D> ().bounds.size;
		earth.transform.position = new Vector3 (sizeAfterScaling.x / 2, y0 + desiredWorldHeight / 2);
		return earth;
	}

	// TODO add GroundTunnel

}
