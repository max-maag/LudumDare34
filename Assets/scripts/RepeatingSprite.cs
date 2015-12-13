using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RepeatingSprite : MonoBehaviour {
	//public SpriteRenderer sprite;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.y);
	}
	
	// Update is called once per frame
	// or when scene is changed in editor
	void Update () {
		if(Application.isEditor && spriteRenderer != null)
			Start();
	}
}
