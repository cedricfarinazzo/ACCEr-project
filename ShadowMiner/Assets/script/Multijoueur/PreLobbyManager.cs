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
			{SMClient = new Client();}
			catch(Exception)
			{Debug.Log ("Failed to join server");
				SceneManager.LoadScene ("failedNetwork");}
			SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
			SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
			SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
			if (SMClient.AskMyProfil() == null)
			{
				SceneManager.LoadScene("connexion");
				return;
			}
			if (!PhotonNetwork.connectedAndReady)
			{
				PhotonNetwork.ConnectUsingSettings(param.Version);
				PhotonNetwork.offlineMode = false;
			}

			string sceneName = "";
			foreach (var room in PhotonNetwork.GetRoomList())
			{
				if (room.IsOpen && room.PlayerCount != room.MaxPlayers)
				{
					string name = room.Name;
					sceneName = name.Split('=')[0];
					break;
				}
			}

			if (sceneName == "")
			{
				int n = Random.Range(0, gameList.Length);
				sceneName = gameList[n];
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
