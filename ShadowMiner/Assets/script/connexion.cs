using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class connexion : MonoBehaviour {
	public InputField pseudo;
	public InputField password;

	void Update ()
	{
		if (pseudo.text == "pseudo" && password.text == "password" && Input.GetKeyDown ("return")) {
			Debug.Log ("Logged as guest");
		
		}
		else {if (Input.GetKeyDown ("return")) {
				Debug.Log ("Wrong password or pseudo"); }
		}
	}
}