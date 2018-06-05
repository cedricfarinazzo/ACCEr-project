using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SMNetwork;
using SMNetwork.Client;
using GILES;
using GILES.Example;
using System;

public class LoadOnClick : MonoBehaviour {

    public void OnClick()
    {
        int id = int.Parse(gameObject.transform.parent.name);
        Client smClient = new Client();
        SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
        SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
        SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
        try
        {
            string json = smClient.AskMapId(id)["mapjsonzip"];
            Debug.Log(json);
            pb_SceneLoader.LoadSceneModeFromJson(json);
        }
        catch (Exception)
        { }
        
    }
}
