using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Photon.MonoBehaviour {

    [SerializeField]
    GameObject player;

    // Use this for initialization
    void Start () {
        if (!PhotonNetwork.connectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
            PhotonNetwork.offlineMode = false;
        }
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount == 3)
        {
            MoveToGame();
        }
	}

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void JoinLobby()
    {
        RoomOptions opt = new RoomOptions()
        {
            isOpen = true,
            IsVisible = true,
            MaxPlayers = 3
        };
        PhotonNetwork.JoinOrCreateRoom("room" + (DateTime.Now.Ticks.ToString()), opt, TypedLobby.Default);
    }

    private void MoveToGame()
    {
        PhotonNetwork.LoadLevel("build_scene_cedric");
    }

    void OnPhotonRandomJoinFailed()
    {
        JoinLobby();
    }

    void OnJoinedRoom()
    {
        Instantiate();
    }

    public void Instantiate()
    {
        GameObject j = PhotonNetwork.Instantiate(player.name, this.transform.position + Vector3.up * 2, Quaternion.identity, 0);
        j.GetComponent<PlayerController>().enabled = true;
        j.GetComponent<CursorTurnHory>().enabled = true;
        j.GetComponent<Entity>().enabled = true;
        j.GetComponentInChildren<CursorTurnVerti>().enabled = true;
        j.GetComponentInChildren<Camera>().enabled = true;
        j.GetComponentInChildren<AudioListener>().enabled = true;
    }
}
