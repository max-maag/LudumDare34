using UnityEngine;
using System.Collections;

public class Utils {

	public const string ON_PLAYER_DEATH = "OnPlayerDeath";

	public static void SendGlobalMessage(string message)
	{
		foreach(GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			go.SendMessage(message, SendMessageOptions.DontRequireReceiver);
		}
	}
}
