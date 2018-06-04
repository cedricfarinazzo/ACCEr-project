using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SMProgress;
using SMNetwork;
using SMNetwork.Client;
using System;

public class progress : MonoBehaviour {

    [SerializeField]
    protected Text solo, multi, lastupdate;

    [SerializeField]
    protected GameObject offline, online;

    private Client SMClient;
    private bool isoffline = false;

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        try
        {
            SMClient = new Client();
            SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
            SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
            SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
            if (SMClient.AskMyProfil() == null)
            {
                isoffline = true;
            }
        }
        catch (UnityException)
        {
            Debug.Log("failed to join server");
            isoffline = true;
        }
        catch (Exception)
        {
            Debug.Log("Failed to join server");
            isoffline = true;
        }
        Progress progress = Progress.Load();
        solo.text = "Solo progression : " + progress.SoloStats;
        multi.text = "Multiplayer game won : " + progress.MultiStats;
        lastupdate.text = "Last update : " + progress.LastUpdate;


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
    }

    public void Upload()
    {
        if (!isoffline)
        {
            Debug.Log("UPLOAD");
            RefreshFromServer();
            Progress progress = Progress.Load();
            SMClient.UpdateProgress(progress.SoloStats, progress.MultiStats);
        }
    }

    public void RefreshFromServer()
    {
        if (!isoffline)
        {
            Debug.Log("REFRESH");
            Progress progress = Progress.Load();
            var data = SMClient.AskProgress();
            progress.SoloStats = progress.SoloStats < int.Parse(data["SoloStats"]) ? int.Parse(data["SoloStats"]) : progress.SoloStats;
            progress.MultiStats = progress.MultiStats < int.Parse(data["MultiStats"]) ? int.Parse(data["MultiStats"]) : progress.MultiStats;
            progress.LastUpdate = DateTime.Parse(progress.LastUpdate) < DateTime.Parse(data["LastTime"]) ? data["LastTime"] : progress.LastUpdate;
            solo.text = "Solo progression : " + progress.SoloStats;
            multi.text = "Multiplayer game won : " + progress.MultiStats;
            lastupdate.text = "Last update : " + progress.LastUpdate;
            progress.Save();
        }
    }
}
