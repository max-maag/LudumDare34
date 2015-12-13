using UnityEngine;
using System.Collections;

public class Utils {

	public static void SendGlobalMessage(string message)
	{
		foreach(GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			go.SendMessage(message, SendMessageOptions.DontRequireReceiver);
		}
	}
}
