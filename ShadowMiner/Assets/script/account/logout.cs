using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SMNetwork.Client;
using UnityEngine.SceneManagement;

public class logout : MonoBehaviour {

	Client nouveauclient;
	public Button logoutbutton;
	// Use this for initialization
	void Start () {
		nouveauclient = new Client();
		logoutbutton.onClick.AddListener(Logout);
	}

	public void Logout(){
		nouveauclient.Logout ();
		SceneManager.LoadScene ("connexion");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
