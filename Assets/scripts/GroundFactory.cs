using UnityEngine;
using System.Collections;

public class GroundFactory : MonoBehaviour
{
	public static GroundFactory instance;
	public const string GRASS = "Grass";
	public const string DIRT = "Dirt";
	public const string SNOW = "Snow";
	public const string STONE = "Stone";
	public const string SAND = "Sand";

	private const string MULTI_GROUND = "MultiGround";

	/// bonus height added to floating ground blocks to avoid skipping blocks by walking on top of the screen (à la Super Mario warp zone access)
	private const float ABOVE_CAMERA_HEIGHT_BONUS = 50f;

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
	/// <param name="tileset">What tileset to use.</param>
	public static GameObject GetGround (float xLeft, float yTop, float width, string tileset) {
		GameObject multi = (GameObject)Instantiate (Resources.Load (MULTI_GROUND), Vector3.zero, Quaternion.identity);
		GameObject top = (GameObject) Instantiate (Resources.Load (tileset + "Top"), Vector3.zero, Quaternion.identity);
		top.transform.parent = multi.transform;
		Vector3 sizeTop = top.GetComponent<Collider2D> ().bounds.size;
		top.transform.localPosition = new Vector3 (0, -sizeTop.y / 2);
		top.transform.localScale = new Vector3 (width, 1);

		GameObject center = (GameObject) Instantiate (Resources.Load (tileset + "Center"), Vector3.zero, Quaternion.identity);
		center.transform.parent = multi.transform;
		Vector3 sizeCenter = center.GetComponent<Collider2D> ().bounds.size;
		float yBottomOfScreen = Camera.main.ViewportToWorldPoint (Vector2.zero).y;
		float desiredWorldHeight = yTop - yBottomOfScreen;
		float yScaleToFitScreen = desiredWorldHeight / sizeCenter.y;
		center.transform.localScale = new Vector3 (width, yScaleToFitScreen);
		center.transform.localPosition = new Vector3 (0, - desiredWorldHeight / 2, 1);

		multi.transform.position = new Vector3 (width / 2 + xLeft, yTop);
		return multi;
	}

	public static GameObject GetFloatingGround(float xLeft, float yBottom, float width, string tileset) {
		GameObject multi = (GameObject) Instantiate (Resources.Load (MULTI_GROUND), Vector3.zero, Quaternion.identity);
		GameObject ground = (GameObject) Instantiate (Resources.Load (tileset + "Center"), Vector3.zero, Quaternion.identity);
		ground.transform.parent = multi.transform;
		Vector3 size = ground.GetComponent<Collider2D> ().bounds.size;

		float yTopOfScreen = Camera.main.ViewportToWorldPoint (Vector2.one).y;
		float desiredWorldHeight = yTopOfScreen - yBottom + ABOVE_CAMERA_HEIGHT_BONUS;
		ground.transform.localScale = new Vector3 (width, desiredWorldHeight / size.y);
		ground.transform.localPosition = new Vector3 (0, desiredWorldHeight / 2);

		multi.transform.position = new Vector3 (width / 2 + xLeft, yBottom);
		return multi;
	}
}
