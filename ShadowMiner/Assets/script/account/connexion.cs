using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;
using SaveData;
using System;

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
        email.text = SaveData.SaveData.GetString("DataClient.Email");
        try
        {
            this.SMClient = new Client();
        }
        catch (Exception)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
        }
        connect.onClick.AddListener(Connect);
		inscription.onClick.AddListener(Inscription);
        BackMenu.onClick.AddListener(BackToMenu);
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