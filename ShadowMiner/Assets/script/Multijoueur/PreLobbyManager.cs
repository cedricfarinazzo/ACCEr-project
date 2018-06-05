using System;
using System.Collections;
using System.Collections.Generic;
using SMNetwork.Client;
using SMParametre;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PreLobbyManager : Photon.MonoBehaviour
{

	private SMNetwork.Client.Client SMClient;
	
	[SerializeField] protected string[] gameList;
	
	// Use this for initialization
	void Start () {
		Parametre param = SMParametre.Parametre.Load();
		try
		{
			try
			{
                SMClient = new Client();
                SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
                SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
                SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
                var data = SMClient.AskMyProfil();
                if (data == null)
                {
                    SceneManager.LoadScene("connexion");
                    return;
                }
                else
                {
                    SaveData.SaveData.SaveString("Photon.playername", data.Login);
                }
            }
			catch(Exception)
			{
                Debug.Log ("Failed to join server");
				SceneManager.LoadScene ("failedNetwork");
            }

			if (!PhotonNetwork.connectedAndReady)
			{
				PhotonNetwork.ConnectUsingSettings(param.Version);
				PhotonNetwork.offlineMode = false;
			}
            PhotonNetwork.playerName = SaveData.SaveData.GetString("Photon.playername");
            string scenetoload = SaveData.SaveData.GetString("Multi.mode");
            string sceneName;

            if (scenetoload == "" || scenetoload == "new")
			{
				int n = Random.Range(0, gameList.Length);
				sceneName = gameList[n];
                System.Random tools = new System.Random();
                string random = tools.Next().ToString().Substring(0, 2);
                scenetoload = sceneName + "==" + random;
                SaveData.SaveData.SaveString("Multi.mode", scenetoload);
            }
            else
            {
                sceneName = scenetoload.Split('=')[0];
            }
            SaveData.SaveData.SaveString("Loader.Next", sceneName);
            SceneManager.LoadScene("loading");
			
		}
		catch (Exception)
		{
			Debug.Log("failed to join server: No network");
			SceneManager.LoadScene("failedNetwork");
		}
	}
}
