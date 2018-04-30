using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;
using System;

public class inscription : MonoBehaviour {

	public InputField prénom;
	public InputField nom;
	public InputField pseudo;
	public InputField email;
	public InputField motdepasse;
	public InputField checkmdp;
	public Button connexion;
    public Button inscription_button;
    [SerializeField]
    protected Button BackMenu;
    public GameObject Failed;

    private Client SMClient;

    // Use this for initialization
    void Start () {
        try
        {
            this.SMClient = new Client();
        }
        catch (Exception)
        {
            Debug.Log("failed to join server");
            SceneManager.LoadScene("failedNetwork");
        }
        DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
        DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
		if (SMClient.AskMyProfil() != null)
		{
			SceneManager.LoadScene("profilplayer");
		}
        connexion.onClick.AddListener(Alreadyexist);
        inscription_button.onClick.AddListener(Create);
        BackMenu.onClick.AddListener(BackToMenu);
    }

	public void Alreadyexist(){
		SceneManager.LoadScene ("connexion");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create()
    {
        if (pseudo.text == "" 
            || prénom.text == ""
            || nom.text == ""
            || email.text == ""
            || motdepasse.text == ""
            || checkmdp.text == "")
        {
            FailedCreate("Some fields are empty!");
            return;
        }
        if (motdepasse.text != checkmdp.text )
        {
            FailedCreate("Passwords do not match!");
            return;
        }
        bool result = SMClient.Create(pseudo.text, prénom.text, nom.text, email.text, motdepasse.text);
        Debug.Log("Create: " + result.ToString());
        if (result)
        {
            SMClient.AskMyProfil();
            SaveData.SaveData.SaveString("DataClient.Token", DataClient.Token);
            SaveData.SaveData.SaveString("DataClient.Email", DataClient.Email);
            SaveData.SaveData.SaveObject("DataClient.User", DataClient.User);
            MoveToAccount();
        }
        else
        {
            Debug.Log("Failed");
        }
        return;
    }

    private void MoveToAccount()
    {
        SceneManager.LoadScene("profilplayer");
    }

    public void FailedCreate(string msg)
    {
        Failed.SetActive(true);
        Failed.GetComponentInChildren<Text>().text = msg;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
