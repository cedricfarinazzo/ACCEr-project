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

    private bool joined = false;

    // Use this for initialization
    void Start () {
        Parametre param = SMParametre.Parametre.Load();
        try
        {
            if (!IsConnected())
            {
                throw new Exception();
            }
            if (!PhotonNetwork.connectedAndReady)
            {
                PhotonNetwork.ConnectUsingSettings(param.Version);
                PhotonNetwork.offlineMode = false;
            }
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.JoinLobby();
        }
        catch (UnityException)
        {
            Debug.Log("failed to join server : Photon");
            SceneManager.LoadScene("failedNetwork");
        }
        catch (Exception)
        {
            Debug.Log("failed to join server: No network");
            SceneManager.LoadScene("failedNetwork");
        }
    }

    public static bool IsConnected(string hostedURL = "http://www.google.com")
    {
        try
        {
            UnityWebRequest w = new UnityWebRequest(hostedURL);
            w.SendWebRequest();
            long Htmlcode = w.responseCode;
            Debug.Log("Code: " + Htmlcode.ToString());
            if (Htmlcode == 404)
                return false;
            else
                return true;
        }
        catch (IOException ex)
        {
            return false;
        }
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
            if (PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount == 3)
            {
                PhotonNetwork.automaticallySyncScene = true;
                MoveToGame();
            }
        }
        catch (UnityException)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
        }
        catch (Exception)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
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
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 3
        };
        System.Random tools = new System.Random();
        string random = tools.Next().ToString().Substring(0, 4);
        PhotonNetwork.JoinOrCreateRoom("room" + random, opt, TypedLobby.Default);
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
        this.joined = true;
    }
}
