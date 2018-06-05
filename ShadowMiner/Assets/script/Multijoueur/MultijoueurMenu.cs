using SMNetwork.Client;
using SMParametre;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultijoueurMenu : Photon.MonoBehaviour {

    private SMNetwork.Client.Client SMClient;
    private List<GameObject> containerlist = new List<GameObject>(); 

    [SerializeField] protected RoomInfo[] gameList;

    [SerializeField]
    protected GameObject MapContainerParent, MapContainer;
    [SerializeField]
    protected Button Create, Refresh;
    [SerializeField]
    protected GameObject Empty, Exist;

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
            catch (Exception)
            {
                Debug.Log("Failed to join server");
                SceneManager.LoadScene("failedNetwork");
            }

            if (!PhotonNetwork.connectedAndReady)
            {
                PhotonNetwork.ConnectUsingSettings(param.Version);
                PhotonNetwork.offlineMode = false;
            }
            PhotonNetwork.playerName = SaveData.SaveData.GetString("Photon.playername");
        }
        catch (Exception)
        {
            Debug.Log("failed to join server: No network");
            SceneManager.LoadScene("failedNetwork");
        }

        Create.onClick.AddListener(CreateAsNew);
        Refresh.onClick.AddListener(RefreshExistingMap);
    }

    public void OnReceivedRoomListUpdate()
    {
        RefreshExistingMap();
    }

    public void ClearExistingMap()
    {
        for(int i = 0; i < containerlist.Count; i ++)
        {
            GameObject.Destroy(containerlist[i]);
        }
        containerlist = new List<GameObject>();
    }

    public void RefreshExistingMap()
    {
        gameList = PhotonNetwork.GetRoomList();
        Debug.Log(gameList.Length);
        ClearExistingMap();
        if (gameList.Length == 0)
        {
            Empty.SetActive(true);
            Exist.SetActive(false);
        }
        else
        {
            Empty.SetActive(false);
            Exist.SetActive(true);
            for (int i = 0; i < gameList.Length; i++)
            {
                var room = gameList[i];
                GameObject newItem = Instantiate(MapContainer) as GameObject;
                newItem.name = room.name;
                newItem.GetComponentInChildren<Text>().text = "Room name : " + room.Name + " with " + room.PlayerCount.ToString() + " / 3"; ;
                newItem.transform.SetParent(MapContainerParent.transform, false);
                containerlist.Add(newItem);
            }
        }
    }

    public void CreateAsNew()
    {
        SaveData.SaveData.SaveString("Multi.mode", "new");
        SaveData.SaveData.SaveString("Loader.Next", "lobby");
        SceneManager.LoadScene("loading");
    }
}
