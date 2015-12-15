using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageCopyFade : MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		GetComponent<Image>().color = transform.parent.GetComponent<Text>().color;
	}
}
