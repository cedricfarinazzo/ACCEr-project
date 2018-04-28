using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class choixniveauxsolo : MonoBehaviour {

	public Button niv1;
	public Button niv2;
	public Button niv3;
	public Button niv4;
	public Button niv5;
	public Button niv6;
	public Button niv7;
	public Button niv8;
	public Button niv9;
	public Button niv10;
	public Button niv11;
	public Button niv12;
	public Button niv13;
	public Button niv14;
	public Button niv15;
	public Button niv16;
	public Button niv17;
	public Button niv18;
	public Button niv19;
	public Button niv20;
	private bool levelfinish = false; // booléen test à supprimer plus tard

	// Use this for initialization
	void Start () {
		Button[] boutons = {niv1 , niv2, niv3, niv4, niv5, niv6, niv7, niv8, niv9 ,niv10, niv11, niv12, niv13, niv14, niv15, niv16, niv17, niv18, niv19, niv20};
		foreach (Button bouton in boutons) {
			bouton.onClick.AddListener (Changementscene);
			if (!levelfinish) { // à remplacer par les données de la classe progression
				bouton.GetComponent<Image>().color = Color.black;
			}
		}
	}

	public void Changementscene()
	{
		if (levelfinish) {
			SceneManager.LoadScene ("level 1");
		}
	}
			
		
	// Update is called once per frame
	void Update () {
		
	}
}
