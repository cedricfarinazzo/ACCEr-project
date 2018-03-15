using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    GameObject player;

    bool play = false;

	// Use this for initialization
	void Start () {
        Debug.Log("Start server");
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
            PhotonNetwork.offlineMode = false;
        }
       
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
    }
	
	// Update is called once per frame
	void Update () {
        /*
		if (Input.GetKey(KeyCode.A) && !play)
        {
            play = true;
            Instantiate(player, this.transform.position + Vector3.up * 2, Quaternion.identity);
        }*/

	}

    void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Joined room failed");
        PhotonNetwork.CreateRoom(null);
        //PhotonNetwork.JoinRoom("redroom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        GameObject j = PhotonNetwork.Instantiate(player.name, this.transform.position + Vector3.up * 2, Quaternion.identity, 0);
        j.GetComponent<PlayerController>().enabled = true;
        j.GetComponent<CursorTurnHory>().enabled = true;
        j.GetComponent<Entity>().enabled = true;
        j.GetComponentInChildren<CursorTurnVerti>().enabled = true;
        j.GetComponentInChildren<Camera>().enabled = true;
        j.GetComponentInChildren<AudioListener>().enabled = true;
    }


}
