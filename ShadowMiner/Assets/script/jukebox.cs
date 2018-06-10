using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jukebox : MonoBehaviour {

	bool allume = false;
	protected KeyCode touche;
	public AudioClip[] listedemusique;
	int size;
	// Use this for initialization
	void Start () {
		SMParametre.Parametre param = SMParametre.Parametre.Load ();
		touche = param.Key ["Interact"];
		size = listedemusique.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (Input.GetKeyDown (this.touche)) {
				if (allume) {
					this.gameObject.GetComponent<AudioSource> ().Stop();
					allume = false;
					;
				} else {
					size--;
					if (size < 0) {
						size = listedemusique.Length - 1;
					}
					this.gameObject.GetComponent<AudioSource> ().PlayOneShot (listedemusique [size]);
					allume = true;
				}
			}
		}
	}

				
}
