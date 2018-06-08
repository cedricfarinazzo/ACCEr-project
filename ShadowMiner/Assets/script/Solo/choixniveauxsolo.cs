﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class choixniveauxsolo : MonoBehaviour {

    [SerializeField]
    public List<Button> niv;
    [SerializeField]
    public List<string> scene;
    [SerializeField]
    protected Button BackMenu;

    //private bool levelfinish = ; // booléen test à supprimer plus tard

    // Use this for initialization
    void Start () {
	    Cursor.visible = true;
	    Cursor.lockState = CursorLockMode.None;
        int soloprogress = SMProgress.Progress.Load().SoloStats;
        for (int i = 0; i < niv.Count && i < scene.Count; i++)
        {
            if (i <= soloprogress)
            {
                niv[i].interactable = true;
            }
            else
            {
                niv[i].interactable = false;
            }

            string name = scene[i];
            niv[i].onClick.AddListener(
            delegate 
            {
                Changementscene(name);
            });
        }
        BackMenu.onClick.AddListener(BackToMenu);
    }

	public void Changementscene(string name)
	{
		SaveData.SaveData.SaveString("Loader.Next", name);
		SceneManager.LoadScene("loading");
        //SceneManager.LoadScene(name);
	}
			
		
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
