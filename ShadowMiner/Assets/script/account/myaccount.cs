using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SMNetwork.Client;
using SaveData;
using UnityEngine.SceneManagement;
using System.Drawing;
using System.Drawing.Imaging;
using ImageSys = System.Drawing.Image;
using Image = UnityEngine.UI.Image;

public class myaccount : MonoBehaviour {
	private SMNetwork.Client.Client SMClient;
	public InputField prénom;
	public InputField nom;
	public InputField email;
	public InputField pseudo;
	public InputField description;
	public InputField oldpassword;
	public InputField newpassword;
	public InputField confpassword;
	public Button backtomenu;
	public Button editdata;
	public Button editpassword;
	public Button browsepicture;
	public Button savepicture;
	public Image profilpic;

	// Use this for initialization
	void Start () {
		try
		{SMClient = new Client();}
		catch(Exception)
		{Debug.Log ("Failed to join server");
			SceneManager.LoadScene ("failedNetwork");}
		SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
		SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
		SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
		if (SMClient.AskMyProfil() == null)
		{
			SceneManager.LoadScene("connexion");
		}
		email.text = DataClient.Email;
		prénom.text = DataClient.User.Firstname;
		nom.text = DataClient.User.Lastname;
		pseudo.text = DataClient.User.Login;
		description.text = DataClient.User.Description;
		backtomenu.onClick.AddListener(Retourmenu);
		editdata.onClick.AddListener(Edituserdata);
		editpassword.onClick.AddListener(Editpasssword);
		browsepicture.onClick.AddListener(Browsepic);
		savepicture.onClick.AddListener(Savepic);
		ImageSys img = SMClient.AskMyImage();
		//profilpic.sprite = 
	}

	public void Retourmenu()
	{
		SceneManager.LoadScene("menu");
	}

	public void Edituserdata()
	{
		Debug.Log(SMClient.UpdateData (pseudo.text, prénom.text, nom.text));
	}

	public void Browsepic()
	{
		
	}

	public void Savepic()
	{
		SMClient.UpdateData (null, null, null, description.text);
	}

	public void Editpasssword()
	{
		if (oldpassword.text != ""
		    && newpassword.text != ""
		    && confpassword.text != ""
		    && newpassword.text == confpassword.text)
		{
			Debug.Log(SMClient.UpadatePassword(oldpassword.text, newpassword.text));
		}
		return;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
