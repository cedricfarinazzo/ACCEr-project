﻿using System.Collections;
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
		DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
		DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
		nouveauclient = new Client();
		logoutbutton.onClick.AddListener(Logout);
	}

	public void Logout(){
        SMProgress.Progress p = new SMProgress.Progress();
        p.SoloStats = 0;
        p.MultiStats = 0;
		nouveauclient.Logout ();
		SaveData.SaveData.DeleteKey("DataClient.Email");
		SaveData.SaveData.DeleteKey("DataClient.Token");
		SceneManager.LoadScene ("menu");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
