using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;
using SaveData;

public class connexion : MonoBehaviour {

    public InputField email;
	public InputField password;
    public Button connect;
	public Button inscription;
    [SerializeField]
    protected Button BackMenu;

    private SMNetwork.Client.Client SMClient;

    public void Start()
    {
        email.text = SaveData.SaveData.GetString("DataClient.Email");
        Debug.Log(SaveData.SaveData.GetString("DataClient.Token"));
        this.SMClient = new Client();
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
			//Debug.Log ("Logged as guest");
		}
	}

    public void Connect()
    {
        if (SMClient.Connect(email.text, password.text))
        {
            Debug.Log("Connected: True");
            SaveData.SaveData.SaveString("DataClient.Token", DataClient.Token);
            SaveData.SaveData.SaveString("DataClient.Email", DataClient.Email);
            Debug.Log("Token: \"" + DataClient.Token + "\"");
            Debug.Log(SMClient.AskMyProfil());
            Debug.Log(SMClient.AskProfil("antoine.claudel@hotmail.fr"));
            Debug.Log("Logout: " + SMClient.Logout());
        }
        else
        {
            Debug.Log("Connected: false");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}