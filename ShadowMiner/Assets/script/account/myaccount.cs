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
using Image = UnityEngine.UI.Image;
using SMProgress;

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
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		try
		{
			SMClient = new Client();
		}
		catch (UnityException)
		{
			Debug.Log("failed to join server");
			SceneManager.LoadScene("failedNetwork");
		}
		catch(Exception)
		{
			Debug.Log ("Failed to join server");
			SceneManager.LoadScene ("failedNetwork");
			
		}
		SMNetwork.Client.DataClient.Email = SaveData.SaveData.GetString("DataClient.Email");
		SMNetwork.Client.DataClient.Token = SaveData.SaveData.GetString("DataClient.Token");
		SMNetwork.Client.DataClient.User = SaveData.SaveData.GetObject<SMNetwork.DataUser>("DataClient.User");
		if (SMClient.AskMyProfil() == null)
		{
			SceneManager.LoadScene("connexion");
		}

        Debug.Log("REFRESH");
        Progress progress = Progress.Load();
        var data = SMClient.AskProgress();
        progress.SoloStats = progress.SoloStats < int.Parse(data["SoloStats"]) ? int.Parse(data["SoloStats"]) : progress.SoloStats;
        progress.MultiStats = progress.MultiStats < int.Parse(data["MultiStats"]) ? int.Parse(data["MultiStats"]) : progress.MultiStats;
        progress.LastUpdate = DateTime.Parse(progress.LastUpdate) < DateTime.Parse(data["LastTime"]) ? data["LastTime"] : progress.LastUpdate;
        progress.Save();

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
		
		/*Bitmap img = (Bitmap)SMClient.AskMyImage();
		
		
		/*
		Texture2D t = new Texture2D(img.Width, img.Height);
		for (int i = 0; i < img.Width; i++)
		{
			for (int j = 0; j < img.Height; j++)
			{
				int r = img.GetPixel(i, j).R;
				int g = img.GetPixel(i, j).G;
				int b = img.GetPixel(i, j).B;
				int a = img.GetPixel(i, j).A;
				t.SetPixel(i, j, new Color(r, g, b, a));
			}
		}
		t.Apply();
		Texture2D bmp = new Texture2D(img.Width, img.Height, TextureFormat.RGBA32, false);
		bmp.LoadRawTextureData(ConvertImage.ImageToByteArray((ImageSys)img));
		Vector2 pivot = new Vector2(0.5f, 0.5f);
		Rect tRect = new Rect(0, 0, img.Width, img.Height);
		Sprite newSprite = Sprite.Create(t, tRect, pivot);
		profilpic.overrideSprite = newSprite;
		profilpic.texture = t;
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(ConvertImage.ImageToByteArray((ImageSys)img));
		profilpic.material.mainTexture = tex;
		*/
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
			Debug.Log(SMClient.UpdatePassword(oldpassword.text, newpassword.text));
		}
		return;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
