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
    protected GameObject monster;
    [SerializeField]
    protected Text countText;
    [SerializeField]
    protected Text RoomNameText;
    [SerializeField]
    protected Text Title;
    [SerializeField]
    protected Text List;

    [SerializeField] protected GameObject gameManager;
    [SerializeField] protected GameObject minuteur;


    public GameObject PhotonPlayer;
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
	    try
	    {
	        if (this.joined)
	        {
	            this.RoomNameText.text = "Room name : " + PhotonNetwork.room.Name;
	            this.SetText();
	        }
            if (PhotonNetwork.room.PlayerCount == 3)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    PhotonNetwork.room.IsOpen = false;
                }
                MoveToGame();
                Title.text = "Multijoueur Game";
            }
            string playerslist = "PLayer List : \n" + SaveData.SaveData.GetString("Photon.playername") + "\n";
            var players = PhotonNetwork.otherPlayers;
            Debug.Log(PhotonNetwork.room.PlayerCount);
            foreach(var player in players)
            {
                playerslist += player.NickName + "\n";
            }
            List.text = playerslist;
	    }
	    catch (Exception e)
	    {
            
	    }
	}

    void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
        JoinLobby();
    }

    void JoinLobby()
    {
        RoomOptions opt = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 3
        };
        string roomname = SaveData.SaveData.GetString("Multi.mode");
        try
        {
            PhotonNetwork.JoinOrCreateRoom(roomname, opt, TypedLobby.Default);
        }
        catch (Exception)
        {
            //full or error
            SceneManager.LoadScene("joinroomerror");
        }
        
    }

    private void MoveToGame()
    {
        if (joined)
        {
            minuteur.SetActive(true);
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
        GameObject j;
        if (PhotonNetwork.room.PlayerCount == 3)
        {
            j = PhotonNetwork.Instantiate(monster.name, this.transform.position + Vector3.up * 2 + Vector3.back * PhotonNetwork.room.PlayerCount, Quaternion.identity, 0);
            j.GetComponent<SMplayer>().enabled = true;
            j.name = "ShadowMiner";
        }
        else
        {
            j = PhotonNetwork.Instantiate(player.name, this.transform.position + Vector3.up * 2 + Vector3.back * PhotonNetwork.room.PlayerCount, Quaternion.identity, 0);
            j.GetComponent<PlayerController>().enabled = true;
            j.name = "Miner";
        }
        
        j.GetComponent<CursorTurnHory>().enabled = true;
        j.GetComponent<Entity>().enabled = true;
        j.GetComponent<NetworkCharacter>().enabled = true;
        j.GetComponentInChildren<CursorTurnVerti>().enabled = true;
        j.GetComponentInChildren<Camera>().enabled = true;
        j.GetComponentInChildren<AudioListener>().enabled = true;
        j.GetComponentInChildren<PauseManager>().enabled = true;
        this.joined = true;
        PhotonPlayer = j;
        SaveData.SaveData.DeleteKey("Multi.mode");
    }
}
