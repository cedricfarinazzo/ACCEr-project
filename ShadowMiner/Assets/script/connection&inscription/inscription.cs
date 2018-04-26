using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SMNetwork.Client;

public class inscription : MonoBehaviour {

	public InputField prénom;
	public InputField nom;
	public InputField pseudo;
	public InputField email;
	public InputField motdepasse;
	public InputField checkmdp;
	public Button connexion;
    public Button inscription_button;

    private Client SMClient;

    // Use this for initialization
    void Start () {
        SMClient = new Client();
        connexion.onClick.AddListener(Alreadyexist);
        inscription_button.onClick.AddListener(Create);
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
            Debug.Log("Les champs sont vides!");
            return;
        }
        if (motdepasse.text != checkmdp.text )
        {
            Debug.Log("les mdp ne correspondent pas !");
            return;
        }
        bool result = SMClient.Create(pseudo.text, prénom.text, nom.text, email.text, motdepasse.text);
        Debug.Log("Create: " + result.ToString());
        return;
    }
}
