using SMParametre;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour {

    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected Text countText;
    [SerializeField]
    protected Text RoomNameText;

    [SerializeField] protected GameObject gameManager;


    private GameObject PhotonPlayer;
    private bool joined = false;

    // Use this for initialization
    void Start () {
        Parametre param = SMParametre.Parametre.Load();
        if (!PhotonNetwork.connectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings(param.Version);
            PhotonNetwork.offlineMode = false;
        }
        PhotonNetwork.JoinLobby();
    }

    public void SetText()
    {
        this.countText.text = "Total player: " + PhotonNetwork.room.playerCount.ToString() + "/ 3";
    }

	// Update is called once per frame
	void Update () {
        /*
        try
        {*/

	    try
	    {
	        if (this.joined)
	        {
	            this.RoomNameText.text = "Room name : " + PhotonNetwork.room.Name;
	            this.SetText();
	        }
            if (PhotonNetwork.room.PlayerCount == 3)
            {
                MoveToGame();
                this.gameObject.SetActive(false);
            }
	    }
	    catch (Exception)
	    {
            
	    }

        /*}
        catch (UnityException)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
        }
        catch (Exception)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
        }*/
	}

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void JoinLobby()
    {
        RoomOptions opt = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 3
        };
        System.Random tools = new System.Random();
        string random = tools.Next().ToString().Substring(0, 2);
        PhotonNetwork.JoinOrCreateRoom(SceneManager.GetActiveScene().name+"=="+random, opt, TypedLobby.Default);
    }

    private void MoveToGame()
    {
        if (joined)
        {
            gameManager.SetActive(true);
        }
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
        j.GetComponent<NetworkCharacter>().enabled = true;
        j.GetComponentInChildren<CursorTurnVerti>().enabled = true;
        j.GetComponentInChildren<Camera>().enabled = true;
        j.GetComponentInChildren<AudioListener>().enabled = true;
        this.joined = true;
        PhotonPlayer = j;
    }
}
