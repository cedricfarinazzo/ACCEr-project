using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SMNetwork;
using SMNetwork.Client;
using System;

public class LoadFromNetwork : MonoBehaviour {

    private Client smClient = null;
    private bool isoffline = false;

    [SerializeField]
    protected GameObject offline, online;
    [SerializeField]
    protected GameObject MapContainerParent, MapContainer;

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        try
        {
            smClient = new Client();
        }
        catch (UnityException)
        {
            Debug.Log("failed to join server");
            isoffline = true;
            SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
            SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
            SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
            if (smClient.AskMyProfil() == null)
            {
                isoffline = true;
            }
        }
        catch (Exception)
        {
            Debug.Log("Failed to join server");
            isoffline = true;

        }
        
        if (isoffline)
        {
            StartOffline();
        }
        else
        {
            StartOnline();
        }

    }

    private void StartOffline()
    {
        offline.SetActive(true);
        online.SetActive(false);
    }

    private void StartOnline()
    {
        offline.SetActive(false);
        online.SetActive(true);

        SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
        SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
        SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");

        List<Dictionary<string, string>> maplist = smClient.AskMapList();
        foreach(var map in maplist)
        {
            Debug.Log("Map { ID: " + map["ID"] + " ; name: " + map["name"] + " ; date: " + map["date"] + " }");
            GameObject newItem = Instantiate(MapContainer) as GameObject;
            newItem.name = map["ID"];
            newItem.GetComponentInChildren<Text>().text = "Numéro " + map["ID"] + " : " + map["name"] + " le " + map["date"];
            newItem.transform.SetParent(MapContainerParent.transform, false);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
