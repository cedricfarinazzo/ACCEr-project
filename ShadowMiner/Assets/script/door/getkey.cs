using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getkey : MonoBehaviour {

	protected KeyCode touche;

	void Start()
	{
		SMParametre.Parametre param = SMParametre.Parametre.Load ();
		touche = param.Key ["Interact"];
	}
	
	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if (Input.GetKeyDown(this.touche))
				{
				GameObject commetuveux = GameObject.Instantiate (new GameObject ());
				commetuveux.name = "key";
				commetuveux.transform.SetParent (other.gameObject.transform);
				this.gameObject.SetActive (false);
				}
		}
	}
}
