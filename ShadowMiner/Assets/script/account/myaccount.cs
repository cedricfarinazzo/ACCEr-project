using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SMNetwork.Client;
using SaveData;
using UnityEngine.SceneManagement;

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
		SMClient = new Client();
		prénom.text = SaveData.SaveData.GetString("DataClient.User.Firstname");
		nom.text = SaveData.SaveData.GetString("DataClient.User.Lastname");
		email.text = SaveData.SaveData.GetString("DataClient.Email");
		pseudo.text = SaveData.SaveData.GetString("DataClient.User.Login");
		description.text = SaveData.SaveData.GetString("DataClient.User.Description");
		backtomenu.onClick.AddListener(Retourmenu);
		editdata.onClick.AddListener(Edituserdata);
		editpassword.onClick.AddListener(Editpasssword);
		browsepicture.onClick.AddListener(Browsepic);
		savepicture.onClick.AddListener(Savepic);
	}

	public void Retourmenu()
	{
		SceneManager.LoadScene ("menu");
	}

	public void Edituserdata()
	{
		SMClient.UpdateData (pseudo.text, prénom.text, nom.text);
	}

	public void Browsepic()
	{
		SceneManager.LoadScene ("menu");
	}

	public void Savepic()
	{
		SMClient.UpdateData (null, null, null, description.text);
	}

	public void Editpasssword()
	{
		SceneManager.LoadScene ("menu");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
