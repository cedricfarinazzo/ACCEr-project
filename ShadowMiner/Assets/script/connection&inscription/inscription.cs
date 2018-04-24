using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inscription : MonoBehaviour {

	public InputField prénom;
	public InputField nom;
	public InputField pseudo;
	public InputField email;
	public InputField motdepasse;
	public InputField checkmdp;
	public Button connexion;

	// Use this for initialization
	void Start () {
		//God was here
		connexion.onClick.AddListener(Alreadyexist);
	}

	public void Alreadyexist(){
		SceneManager.LoadScene ("connexion");
	}
	
	// Update is called once per frame
	void Update () {
		//à toi de jouer Cédric, RAJOUTE PAS DE STERILIZED !
	}
}
