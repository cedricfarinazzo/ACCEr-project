using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;
using SaveData;
using System;
using SMProgress;

public class connexion : MonoBehaviour {

    public InputField email;
	public InputField password;
    public Button connect;
	public Button inscription;
    [SerializeField]
    protected Button BackMenu;
    public GameObject Failed;

    private SMNetwork.Client.Client SMClient;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        try
        {
            this.SMClient = new Client();
            DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
            DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
            var data = SMClient.AskMyProfil();
            if ( data != null)
            {
                Progress progress = Progress.Load();
                var datap = SMClient.AskProgress();
                progress.SoloStats = progress.SoloStats < int.Parse(datap["SoloStats"]) ? int.Parse(datap["SoloStats"]) : progress.SoloStats;
                progress.MultiStats = progress.MultiStats < int.Parse(datap["MultiStats"]) ? int.Parse(datap["MultiStats"]) : progress.MultiStats;
                progress.LastUpdate = DateTime.Parse(progress.LastUpdate) < DateTime.Parse(datap["LastTime"]) ? datap["LastTime"] : progress.LastUpdate;
                progress.Save();
                SaveData.SaveData.SaveString("Photon.playername", data.Login);
                SceneManager.LoadScene("profilplayer");
            }

            connect.onClick.AddListener(Connect);
            inscription.onClick.AddListener(Inscription);
            BackMenu.onClick.AddListener(BackToMenu);
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

	public void Inscription()
	{
		SceneManager.LoadScene ("inscription");
	}

    void Update ()
	{
		if (Input.GetKeyDown("return")) {
            Connect();
		}
	}

    public void Connect()
    {
        if (SMClient.Connect(email.text, password.text))
        {
            Debug.Log("Connected: True");
            SMClient.AskMyProfil();
            SaveData.SaveData.SaveString("DataClient.Token", DataClient.Token);
            SaveData.SaveData.SaveString("DataClient.Email", DataClient.Email);
            SaveData.SaveData.SaveObject("DataClient.User", DataClient.User);
            Progress progress = Progress.Load();
            var data = SMClient.AskProgress();
            progress.SoloStats = progress.SoloStats < int.Parse(data["SoloStats"]) ? int.Parse(data["SoloStats"]) : progress.SoloStats;
            progress.MultiStats = progress.MultiStats < int.Parse(data["MultiStats"]) ? int.Parse(data["MultiStats"]) : progress.MultiStats;
            progress.LastUpdate = DateTime.Parse(progress.LastUpdate) < DateTime.Parse(data["LastTime"]) ? data["LastTime"] : progress.LastUpdate;
            progress.Save();
            MoveToAccount();
        }
        else
        {
            FailedConnect();
            Debug.Log("Connected: false");
        }
    }

    public void FailedConnect()
    {
        Failed.SetActive(true);
    }

    private void MoveToAccount()
    {
        SceneManager.LoadScene("profilplayer");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}