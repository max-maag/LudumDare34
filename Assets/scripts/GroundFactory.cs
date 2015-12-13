using UnityEngine;
using System.Collections;

public class GroundFactory : MonoBehaviour
{
	public static GroundFactory instance;
	private const string EARTH = "GroundEarth";

	private GroundFactory () {}

	void Awake () {
		instance = this;
	}

	/// <summary>
	/// Returns an earth ground (bottom half of the screen is ground).
	/// </summary>
	/// <returns>The earth.</returns>
	/// <param name="xLeft">The x coordinate destination (left).</param>
	/// <param name="yTop">The y coordinate destination (top).</param>
	/// <param name="width">Width.</param>
	public GameObject getEarth (float xLeft, float yTop, float width) {
		GameObject earth = (GameObject) Instantiate (Resources.Load (EARTH), Vector3.zero, Quaternion.identity);

		Vector3 size = earth.GetComponent<Collider2D> ().bounds.size;
		float yBottomOfScreen = Camera.main.ViewportToWorldPoint (Vector2.zero).y;
		float desiredWorldHeight = yTop - yBottomOfScreen;
		float yScaleToFitScreen = desiredWorldHeight / size.y;
		earth.transform.localScale = new Vector3 (width, yScaleToFitScreen);
		Vector3 sizeAfterScaling = earth.GetComponent<Collider2D> ().bounds.size;
		earth.transform.position = new Vector3 (sizeAfterScaling.x / 2 + xLeft, yBottomOfScreen + desiredWorldHeight / 2);
		return earth;
	}

	// TODO add GroundTunnel

}
