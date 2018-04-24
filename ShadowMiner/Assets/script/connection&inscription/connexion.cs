using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;

public class connexion : MonoBehaviour {

    public InputField email;
	public InputField password;
    public Button connect;
	public Button inscription;

    private SMNetwork.Client.Client SMClient;

    public void Start()
    {
        this.SMClient = new Client();
        connect.onClick.AddListener(Connect);
		inscription.onClick.AddListener(Inscription);
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
        Debug.Log("Connected: " + SMClient.Connect(email.text, password.text).ToString());
        Debug.Log("Token: \"" + DataClient.Token + "\"");
        Debug.Log(SMClient.AskMyProfil());
        Debug.Log(SMClient.AskProfil("antoine.claudel@hotmail.fr"));
        Debug.Log("Logout: " + SMClient.Logout());
    }
}